using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Core;
using Assets.Scripts.Helpers;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class GameController : MonoBehaviour
    {
        private Field _gameField;

        private Player _player;

        [SerializeField]
        private GameFieldManager _gameFieldManager;

        [SerializeField]
        private HudPanel _hudPanel;

        [SerializeField]
        private GameObject _gameOverPopup;

        [SerializeField]
        private SaveScorePopup _saveScorePopup;

        [SerializeField]
        private CameraController _cameraController;

        [SerializeField]
        private AudioController _audioController;

        private void Awake()
        {
            //System.Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes"); <- добавить это если будет юзаться на айфоне для декодинга левела
            _saveScorePopup.Close += SaveScorePopupOnClose;
            _gameField = new Field();
            _gameFieldManager.Init(_gameField);
            _player = new Player(_gameField);
            _player.OnLifesCountChange += Player_OnLifesCountChange;
            _player.OnScoreChange += Player_OnScoreChange;
            _player.OnSnakeLengthChange += Player_OnSnakeLengthChange;
            _hudPanel.ChangeSnakeLength(_gameField.Snake.Count);
            _cameraController.Init(_gameFieldManager.SnakeHead);
            _audioController.PlayMusic();
            _gameFieldManager.Play();
        }

        private void SaveScorePopupOnClose()
        {
            _gameOverPopup.SetActive(true);
        }

        private void Player_OnSnakeLengthChange(int length)
        {
            _hudPanel.ChangeSnakeLength(length);
        }

        private void Player_OnScoreChange(int score)
        {
            _audioController.PlayFoodItedSound();
            _hudPanel.ChangeScore(score);
        }

        private void Player_OnLifesCountChange(int count)
        {
            _hudPanel.ChangeLifesCount(count);
            if (count == 0)
            {
                _saveScorePopup.Init(_player.Score);
                _gameFieldManager.Stop();
                _cameraController.StopTrack();
            }
            else
            {
                _audioController.PlayLifeLosedSound();
                _gameFieldManager.Restart();
                _cameraController.Init(_gameFieldManager.SnakeHead);
            }
        }

        public void Restart()
        {
            _gameOverPopup.SetActive(false);
            _hudPanel.ChangeLifesCount(3);
            _hudPanel.ChangeScore(0);
            _hudPanel.ChangeSnakeLength(4);
            _player.Init();
            _gameFieldManager.Restart();
        }

        public void Exit()
        {
            _saveScorePopup.Close -= SaveScorePopupOnClose;
            Application.LoadLevel(0);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                _cameraController.ChangeCameraType();
            }
        }
    }
}
