using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Views.Level.UI;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.Scripts.Mediators.Level
{
    public class GameOverPopupMediator : EventMediator
    {
        [Inject]
        public GameOverPopupView View { get; set; }

        public override void OnRegister()
        {
            View.ExitButton.onClick.AddListener(OnExitClicked);
            View.RestartButton.onClick.AddListener(OnRestartClicked);
        }

        private void OnExitClicked()
        {
            Application.LoadLevel(0);
        }

        private void OnRestartClicked()
        {
            /*
            _gameOverPopup.SetActive(false);
            _hudPanel.ChangeLifesCount(3);
            _hudPanel.ChangeScore(0);
            _hudPanel.ChangeSnakeLength(4);
            _player.Init();
            _gameFieldManager.Restart();*/
        }

        public override void OnRemove()
        {
            View.ExitButton.onClick.RemoveListener(OnExitClicked);
            View.RestartButton.onClick.RemoveListener(OnRestartClicked);
        }
    }
}
