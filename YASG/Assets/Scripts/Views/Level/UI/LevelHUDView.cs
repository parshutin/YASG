using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Views.Level
{
    public class LevelHUDView : View
    {
        [SerializeField]
        private Text _livesCount;

        [SerializeField]
        private Text _snakeLength;

        [SerializeField]
        private Text _score;

        public Button ExitButton;

        public void ChangeLifesCount(int count)
        {
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
