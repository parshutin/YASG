using System;
using System.Collections.Generic;
using Assets.Scripts.UserData;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ScoresPanel: MonoBehaviour
    {
        public event Action ClosePanel;

        [SerializeField]
        private GameObject _scorePrefab;

        [SerializeField]
        private Transform _contenTransform;

        private List<ScoreRow> _scores = new List<ScoreRow>();

        public void Init()
        {
            var scores = ScoresManager.Instance.Scores;
            for (int i = 0; i < scores.Count; i++)
            {
                CreateScoreRow(scores[i]);
            }

            gameObject.SetActive(true);
        }

        private void CreateScoreRow(UserScore score)
        {
            var go = Instantiate(_scorePrefab);
            go.transform.parent = _contenTransform;
            var scoreRow = go.GetComponent<ScoreRow>();
            scoreRow.Init(score, _scores.Count + 1);
            _scores.Add(scoreRow);
        }

        public void Close()
        {
            if (ClosePanel != null)
            {
                gameObject.SetActive(false);
                ClearObjects();
                ClosePanel();
            }
        }

        public void Clear()
        {
            ScoresManager.Instance.Clear();
            ClearObjects();
        }

        private void ClearObjects()
        {
            foreach (var score in _scores)
            {
                Destroy(score.gameObject);
            }

            _scores.Clear();
        }
    }
}