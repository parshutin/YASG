using Assets.Scripts.Core;
using Assets.Scripts.Enums;
using strange.extensions.command.impl;
using strange.extensions.pool.api;
using UnityEngine;

namespace Assets.Scripts.Commands.Level
{
    public class CreateFoodCommand : Command
    {
        [Inject]
        public Field Field { get; set; }

        [Inject(GameElement.FoodPool)]
        public IPool<GameObject> Pool { get; set; }

        public override void Execute()
        {
            var cell = Field.GetFoodCell();
            var instance = Pool.GetInstance();
            instance.transform.localPosition = new Vector3(cell.Coorditanes[0], cell.Coorditanes[1], 20f);
            instance.name = "Food";
        }
    }
}
