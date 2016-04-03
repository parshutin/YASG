using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Commands.Menu;
using Assets.Scripts.UI;
using Assets.Scripts.UserData;
using Assets.Scripts.Views.Menu;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.Scripts.Mediators.Menu
{
    public class ScoresMediator : EventMediator
    {
        private List<ScoreRow> _scores = new List<ScoreRow>();

        [Inject]
        public ScoresView View { get; set; }

        [Inject]
        public InitScoresSignal InitScoresSignal { get; set; }

        [Inject]
        public ClearHigscoresSignal ClearHigscoresSignal { get; set; }

        [Inject]
        public OpenMenuSignal OpenMenuSignal { get; set; }

        public override void OnRegister()
        {
            InitScoresSignal.AddListener(Init);
            View.ClearScores.onClick.AddListener(ClearScores);
            View.ClosePanel.onClick.AddListener(CloseScores);
        }

        public override void OnRemove()
        {
            View.ClearScores.onClick.RemoveListener(ClearScores);
            View.ClosePanel.onClick.RemoveListener(CloseScores);
        }

        private void Init(IEnumerable<UserScore> scores)
        {
            int index = 1;
            foreach (var score in scores)
            {
                CreateScoreRow(score, index);
                index++;
            }

            gameObject.SetActive(true);
        }

        private void CreateScoreRow(UserScore score, int index)
        {
            var go = Instantiate(View.ScorePrefab);
            go.transform.parent = View.ContenTransform.transform;
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y, 0);
            var scoreRow = go.GetComponent<ScoreRow>();
            scoreRow.Init(score, index);
            _scores.Add(scoreRow);
        }

        private void ClearScores()
        {
            ClearObjects();
            ClearHigscoresSignal.Dispatch();
        }

        private void CloseScores()
        {
            gameObject.SetActive(false);
            OpenMenuSignal.Dispatch();
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
