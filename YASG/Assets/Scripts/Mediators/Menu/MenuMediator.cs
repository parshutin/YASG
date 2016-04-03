using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Commands.Menu;
using Assets.Scripts.Helpers;
using Assets.Scripts.Views.Menu;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.Scripts.Mediators.Menu
{
    public class MenuMediator : EventMediator
    {
        [Inject]
        public MenuView View { get; set; }

        [Inject]
        public ShowMenuSignal ShowMenuSignal { get; set; }

        [Inject]
        public OpenScoresSignal OpenScoresSignal { get; set; }

        [Inject]
        public SaveHigscoresSignal SaveHigscoresSignal { get; set; }

        public override void OnRegister()
        {
            View.ChangeCameraButton.onClick.AddListener(ChangeCameraMode);
            View.ExitGameButton.onClick.AddListener(ExitGame);
            View.StartGameButton.onClick.AddListener(StartGame);
            View.ShowScoresButton.onClick.AddListener(ShowScores);
            ShowMenuSignal.AddListener(Show);
        }

        public override void OnRemove()
        {
            View.ChangeCameraButton.onClick.RemoveListener(ChangeCameraMode);
            View.ExitGameButton.onClick.RemoveListener(ExitGame);
            View.StartGameButton.onClick.RemoveListener(StartGame);
            View.ShowScoresButton.onClick.RemoveListener(ShowScores);
            ShowMenuSignal.RemoveListener(Show);
        }

        private void StartGame()
        {
            Application.LoadLevel(1);
        }

        private void ExitGame()
        {
            SaveHigscoresSignal.Dispatch();
            Application.Quit();
        }

        private void ChangeCameraMode()
        {
            SettingsHelper.IsOrtographicCamera = !SettingsHelper.IsOrtographicCamera;
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void ShowScores()
        {
            Hide();
            OpenScoresSignal.Dispatch();
        }
    }
}
