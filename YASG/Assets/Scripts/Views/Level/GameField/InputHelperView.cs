using System;
using Assets.Scripts.Core;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Assets.Scripts.Views.Level.GameField
{
    public class InputHelperView : View
    {
        public event Action<MovementDirection> DirectionChanged;

        private void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                if (DirectionChanged != null)
                {
                    DirectionChanged(MovementDirection.Down);
                }
                
                return;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                if (DirectionChanged != null)
                {
                    DirectionChanged(MovementDirection.Up);
                }

                return;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                if (DirectionChanged != null)
                {
                    DirectionChanged(MovementDirection.Left);
                }

                return;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                if (DirectionChanged != null)
                {
                    DirectionChanged(MovementDirection.Right);
                }

                return;
            }

            var x = CrossPlatformInputManager.GetAxis("Horizontal");
            var y = CrossPlatformInputManager.GetAxis("Vertical");
            ChangeDirection(x, y);
        }

        private void ChangeDirection(float x, float y)
        {
            if (x == 0f && y == 0f)
            {
                return;
            }

            float absX = Math.Abs(x);
            float absY = Math.Abs(y);

            if (absX > absY)
            {
                if (DirectionChanged != null)
                {
                    DirectionChanged(x > 0 ? MovementDirection.Right : MovementDirection.Left);
                }
            }
            else
            {
                if (DirectionChanged != null)
                {
                    DirectionChanged(y > 0 ? MovementDirection.Up : MovementDirection.Down);
                }
            }
        }
    }
}
