using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Core;
using strange.extensions.command.impl;

namespace Assets.Scripts.Commands.Level.GameField
{

    public class FoodItedCommand : Command
    {
        [Inject]
        public Player Player { get; set; }

        [Inject]
        public SnakeLengthChangedSignal SnakeLengthChangedSignal { get; set; }

        [Inject]
        public ScoreChangedSignal ScoreChangedSignal { get; set; }


        public override void Execute()
        {
            Player.AddScore();
            SnakeLengthChangedSignal.Dispatch(Player.SnakeLenght);
            ScoreChangedSignal.Dispatch(Player.Score);
        }
    }
}
