using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Core;
using Assets.Scripts.Helpers;
using strange.extensions.command.impl;

namespace Assets.Scripts.Commands.Level
{
    public class StartLevelCommand : Command
    {
        private const float StarterInterval = 0.5f;

        private const float DeltaSpeed = -0.2f;

        [Inject]
        public Timer Timer { get; set; }

        [Inject]
        public Player Player { get; set; }

        [Inject]
        public Field Field { get; set; }

        [Inject]
        public StartGameSignal StartGameSignal { get; set; }

        public override void Execute()
        {
            /*_saveScorePopup.Close += SaveScorePopupOnClose;
            ///StartGameSignal
            _gameField = new Field();
            _gameFieldManager.Init(_gameField);
            _gameFieldManager.Play();
            //
            _player = new Player();
            _player.Init(_gameField);
            _player.OnLifesCountChange += Player_OnLifesCountChange;
            _player.OnScoreChange += Player_OnScoreChange;
            _player.OnSnakeLengthChange += Player_OnSnakeLengthChange;
            _hudPanel.ChangeSnakeLength(_gameField.Snake.Count);
            _cameraController.Init(_gameFieldManager.SnakeHead);
            _audioController.PlayMusic();
            */

            Player.Init(Field);


            StartGameSignal.Dispatch();

            Timer.loop = true;
            Timer.SetDelta(DeltaSpeed);
            Timer.Start(StarterInterval);
        }
    }
}
