using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Core
{
    [Serializable]
    public class Level
    {
        public int SizeX { get; set; }

        public int SizeY { get; set; }

        public float StartSpeed { get; set; }

        public float DeltaSpeed { get; set; }

        public int[,] Field { get; set; }
    }
}
