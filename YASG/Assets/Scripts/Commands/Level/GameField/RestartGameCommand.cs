using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Core;
using Assets.Scripts.Helpers;
using strange.extensions.command.impl;

namespace Assets.Scripts.Commands.Level.GameField
{
    public class RestartGameCommand : Command
    {
        private const float StarterInterval = 0.5f;

        private const float DeltaSpeed = -0.2f;

        [Inject]
        public StopGameSignal StopGameSignal { get; set; }

        [Inject]
        public StartGameSignal StartGameSignal { get; set; }

        [Inject]
        public CleanFoodContainerSignal CleanFoodContainerSignal { get; set; }

        [Inject]
        public Timer Timer { get; set; }

        [Inject]
        public Field Field { get; set; }

        public override void Execute()
        {
            StopGameSignal.Dispatch();
            CleanFoodContainerSignal.Dispatch();
            Field.CreateSnake();
            Timer.loop = true;
            Timer.SetDelta(DeltaSpeed);
            Timer.Start(StarterInterval);

            StartGameSignal.Dispatch();
        }
    }
}
