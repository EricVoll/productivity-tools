using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProductivityCore.Commands
{
    public class CommandSave : CommandBase, ICommandBookOwner
    {
        public CommandSave() : this(null)
        {

        }

        public CommandSave(CommandBook book) : base("save", "save", CommandType.Save, "Saves the current command list to the drive")
        {
            this.book = book;
        }

        [JsonIgnore]
        public CommandBook book { get; set; }

        public override void Execute()
        {
            base.Execute();
            if (executionFinished) return;
            string json = JsonConvert.SerializeObject(
                  book,
                  new JsonSerializerSettings()
                  {
                      TypeNameHandling = TypeNameHandling.Auto
                  });



            string savePath = "";
            if(args.Length > 0)
            {
                savePath = args[0];
            }
            else
            {
                savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ProductivityConsole");
                Directory.CreateDirectory(savePath);
                savePath = Path.Combine(savePath, "save.json");
            }

            File.WriteAllText(savePath, json);
            Output.Write($"Saved commands to {savePath}");
        }
    }
}
