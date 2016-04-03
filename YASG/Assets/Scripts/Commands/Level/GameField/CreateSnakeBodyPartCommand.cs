using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Core;
using Assets.Scripts.Enums;
using Assets.Scripts.Snake;
using Assets.Scripts.Views.Game;
using strange.extensions.command.impl;
using strange.extensions.pool.api;
using UnityEngine;

namespace Assets.Scripts.Commands.Level
{
    public class CreateSnakeBodyPartCommand : Command
    {
        [Inject]
        public SnakeContainer SnakeContainer { get; set; }

        [Inject]
        public Cell Cell { get; set; }

        [Inject]
        public Transform FieldTransform { get; set; }

        [Inject(GameElement.SnakePartsPool)]
        public IPool<GameObject> Pool { get; set; }

        public override void Execute()
        {
            var instance = Pool.GetInstance();
            instance.transform.SetParent(FieldTransform);
            instance.transform.position = new Vector3(Cell.Coorditanes[0], Cell.Coorditanes[1], 20f);
            SnakeContainer.AddSnakeBodyPart(instance.GetComponent<SnakeBodyPartView>());
            /*
            _snake.Add(go.GetComponent<SnakeBodyPart>());
            _snake[_snake.Count - 1].Cell = cell;
            _snake[_snake.Count - 1].Init();
            if (_snake.Count > 4)
            {
                Swap(_snake.Count - 1, _snake.Count - 2);
            }*/
        }
    }
}
