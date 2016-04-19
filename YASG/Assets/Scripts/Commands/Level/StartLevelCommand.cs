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

        private const float DeltaSpeed = -0.2f;

        [Inject]
        public Timer Timer { get; set; }

        [Inject]
        public StartGameSignal StartGameSignal { get; set; }

        public override void Execute()
        {
            StartGameSignal.Dispatch();
            Timer.loop = true;
            Timer.SetDelta(DeltaSpeed);
            Timer.Start(StarterInterval);
        }
    }
}
