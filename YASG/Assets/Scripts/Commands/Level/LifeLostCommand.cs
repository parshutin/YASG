using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Core;
using strange.extensions.command.impl;

namespace Assets.Scripts.Commands.Level
{
    public class LifeLostCommand : Command
    {
        [Inject]
        public Player Player { get; set; }

        [Inject]
        public SnakeLengthChangedSignal SnakeLengthChangedSignal { get; set; }

        [Inject]
        public LifesCountChangedSignal LifesCountChangedSignal { get; set; }

        [Inject]
        public RestartGameSignal RestartGameSignal { get; set; }

        [Inject]
        public StopGameSignal StopGameSignal { get; set; }

        public override void Execute()
        {
            Player.RemoveLife();
            SnakeLengthChangedSignal.Dispatch(Player.SnakeLenght);
            LifesCountChangedSignal.Dispatch(Player.Lifes);

            if (Player.Lifes == 0)
            {
                StopGameSignal.Dispatch();
            }
            else
            {
                RestartGameSignal.Dispatch();
            }

            /*
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
            }*/
        } 
    }
}
