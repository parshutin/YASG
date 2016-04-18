using Assets.Scripts.Core;
using Assets.Scripts.Enums;
using Assets.Scripts.Snake;
using Assets.Scripts.Views.Level.GameField;
using strange.extensions.command.impl;
using strange.extensions.pool.api;
using UnityEngine;

namespace Assets.Scripts.Commands.Level
{
    public class CreateFoodCommand : Command
    {
        [Inject]
        public Field Field { get; set; }

        [Inject]
        public FoodContainer FoodContainer { get; set; }

        [Inject(GameElement.FoodPool)]
        public IPool<GameObject> Pool { get; set; }

        public override void Execute()
        {
            var cell = Field.GetFoodCell();
            var instance = Pool.GetInstance();
            var food = instance.GetComponent<FoodView>();
            food.Init(cell.Coorditanes[0], cell.Coorditanes[1]);
            instance.transform.localPosition = new Vector3(cell.Coorditanes[0], cell.Coorditanes[1], 20f);
            instance.name = "Food";
            FoodContainer.AddFood(food);
        }
    }
}
