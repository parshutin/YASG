using Assets.Scripts.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Assets.Scripts.UserData;
using GDataDB;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField]
        private ScoresPanel _scoresPanel;

        [SerializeField]
        private GameObject _mainMenuPanel;

        private void Awake()
        {
            _scoresPanel.ClosePanel += ScoresPanelOnClosePanel;
        }

        private void ScoresPanelOnClosePanel()
        {
            _mainMenuPanel.gameObject.SetActive(true);
        }

        public void StartGame()
        {
            Application.LoadLevel(1);
        }

        public void ExitGame()
        {
            var dbHelper = new GDadaDbHelper();
            dbHelper.SaveHighscoresTable();
            Application.Quit();
        }

        public void ChangeCameraMode()
        {
            SettingsHelper.IsOrtographicCamera = !SettingsHelper.IsOrtographicCamera;
        }

        public void ShowScores()
        {
             _mainMenuPanel.gameObject.SetActive(false);
             _scoresPanel.Init();
        }

        
    }
}
