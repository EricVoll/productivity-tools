using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductivityCore.Commands
{
    public class CommandBase
    {
        public CommandBase(string name, string uniqueName, CommandType type, string description)
        {
            Name = name;
            CommandType = type;
            Description = description;
            UniqueName = uniqueName;
        }

        public int id = -1;
        public string Name;
        public int MaxNrArgs;
        public CommandType CommandType;
        public string Description;
        public string UniqueName;

        protected string[] args;
        protected bool executionFinished = false;

        protected virtual bool SubCheck(string[] args)
        {
            return true;
        }

        /// <summary>
        /// Returns true if the input corresponds to this command's structure
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Prepare(string[] inputs)
        {
            var args = inputs.Skip(1).ToArray();
            if (inputs[0] == Name && SubCheck(args))
            {
                this.args = args;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Implemented by inheriting classes
        /// </summary>
        protected virtual void Explain()
        {

        }

        public void PrintExplanation()
        {
            Output.Write(Description);
            Explain();
        }

        public virtual void Execute()
        {
            if (args.Length > 1 && args[1] == "?")
            {
                PrintExplanation();
                executionFinished = true;
            }
        }

        public void Reset()
        {
            executionFinished = false;
        }

        internal virtual void Add(string[] vs)
        {
            Output.Write("Not implemented for this type!");
        }

        internal virtual void Delete(string v)
        {
            throw new NotImplementedException();
        }
    }
}
