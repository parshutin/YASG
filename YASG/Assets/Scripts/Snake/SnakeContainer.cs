using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Views.Game;

namespace Assets.Scripts.Snake
{
    public class SnakeContainer
    {
        private List<SnakeBodyPartView> _snake = new List<SnakeBodyPartView>();

        public List<SnakeBodyPartView> Snake
        {
            get { return _snake; }
        }

        public void AddSnakeBodyPart(SnakeBodyPartView snakeBodyPartView)
        {
            _snake.Add(snakeBodyPartView);
            if (_snake.Count > 4)
            {
                Swap(_snake.Count - 1, _snake.Count - 2);
            }
        }

        private void Swap(int index1, int index2)
        {
            var temp = _snake[index1];
            _snake[index1] = _snake[index2];
            _snake[index2] = temp;
        }

        public void RemoveItem(SnakeBodyPartView view)
        {
            foreach (var partView in _snake)
            {
                if (partView == view)
                {
                    _snake.Remove(view);
                    return;
                }
            }
        }

        public void Clear()
        {
            _snake.Clear();
        }
    }
}
