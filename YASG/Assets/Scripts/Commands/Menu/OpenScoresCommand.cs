using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.UserData;
using strange.extensions.command.impl;

namespace Assets.Scripts.Commands.Menu
{
    public class OpenScoresCommand : Command
    {

        [Inject]
        public InitScoresSignal InitScoresSignal { get; set; }

        public override void Execute()
        {
            InitScoresSignal.Dispatch(ScoresManager.Instance.Scores);
        }
    }
}
