using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Core;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.Scripts.Views.Game
{
    public class SnakeBodyPartView : View
    {
        private CellType _cellType;

        private Vector3 _destinition;

        private Vector3 _start;

        public int I;
        public int J;

        public bool IsMoving { get; set; }

        public void Move(Cell cell)
        {
            I = cell.Coorditanes[0];
            J = cell.Coorditanes[1];
            _start = transform.position;
            _destinition = new Vector3(cell.Coorditanes[0], cell.Coorditanes[1], 20f);
            IsMoving = true;
        }

        IEnumerator Move(Vector3 startPos, Vector3 endPos, float time)
        {
            IsMoving = false;
            var i = 0.0f;
            var rate = 1.0f / time;
            while (i < 1.0f)
            {
                i += Time.deltaTime * rate;
                transform.position = Vector3.Lerp(startPos, endPos, i);
                yield return null;
            }
        }

        private void Update()
        {
            if (IsMoving)
            {
                StartCoroutine(Move(_start, _destinition, 0.5f));
            }
        }
    }
}
