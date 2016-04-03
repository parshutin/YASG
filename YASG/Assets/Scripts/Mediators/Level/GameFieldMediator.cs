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
        //private Timer _timer;

        public override void OnRegister()
        {
            Field.CreateBodyPart += FieldOnCreateBodyPart;
            Field.FoodIted += OnFoodIted;
        }

        private void OnFoodIted()
        {
        }

        private void FieldOnCreateBodyPart(Cell cell)
        {
            CreateSnakeBodyPartSignal.Dispatch(cell, View.transform);
        }

        public override void OnRemove()
        {
            Field.CreateBodyPart -= FieldOnCreateBodyPart;
            Field.FoodIted -= OnFoodIted;
        }

        private void Start()
        {
            GenerateFieldSignal.Dispatch(View.transform);
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
            
        }
    }
}
