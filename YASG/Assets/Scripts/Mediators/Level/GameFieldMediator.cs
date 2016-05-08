using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Commands.Level;
using Assets.Scripts.Core;
using Assets.Scripts.Views.Level;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.Scripts.Mediators.Level
{
    public class GameFieldMediator : EventMediator
    {
        private bool _gameStarted;

        [Inject]
        public Field Field { get; set; }

        [Inject]
        public GameFieldView View { get; set; }

        [Inject]
        public GenerateFieldSignal GenerateFieldSignal { get; set; }

        [Inject]
        public CreateSnakeBodyPartSignal CreateSnakeBodyPartSignal { get; set; }

        [Inject]
        public CreateFoodSignal CreateFoodSignal { get; set; }

        [Inject]
        public RemoveFoodSignal RemoveFoodSignal { get; set; }

        [Inject]
        public CheckFieldSignal CheckFieldSignal { get; set; }

        [Inject]
        public StartGameSignal StartGameSignal { get; set; }

        [Inject]
        public ChangeTimerIntervalSignal ChangeTimerIntervalSignal { get; set; }

        [Inject]
        public StartLevelSignal StartLevelSignal { get; set; }

        [Inject]
        public LifeLostSignal LisLostSignal { get; set; }

        [Inject]
        public FoodItedSignal FoodItedSignal { get; set; }

        [Inject]
        public StopFieldCheckingSignal StopFieldCheckingSignal { get; set; }

        public override void OnRegister()
        {
            StartGameSignal.AddListener(StartGame);
            Field.LifeLosed += FieldOnLifeLosed;
            Field.CreateBodyPart += FieldOnCreateBodyPart;
            Field.FoodIted += OnFoodIted;
            StopFieldCheckingSignal.AddListener(StopFieldChecking);
            GenerateFieldSignal.Dispatch(View.transform);
            StartLevelSignal.Dispatch();
        }

        private void StopFieldChecking()
        {
            _gameStarted = false;
        }

        private void FieldOnLifeLosed()
        {
            LisLostSignal.Dispatch();
        }

        private void OnFoodIted(int[] coordinates)
        {
            FoodItedSignal.Dispatch();
            RemoveFoodSignal.Dispatch(coordinates);
            ChangeTimerIntervalSignal.Dispatch();
            //Do other stuff with food, like score changed and other
        }

        private void StartGame()
        {
            _gameStarted = true;
        }

        private void FieldOnCreateBodyPart(Cell cell)
        {
            CreateSnakeBodyPartSignal.Dispatch(cell, View.transform);
        }

        public override void OnRemove()
        {
            StartGameSignal.RemoveListener(StartGame);
            StopFieldCheckingSignal.RemoveListener(StopFieldChecking);
            Field.LifeLosed -= FieldOnLifeLosed;
            Field.CreateBodyPart -= FieldOnCreateBodyPart;
            Field.FoodIted -= OnFoodIted;
        }

        private void Start()
        {
            //GenerateFieldSignal.Dispatch(View.transform);
        }

        private void Update()
        {
            if (_gameStarted)
            {
                CheckField();
                // _gameStarted = false;
                // _timer.Stop();
            }
        }

        private void CheckField()
        {
            CheckFieldSignal.Dispatch();
        }
    }
}
