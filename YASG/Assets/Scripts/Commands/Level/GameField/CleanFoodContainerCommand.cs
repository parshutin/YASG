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
    public class CleanFoodContainerCommand : Command
    {
        [Inject(GameElement.FoodPool)]
        public IPool<GameObject> Pool { get; set; }

        [Inject]
        public FoodContainer FoodContainer { get; set; }

        public override void Execute()
        {
            foreach (var food in FoodContainer.Food)
            {
                food.gameObject.SetActive(false);
                Pool.ReturnInstance(food.gameObject);
            }

            FoodContainer.Clear();
        }
    }
}