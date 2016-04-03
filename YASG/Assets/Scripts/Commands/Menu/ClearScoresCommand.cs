using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.UserData;
using strange.extensions.command.impl;

namespace Assets.Scripts.Commands.Menu
{
    public class ClearScoresCommand : Command
    {
        public override void Execute()
        {
            ScoresManager.Instance.Clear();
        }
    }
}
