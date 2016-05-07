using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Core
{
    public class Player
    {
        private const int MaxLifes = 3;

        private const int PointRate = 5;

        public int Lifes { get; private set; }

        public int SnakeLenght { get; private set; }

        public int Score {  get; private set; }

        public event Action<int> OnSnakeLengthChange;

        public event Action<int> OnScoreChange;

        public event Action<int> OnLifesCountChange;

        public void Init(Field gameField)
        {
            //_gameField = gameField;
            Init();
        }

        public void Init()
        {
            Lifes = MaxLifes;
            Score = 0;
            SnakeLenght = 4;
            //SnakeLenght = _gameField.Snake.Count;
        }


        public void AddScore()
        {
            SnakeLenght++;
            Score += PointRate;
        }

        private void GameFieldOnFoodIted(int[] coordinates)
        {
            SnakeLenght++;
            Score += PointRate;
            OnSnakeLengthChange(SnakeLenght);
            OnScoreChange(Score);
        }

        public void RemoveLife()
        {
            Lifes--;
            //SnakeLenght = _gameField.Snake.Count;
            //OnSnakeLengthChange(SnakeLenght);
            if (Lifes == 0)
            {
                //TODO: GameOver
            }

            //OnLifesCountChange(Lifes);
        }

    }
}
