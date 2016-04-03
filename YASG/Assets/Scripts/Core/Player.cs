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

        private readonly Field _gameField;

        public int Lifes { get; private set; }

        public int SnakeLenght { get; private set; }

        public int Score {  get; private set; }

        public event Action<int> OnSnakeLengthChange;

        public event Action<int> OnScoreChange;

        public event Action<int> OnLifesCountChange;

        public Player(Field gameField)
        {
            _gameField = gameField;
            _gameField.FoodIted += GameFieldOnFoodIted;
            _gameField.LifeLosed += RemoveLife;
            Init();
        }

        public void Init()
        {
            Lifes = MaxLifes;
            Score = 0;
            SnakeLenght = _gameField.Snake.Count;
        }

        private void GameFieldOnFoodIted()
        {
            SnakeLenght++;
            Score += PointRate;
            OnSnakeLengthChange(SnakeLenght);
            OnScoreChange(Score);
        }

        public void RemoveLife()
        {
            Lifes--;
            SnakeLenght = _gameField.Snake.Count;
            OnSnakeLengthChange(SnakeLenght);
            if (Lifes == 0)
            {
                //TODO: GameOver
            }

            OnLifesCountChange(Lifes);
        }

    }
}
