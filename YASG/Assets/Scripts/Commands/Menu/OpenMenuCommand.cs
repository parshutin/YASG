using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using strange.extensions.command.impl;

namespace Assets.Scripts.Commands.Menu
{
    public class OpenMenuCommand : Command
    {
        [Inject]
        public ShowMenuSignal ShowMenuSignal { get; set; }

        public override void Execute()
        {
            ShowMenuSignal.Dispatch();
        }
    }
}
