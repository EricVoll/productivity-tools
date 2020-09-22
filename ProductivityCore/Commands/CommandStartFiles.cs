using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ProductivityCore.Commands
{
    public class CommandStartFiles : CommandBase
    {
        public CommandStartFiles(string[] filesToStart, string subName) : base("start", subName, CommandType.StartFiles, "Starts a list of files with their default apps")
        {
            this.filesToStart = filesToStart;
            workType = subName;
        }

        protected override bool SubCheck(string[] args)
        {
            return workType == args[0];
        }

        public string[] filesToStart { get; set; }
        public string workType { get; set; }

        protected override void Explain()
        {
            Output.Write("Opens these files:");
            int counter = 0;
            foreach (var file in filesToStart)
            {
                Output.Write($"Nr {counter++}: {file}");
            }
        }
        public override void Execute()
        {
            base.Execute();

            if (executionFinished) return;

            if (args[0] == workType)
            {

                try
                {
                    foreach (var file in filesToStart)
                    {
                        var fileInfo = new FileInfo(file);

                        if (file.StartsWith(@"C:\") && !File.Exists(file))
                        {
                            Output.Write($"File {fileInfo.Name} does not exist");
                            continue;
                        }

                        if (file.Contains(@":\") || file.Contains(@"://"))
                            OpenWithDefaultProgram(file);
                        else
                            Process.Start(file);

                        Output.Write($"Started {file}");
                    }
                }
                catch (Exception ex)
                {
                    Output.Write("Error: " + ex.Message);
                }
            }

        }

        public void OpenWithDefaultProgram(string path)
        {
            Process fileopener = new Process();
            fileopener.StartInfo.FileName = "explorer";
            fileopener.StartInfo.Arguments = "\"" + path + "\"";
            fileopener.Start();
        }


        internal override void Add(string[] vs)
        {
            List<string> files = filesToStart.ToList();
            files.Add(string.Join(' ', vs));
            filesToStart = files.ToArray();
            Output.Write($"Added {vs[0]} to file list");
        }

        /// <summary>
        /// Delets the specified index if ok
        /// </summary>
        /// <param name="v"></param>
        internal override void Delete(string v)
        {
            if(Int32.TryParse(v, out int index)){
                if(filesToStart.Length > index)
                {
                    var list = filesToStart.ToList();
                    string fileToDelete = list[index];
                    list.RemoveAt(index);
                    filesToStart = list.ToArray();
                    Output.Write($"Deleted {fileToDelete} from {UniqueName}");
                    return;
                }
            }
            Output.Write("An Error occurred while deleting the file");
        }
    }
}
