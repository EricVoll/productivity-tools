using ProductivityCore;
using ProductivityCore.Commands;
using System;
using System.Collections.Generic;


namespace ProductivityConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var book = new Importer().Import();

            CommandInterpreter interpreter = new CommandInterpreter();

            //Load data
            Output.Write("Loading data...");
            interpreter.InterpretCommand(book, "load");

            while (true)
            {
                Console.Write("\ncommand: ");
                string input = Console.ReadLine();
                interpreter.InterpretCommand(book, input);
            }
        }
    }
}
