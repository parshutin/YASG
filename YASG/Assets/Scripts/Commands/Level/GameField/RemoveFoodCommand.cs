using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Enums;
using strange.extensions.command.impl;
using strange.extensions.pool.api;
using UnityEngine;

namespace Assets.Scripts.Commands.Level.GameField
{
    public class RemoveFoodCommand : Command
    {
        [Inject(GameElement.FoodPool)]
        public IPool<GameObject> Pool { get; set; }

        [Inject]
        public GameObject Food { get; set; }

        public override void Execute()
        {
            Food.SetActive(false);
            Pool.ReturnInstance(Food);
        }
    }
}
