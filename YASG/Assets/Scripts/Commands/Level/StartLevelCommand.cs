using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Helpers;
using strange.extensions.command.impl;

namespace Assets.Scripts.Commands.Level
{
    public class StartLevelCommand : Command
    {
        private const float StarterInterval = 0.5f;

        [Inject]
        public Timer Timer { get; set; }

        [Inject]
        public StartGameSignal StartGameSignal { get; set; }

        public override void Execute()
        {
            StartGameSignal.Dispatch();
            Timer.loop = true;
            //_deltaSpeed = -0.02f;
            Timer.Start(StarterInterval);
        }
    }
}
