using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Assets.Scripts.UserData
{
    public class ScoreTable
    {
        [XmlArray("Scores"), XmlArrayItem(typeof(UserScore), ElementName = "UserScore")]
        public List<UserScore> Scores = new List<UserScore>();

        private void Sort()
        {
            Scores.Sort();
        }

        public bool AddNew(UserScore score)
        {
            if (Scores.Count < 10)
            {
                Add(score);
                return true;
            }

            if (Scores[Scores.Count - 1].Score <= score.Score)
            {
                Scores.RemoveAt(Scores.Count - 1);
                Add(score);
                return true;
            }

            return false;
        }

        private void Add(UserScore score)
        {
            Scores.Add(score);
            Sort();
        }
    }
}
