using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Helpers;
using Assets.Scripts.UserData;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class SaveScorePopup : MonoBehaviour
    {
        [SerializeField]
        private InputField _userName;

        public event Action Close;

        private int _score;

        public void Init(int score)
        {
            _score = score;
            gameObject.SetActive(true);
        }

        public void SaveScore()
        {
            ScoresManager.Instance.SaveScore(new UserScore {Name = _userName.text, Score = _score});
            if (Close != null)
            {
                gameObject.SetActive(false);
                Close();
            }
        }

        private void Update()
        {
            if (_userName.text.Length > 10)
            {
                _userName.text = _userName.text.Substring(0, 9);
            }
        }
    }
}
