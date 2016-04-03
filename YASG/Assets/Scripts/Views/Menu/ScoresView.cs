using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.UI;
using Assets.Scripts.UserData;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Views.Menu
{
    public class ScoresView : View
    {
        public GameObject ScorePrefab;

        public Transform ContenTransform;

        public Button ClearScores;

        public Button ClosePanel;
    }
}
