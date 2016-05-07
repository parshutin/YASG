using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Core;
using Assets.Scripts.Helpers;
using Assets.Scripts.Snake;
using strange.extensions.command.impl;

namespace Assets.Scripts.Commands.Level.GameField
{
    public class StopGameCommand : Command
    {
        [Inject]
        public SnakeContainer Container { get; set; }

        [Inject]
        public Timer Timer { get; set; }

        [Inject]
        public Field Field { get; set; }

        [Inject]
        public CleanSnakeContainerSignal CleanSnakeContainerSignal { get; set; }

        [Inject]
        public StopFieldCheckingSignal StopFieldCheckingSignal { get; set; }

        public override void Execute()
        {
            StopFieldCheckingSignal.Dispatch();
            Timer.Stop();
            CleanSnakeContainerSignal.Dispatch();
            Field.Clear();
        }
    }
}
