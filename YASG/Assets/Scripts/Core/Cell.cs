using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Core
{
    public class Cell
    {
        public CellType Type { get; set; }

        public MovementDirection CurrentDirection { get; set; }

        public MovementDirection PreviousDirection { get; set; }

        public Cell(int x, int y)
        {
            Coorditanes = new[] {x, y};
            Type = CellType.Empty;
        }

        public int[] Coorditanes { get; private set; }

        public void SetData(Cell cell)
        {
            Type = cell.Type;
            CurrentDirection = cell.CurrentDirection;
            PreviousDirection = cell.PreviousDirection;
            Coorditanes[0] = cell.Coorditanes[0];
            Coorditanes[1] = cell.Coorditanes[1];
        }
    }
}
