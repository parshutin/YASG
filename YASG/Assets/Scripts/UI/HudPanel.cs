using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class HudPanel : MonoBehaviour
    {
        [SerializeField]
        private Text _livesCount;

        [SerializeField]
        private Text _snakeLength;

        [SerializeField]
        private Text _score;

        public void ChangeLifesCount(int count)
        {
            Debug.Log(count);
            _livesCount.text = count.ToString();
        }

        public void ChangeSnakeLength(int length)
        {
            _snakeLength.text = length.ToString();
        }

        public void ChangeScore(int score)
        {
            _score.text = score.ToString();
        }
    }
}
