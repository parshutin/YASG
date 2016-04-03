using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Core;
using Assets.Scripts.Views.Game;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.Scripts.Mediators.Game
{
    public class SnakeBodyPartMediator : EventMediator
    {
        [Inject]
        public SnakeBodyPartView View { get; set; }

        private void Start()
        {
            
        }
    }
}
