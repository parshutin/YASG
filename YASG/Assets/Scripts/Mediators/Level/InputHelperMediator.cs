using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Commands.Level;
using Assets.Scripts.Core;
using Assets.Scripts.Views.Level.GameField;
using strange.extensions.mediation.impl;

namespace Assets.Scripts.Mediators.Level
{
    public class InputHelperMediator : EventMediator
    {
        [Inject]
        public InputHelperView View { get; set; }

        [Inject]
        public ChangeMovementDirectionSignal ChangeMovementDirectionSignal { get; set; }

        public override void OnRegister()
        {
            View.DirectionChanged += ViewOnDirectionChanged;
        }

        private void ViewOnDirectionChanged(MovementDirection movementDirection)
        {
            ChangeMovementDirectionSignal.Dispatch(movementDirection);
        }

        public override void OnRemove()
        {
            View.DirectionChanged -= ViewOnDirectionChanged;
        }
    }
}
