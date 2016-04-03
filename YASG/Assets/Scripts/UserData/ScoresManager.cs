using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.UserData
{
    public class ScoresManager
    {
        private static ScoresManager _instance;

        private readonly ScoreSerializer _serializer;

        private readonly ScoreTable _scoreTable;

        public List<UserScore> Scores
        {
            get { return _scoreTable.Scores; }
        } 

        public static ScoresManager Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                _instance = new ScoresManager();
                return _instance;
            }
        }

        private ScoresManager()
        {
            _serializer = new ScoreSerializer();
            _scoreTable = _serializer.ReadScores();
            if (_scoreTable == null)
            {
                _scoreTable = new ScoreTable();
            }
        }

        public void SaveScore(UserScore score)
        {
            if (_scoreTable.AddNew(score))
            {
                _serializer.WriteScores(_scoreTable);
            }
        }

        public void Clear()
        {
            _scoreTable.Scores.Clear();
            _serializer.WriteScores(_scoreTable);
        }
    }
}
