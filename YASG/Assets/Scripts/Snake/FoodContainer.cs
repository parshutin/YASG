using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Views.Level.GameField;

namespace Assets.Scripts.Snake
{
    public class FoodContainer
    {
        private List<FoodView> _food = new List<FoodView>();

        public void AddFood(FoodView food)
        {
            _food.Add(food);
        }

        public FoodView GetFoodView(int[] coordinates)
        {
            foreach (var item in _food)
            {
                if (item.I == coordinates[0] && item.J == coordinates[1])
                {
                    return item;
                }
            }

            return null;
        }

        public void RemoveFoodItem(int[] coordinates)
        {
            foreach (var item in _food)
            {
                if (item.I == coordinates[0] && item.J == coordinates[1])
                {
                    _food.Remove(item);
                    break;
                }
            }
        }
    }
}
