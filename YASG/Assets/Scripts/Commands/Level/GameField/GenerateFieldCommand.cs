using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Core;
using Assets.Scripts.Enums;
using strange.extensions.command.impl;
using strange.extensions.pool.api;
using UnityEngine;

namespace Assets.Scripts.Commands.Level
{
    public class GenerateFieldCommand : Command
    {
        [Inject]
        public Field Field { get; set; }

        [Inject]
        public Transform GameField { get; set; }

        [Inject(GameElement.WallPool)]
        public IPool<GameObject> Pool { get; set; }

        public override void Execute()
        {
            Field.Initialize();
            foreach (var cell in Field.Cells)
            {
                GameObject instance = Pool.GetInstance();
                instance.transform.localPosition = new Vector3(cell.Coorditanes[0], cell.Coorditanes[1],
                    cell.Type == CellType.Wall ? 20f : 21f);
                instance.transform.SetParent(GameField);
            }
        }
    }
}
