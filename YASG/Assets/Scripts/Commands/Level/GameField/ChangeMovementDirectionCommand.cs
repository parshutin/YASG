using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Core;
using strange.extensions.command.impl;

namespace Assets.Scripts.Commands.Level.GameField
{
    public class ChangeMovementDirectionCommand : Command
    {
        [Inject]
        public Field GameField { get; set; }

        [Inject]
        public MovementDirection Direction { get; set; }

        public override void Execute()
        {
            GameField.ChangeMovementDirection(Direction);
        }
    }
}
