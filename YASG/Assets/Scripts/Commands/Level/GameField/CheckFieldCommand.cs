using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Core;
using Assets.Scripts.Helpers;
using Assets.Scripts.Snake;
using strange.extensions.command.impl;
using UnityEngine;

namespace Assets.Scripts.Commands.Level.GameField
{
    public class CheckFieldCommand : Command
    {
        [Inject]
        public Field Field { get; set; }

        [Inject]
        public Timer Timer { get; set; }

        [Inject]
        public SnakeContainer SnakeContainer { get; set; }

        public override void Execute()
        {
            if (Timer.Update(Time.deltaTime))
            {
                if (Field.CanCreateFood)
                {
                    //CreateFood();
                }


                Field.MoveSnake();
                for (int i = 0; i < SnakeContainer.Snake.Count; i++)
                {
                    SnakeContainer.Snake[i].Move(Field.Snake[i]);
                }
            }

        }
    }
}
