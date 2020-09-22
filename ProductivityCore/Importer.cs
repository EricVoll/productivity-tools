using ProductivityCore.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityCore
{
    public class Importer
    {
        public CommandBook Import()
        {
            List<CommandBase> commands = new List<CommandBase>()
            {
                new CommandExit(),
                new CommandStartFiles(
                    new []
                    {
                        @"C:\Users\ericv\AppData\Local\slack\slack.exe",
                        @"C:\Users\ericv\Documents\TheraPsy\02_TheraPsy_Windows\01_TheraPsy\TheraPsySln\TheraPsySln.sln",
                        @"C:\Users\ericv\AppData\Local\gitkraken\app-7.3.1\gitkraken.exe",
                        @"spotify",
                    }, "tpdev"),
                new CommandStartFiles(
                    new []
                    {
                        @"spotify",
                    }, "spotify"),
                new CommandStartFiles(
                    new []
                    {
                        @"https://therapsy-org.freshdesk.com/a/tickets/filters/open",
                        @"https://therapsy.freshchat.com/a/159825359782547/inbox/0/0",
                    }, "tpsup"),
            };

            CommandBook book = new CommandBook();
            book.AddCommands(commands);
            book.AddCommand(new CommandEditCommand(book));
            book.AddCommand(new CommandSave(book));
            book.AddCommand(new CommandLoad(book));

            return book;
        }
    }
}
