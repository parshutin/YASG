using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

namespace Assets.Scripts.UserData
{
    public class ScoreSerializer
    {
        private const string FilePath = "/Gamedata/Scores.xml";

        private XmlSerializer _serializer = new XmlSerializer(typeof(ScoreTable));

        public void WriteScores(ScoreTable table)
        {
            using (var stream = new FileStream(Application.dataPath + FilePath, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                _serializer.Serialize(stream, table);
                stream.Close();
            }
        }

        public ScoreTable ReadScores()
        {
            ScoreTable table = null;
            using (var stream = new FileStream(Application.dataPath + FilePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
            {
                if (stream.Length > 0)
                {
                    table = (ScoreTable)_serializer.Deserialize(stream);
                }

                stream.Close();
            }

            return table;
        }
    }
}
