using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Helpers;
using strange.extensions.command.impl;

namespace Assets.Scripts.Commands.Level.GameField
{
    public class ChangeTimerIntervalCommand : Command
    {
        [Inject]
        public Timer Timer { get; set; }

        public override void Execute()
        {
            Timer.ChangeTimeInterval();
        }
    }
}
