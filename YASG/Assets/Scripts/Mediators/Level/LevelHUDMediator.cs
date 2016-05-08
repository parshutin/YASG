using Assets.Scripts.Commands.Level;
using Assets.Scripts.Views.Level;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.Scripts.Mediators.Level
{
    public class LevelHUDMediator : EventMediator
    {
        [Inject]
        public LevelHUDView View { get; set; }

        [Inject]
        public SnakeLengthChangedSignal SnakeLengthChangedSignal { get; set; }

        [Inject]
        public LifesCountChangedSignal LifesCountChangedSignal { get; set; }

        [Inject]
        public ScoreChangedSignal ScoreChangedSignal { get; set; }

        public override void OnRegister()
        {
            SnakeLengthChangedSignal.AddListener(View.ChangeSnakeLength);
            ScoreChangedSignal.AddListener(View.ChangeScore);
            LifesCountChangedSignal.AddListener(View.ChangeLifesCount);
            View.ExitButton.onClick.AddListener(OnExitButtonClicked);
        }

        private void OnExitButtonClicked()
        {
            Application.LoadLevel(0);
        }

        public override void OnRemove()
        {
            SnakeLengthChangedSignal.RemoveListener(View.ChangeSnakeLength);
            ScoreChangedSignal.RemoveListener(View.ChangeScore);
            LifesCountChangedSignal.RemoveListener(View.ChangeLifesCount);
            View.ExitButton.onClick.RemoveListener(OnExitButtonClicked);
        }
    }
}
