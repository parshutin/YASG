using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Assets.Scripts.UserData;
using GDataDB;
using GDataDB.Impl;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public class GDadaDbHelper
    {
        private const string Email = "625927765290-compute@developer.gserviceaccount.com";

        private const string KeyPath = "/Gamedata/key.p12";

        private const string DbFileName = "test_table";

        private const string TableName = "Scores";

        private DatabaseClient _databaseClient;

        private IDatabase _database;

        private ITable<UserScore> _scoresTable;
        
        public GDadaDbHelper()
        {
            ServicePointManager.ServerCertificateValidationCallback = RemoteCertificateValidationCallback;
            _databaseClient = new DatabaseClient(Email,
                File.ReadAllBytes(Application.dataPath + KeyPath));
            _database = _databaseClient.GetDatabase("test_table") ?? _databaseClient.CreateDatabase("test_table");
            _scoresTable = _database.GetTable<UserScore>("Scores") ?? _database.CreateTable<UserScore>("Scores");
        }

        public void SaveHighscoresTable()
        {
            var localScores = ScoresManager.Instance.Scores;
            var globalScores = _scoresTable.FindAll();
            var scoresTable = new ScoreTable();
            foreach (var score  in globalScores)
            {
                scoresTable.AddNew(score.Element);
            }

            for (int i = 0; i < localScores.Count; i++)
            {
                scoresTable.AddNew(localScores[i]);
            }

            for (int i = 0; i < scoresTable.Scores.Count; i++)
            {
                if (i >= globalScores.Count)
                {
                    _scoresTable.Add(scoresTable.Scores[i]);
                    continue;
                }

                if (globalScores[i].Element.Score < scoresTable.Scores[i].Score)
                {
                    globalScores[i].Element = scoresTable.Scores[i];
                    globalScores[i].Update();
                }
            }
        }


        private bool RemoteCertificateValidationCallback(System.Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            bool isOk = true;
            // If there are errors in the certificate chain, look at each error to determine the cause.
            if (sslPolicyErrors != SslPolicyErrors.None)
            {
                for (int i = 0; i < chain.ChainStatus.Length; i++)
                {
                    if (chain.ChainStatus[i].Status != X509ChainStatusFlags.RevocationStatusUnknown)
                    {
                        chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
                        chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                        chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 1, 0);
                        chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;
                        bool chainIsValid = chain.Build((X509Certificate2)certificate);
                        if (!chainIsValid)
                        {
                            isOk = false;
                        }
                    }
                }
            }
            return isOk;
        }
    }
}
