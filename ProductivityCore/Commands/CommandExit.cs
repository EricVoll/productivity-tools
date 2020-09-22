using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityCore.Commands
{
    public class CommandExit : CommandBase
    {
        public CommandExit() : base("exit", "exit", CommandType.Exit, "Closes the application")
        {

        }

        public override void Execute()
        {
            base.Execute();

            if (executionFinished) return;

            Environment.Exit(0);
        }
    }
}
