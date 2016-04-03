using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.UserData;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class ScoreRow : MonoBehaviour
    {
        [SerializeField]
        private Text _score;

        public void Init(UserScore score, int number)
        {
            _score.text = string.Format("{0}. {1} - {2}", number, score.Name, score.Score);
        }
    }
}