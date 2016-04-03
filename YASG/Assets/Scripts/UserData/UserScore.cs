using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.UserData
{
    public class UserScore : IComparable<UserScore>
    {
        public string Name { get; set; }

        public int Score { get; set; }

        public int CompareTo(UserScore other)
        {
            if (Score > other.Score)
            {
                return -1;
            }

            if (Score < other.Score)
            {
                return 1;
            }

            return 0;
        }
    }
}