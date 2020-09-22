using ProductivityCore.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityCore
{
    public class CommandInterpreter
    {
        public void InterpretCommand(CommandBook book, string input)
        {
            if(input == "?")
            {
                book.Explain();
                return;
            }

            string[] parts = input.Split(' ');

            for (int i = 0; i < book.Commands.Count; i++) //not a foreach because some commands edit the loop
            {
                var command = book.Commands[i];
                if (command.Prepare(parts))
                {
                    command.Execute();
                    command.Reset();
                }
            }
        }
    }
}
