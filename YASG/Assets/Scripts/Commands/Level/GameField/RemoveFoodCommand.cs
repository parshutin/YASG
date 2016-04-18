using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Enums;
using Assets.Scripts.Snake;
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
        public int[] Coordinates { get; set; }

        [Inject]
        public FoodContainer FoodContainer { get; set; }

        public override void Execute()
        {
            var food = FoodContainer.GetFoodView(Coordinates);
            if (food != null)
            {
                food.gameObject.SetActive(false);
                FoodContainer.RemoveFoodItem(Coordinates);
                Pool.ReturnInstance(food.gameObject);
            }
        }
    }
}
