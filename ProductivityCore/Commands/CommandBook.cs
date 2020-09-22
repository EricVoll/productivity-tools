using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityCore.Commands
{
    public class CommandBook
    {
        public CommandBook()
        {

        }

        public void AddCommand(CommandBase command)
        {
            if (command == null)
            {
                Output.Write("Command cannot be null while adding it");
                return;
            }
            command.id = GetId();
            Commands.Add(command);
        }

        public void AddCommands(List<CommandBase> commands)
        {
            foreach (var cmd in commands)
            {
                AddCommand(cmd);
            }
        }

        private int currentId = 0;
        private int GetId()
        {
            return ++currentId;
        }

        public List<Commands.CommandBase> Commands { get; set; } = new List<CommandBase>();

        public void Explain()
        {
            Output.Write("-------------------------------------------------------------------------");
            Output.Write("---  This is a tool to automate some steps of your daily worklive     ---");
            Output.Write("---  It provides a certain set of commands, such that you can adjust  ---");
            Output.Write("---  the commands to your requirements                                ---");
            Output.Write("---  Currently the following commands exist:                          ---");
            Output.Write("---  Hacked together by Eric                                          ---");
            Output.Write("-------------------------------------------------------------------------");
            foreach (var cmd in Commands)
            {
                Output.Write("----------------");
                Output.Write($"({cmd.id.ToString().PadLeft(2, '0')}) {cmd.Name} {cmd.UniqueName}  - type: {cmd.CommandType}");
                cmd.PrintExplanation();
            }
        }
    }
}
