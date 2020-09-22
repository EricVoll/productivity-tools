using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductivityCore.Commands
{
    public class CommandEditCommand : CommandBase, ICommandBookOwner
    {
        public CommandEditCommand() : this(null)
        {

        }

        public CommandEditCommand(CommandBook book) : base("editcmd", "edit", CommandType.EditCmd, "Edits an existing command or creates it if it does not exist")
        {
            this.book = book;
        }

        /*
         
        editcmd [add] [type] [args[]]   --> Add a full command
        editcmd [add] [id] [args[]]     --> Add a part of a command specified by args[] "one item"
        editcmd [del] [id]              --> Delete a full command
        editcmd [del] [id] [index]      --> Delete part of a command specified by index if implemented by command
         
         */

        [JsonIgnore]
        public CommandBook book { get; set; }

        enum EditCmdType { add, delete, editDescription }

        public override void Execute()
        {
            base.Execute();

            if (executionFinished) return;

            //parse arguments
            EditCmdType editCmdType = EditCmdType.add;
            switch (args[0])
            {
                case "add": editCmdType = EditCmdType.add; break;
                case "del": editCmdType = EditCmdType.delete; break;
                case "editD": editCmdType = EditCmdType.editDescription; break;
            }

            switch (editCmdType)
            {
                case EditCmdType.add:
                    HandleAddCmd(args);
                    break;
                case EditCmdType.delete:
                    string idToEdit = args[1];
                    CommandBase cmd = GetCommand(idToEdit);

                    if (cmd == null)
                    {
                        Output.Write($"Id value is invalid. Parsing {idToEdit} to int did not work");
                        return;
                    }

                    HandleDeleteCmd(args, cmd);

                    break;
                case EditCmdType.editDescription:
                    CommandBase cmdToEdit = GetCommand(args[1]);
                    cmdToEdit.Description = string.Join(" ", args.Skip(2));
                    Output.Write("Changed description");
                    break;
            }
        }

        protected override void Explain()
        {
            Output.Write("Changes other commands.");
            Output.Write("Usage:");

            Output.Write("editcmd [add] [type] [args[]]   --> Add a full command");
            Output.Write("editcmd [add] [id] [args[]]     --> Add a part of a command specified by args[] \"one item\"");
            Output.Write("editcmd [del] [id]              --> Delete a full command");
            Output.Write("editcmd [del] [id] [index]      --> Delete part of a command specified by index if implemented by command");
            Output.Write("editcmd [editD] [id] [string]   --> Set a command's description");
        }

        private void HandleAddCmd(string[] args)
        {
            CommandBase newCmd = null;
            switch (args[1])
            {
                case "EditCmd":
                    newCmd = new CommandEditCommand(book);
                    book.AddCommand(newCmd);
                    break;
                case "StartFiles":
                    newCmd = new CommandStartFiles(args.Skip(3).ToArray(), args[2]);
                    book.AddCommand(newCmd);
                    break;
                //Add more here
                default:
                    //is id -> Add sub-part of command
                    var cmd = GetCommand(args[1]);
                    if (cmd == null)
                    {
                        Output.Write("Command not found by name or id");
                        return;
                    }
                    cmd.Add(args.Skip(2).ToArray());
                    break;
            }
        }

        private void HandleDeleteCmd(string[] args, CommandBase cmd)
        {
            if (cmd == null)
            {
                Output.Write($"No command with specified id found");
                return;
            }

            if (args.Length > 2)
            {
                cmd.Delete(args[2]);
            }
            else
            {
                book.Commands.Remove(cmd);
                Output.Write($"Deleted command {cmd.UniqueName}");
            }

        }
        private CommandBase GetCommand(string IdOrName)
        {
            CommandBase cmd = null;
            if (int.TryParse(IdOrName, out int id))
            {
                cmd = book.Commands.FirstOrDefault(x => x.id == id);
            }
            else
            {
                cmd = book.Commands.FirstOrDefault(x => x.UniqueName == IdOrName);
            }
            return cmd;
        }
    }
}

