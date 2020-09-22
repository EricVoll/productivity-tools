using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityCore.Commands
{
    interface ICommandBookOwner
    {
        CommandBook book { get; set; }
    }
}
