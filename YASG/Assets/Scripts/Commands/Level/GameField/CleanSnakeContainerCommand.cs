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
    public class CleanSnakeContainerCommand : Command
    {
        [Inject(GameElement.SnakePartsPool)]
        public IPool<GameObject> Pool { get; set; }

        [Inject]
        public SnakeContainer Container { get; set; }

        public override void Execute()
        {
            foreach (var snakeBodyPartView in Container.Snake)
            {
                snakeBodyPartView.gameObject.SetActive(false);
                Pool.ReturnInstance(snakeBodyPartView.gameObject);
            }

            Container.Clear();
        }
    }
}