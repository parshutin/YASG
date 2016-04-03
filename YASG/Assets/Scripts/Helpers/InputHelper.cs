using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Assets.Scripts.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

namespace Assets.Scripts.Helpers
{
    public class InputHelper : MonoBehaviour
    {
        public event Action<MovementDirection> DirectionChanged; 

        private void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                DirectionChanged(MovementDirection.Down);
                return;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                DirectionChanged(MovementDirection.Up);
                return;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                DirectionChanged(MovementDirection.Left);
                return;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                DirectionChanged(MovementDirection.Right);
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
                DirectionChanged(x > 0 ? MovementDirection.Right : MovementDirection.Left);
            }
            else
            {
                DirectionChanged(y > 0 ? MovementDirection.Up : MovementDirection.Down);
            }
        }
    }
}
