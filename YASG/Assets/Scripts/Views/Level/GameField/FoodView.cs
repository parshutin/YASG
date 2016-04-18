using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using strange.extensions.mediation.impl;

namespace Assets.Scripts.Views.Level.GameField
{
    public class FoodView : View
    {
        public int I { get; private set; }
        public int J { get; private set; }

        public void Init(int i, int j)
        {
            I = i;
            J = j;
        }
    }
}
