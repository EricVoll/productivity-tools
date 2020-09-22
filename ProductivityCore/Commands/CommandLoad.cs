using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ProductivityCore.Commands
{
    public class CommandLoad : CommandBase, ICommandBookOwner
    {
        public CommandLoad() : this(null)
        {

        }
        public CommandLoad(CommandBook book) : base("load", "load", CommandType.Load, "Loads the commands from the specified location")
        {
            this.book = book;
        }

        [JsonIgnore]
        public CommandBook book { get; set; }

        public override void Execute()
        {
            base.Execute();
            if (executionFinished) return;
            

            string savePath = "";
            if (args.Length > 0)
            {
                savePath = args[0];
            }
            else
            {
                savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ProductivityConsole");
                Directory.CreateDirectory(savePath);
                savePath = Path.Combine(savePath, "save.json");
            }

            string json = File.ReadAllText(savePath);
            var importedBook = JsonConvert.DeserializeObject<CommandBook>(json, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            book.Commands = importedBook.Commands;

            foreach (var cmd in book.Commands.Where(x => x is ICommandBookOwner).Cast<ICommandBookOwner>())
            {
                cmd.book = book;
            }

            Output.Write($"Loaded commands from {savePath}");
        }
    }
}
