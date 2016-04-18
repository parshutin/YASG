using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Assets.Scripts.Core
{
    public class Field
    {
        private const float SpeadRate = 0.05f;

        private bool _canCreateFood = true;

        private readonly int[] _fieldSize;

        public List<Cell> Cells;

        public List<Cell> Snake;

        private MovementDirection _currentDirection;

        private MovementDirection _previousDirection;

        private Random _randomizer = new Random(DateTime.Now.Millisecond);

        public event Action<Cell> CreateBodyPart;

        public event Action<int[]> FoodIted;

        public event Action LifeLosed;

        public int SizeX
        {
            get { return _fieldSize[0]; }
        }

        public int SizeY { get { return _fieldSize[1]; } }

        public float Speed { get; private set; }

        public bool CanCreateFood
        {
            get { return _canCreateFood; }
        }

        public Field()
        {
            _fieldSize = new[] { 20, 20 };
        }

        public void Initialize()
        {
            FillField();
            CreateBorders();
            CreateSnake();
            Init();
        }

        public Field(int rows, int columns)
        {
            _fieldSize = new[] { rows, columns };
        }

        private void FillField()
        {
            Cells = new List<Cell>();
            for (int i = 0; i < (int) _fieldSize[0]; i++)
            {
                for (int j = 0; j < (int)_fieldSize[1]; j++)
                {
                    Cells.Add(new Cell(i, j));
                }
            }
        }

        private void CreateBorders()
        {
            for (int i = 0; i < _fieldSize[1]; i++)
            {
                this[0,i].Type = CellType.Wall;
            }

            for (int i = 0; i < _fieldSize[1]; i++)
            {
                this[_fieldSize[0] - 1, i].Type = CellType.Wall;
            }

            for (int i = 0; i < _fieldSize[0]; i++)
            {
                this[i, 0].Type = CellType.Wall;
            }

            for (int i = 0; i < _fieldSize[0]; i++)
            {
                this[i, _fieldSize[1] - 1].Type = CellType.Wall;
            }
        }

        public void ChangeMovementDirection(MovementDirection direction)
        {
            if (direction == _currentDirection || (direction == MovementDirection.Down && _currentDirection == MovementDirection.Up) ||
                (direction == MovementDirection.Up && _currentDirection == MovementDirection.Down)
                || (direction == MovementDirection.Right && _currentDirection == MovementDirection.Left) ||
                (direction == MovementDirection.Left && _currentDirection == MovementDirection.Right))
            {
                return;
            }

            _previousDirection = _currentDirection;
            _currentDirection = direction;
        }

        public void Clear()
        {
            foreach (var cell in Cells)
            {
                if (cell.Type != CellType.Wall)
                {
                    cell.Type = CellType.Empty;
                }
            }

            Snake.Clear();
        }

        public void CreateSnake()
        {
            _currentDirection = _previousDirection = MovementDirection.Right;
            Snake = new List<Cell>();
            var head = this[_fieldSize[0]/2, _fieldSize[1]/2];
            head.Type = CellType.Head;
            head.CurrentDirection = head.PreviousDirection = _currentDirection;
            Snake.Add(head);
            CreateBodyPart.Invoke(head);

            var body = this[_fieldSize[0]/2 - 1, _fieldSize[1]/2];
            body.Type = CellType.HorisontalBody;
            body.CurrentDirection = body.PreviousDirection = _currentDirection;
            Snake.Add(body);
            CreateBodyPart.Invoke(body);

            var body1 = this[_fieldSize[0] / 2 - 2, _fieldSize[1] / 2];
            body1.Type = CellType.HorisontalBody;
            body1.CurrentDirection = body1.PreviousDirection = _currentDirection;
            Snake.Add(body1);
            CreateBodyPart.Invoke(body1);

            var tail = this[_fieldSize[0]/2 - 3, _fieldSize[1]/2];
            tail.Type = CellType.Tail;
            tail.CurrentDirection = tail.PreviousDirection = _currentDirection;
            Snake.Add(tail);
            CreateBodyPart.Invoke(tail);
        }

        private void Swap(int index1, int index2)
        {
            var temp = Snake[index1];
            Snake[index1] = Snake[index2];
            Snake[index2] = temp;
        }

        private Index GetNextIndex(int x, int y)
        {
            if (_currentDirection == MovementDirection.Down)
            {
                return new Index(x, y - 1);
            }

            if (_currentDirection == MovementDirection.Left)
            {
                return new Index(x - 1, y);
            }

            if (_currentDirection == MovementDirection.Right)
            {
                return new Index(x + 1, y);
            }

            if (_currentDirection == MovementDirection.Up)
            {
                return new Index(x, y + 1);
            }

            return new Index(-1, -1);
        }

        private CellType GetTurnType(MovementDirection current, MovementDirection previous)
        {
            if ((current == MovementDirection.Down && previous == MovementDirection.Right)
                || (current == MovementDirection.Left && previous == MovementDirection.Up))
            {
                return CellType.TopRightTurn;
            }

            if ((current == MovementDirection.Down && previous == MovementDirection.Left)
                || (current == MovementDirection.Right && previous == MovementDirection.Up))
            {
                return CellType.TopLeftTurn;
            }

            if ((current == MovementDirection.Up && previous == MovementDirection.Right)
                || (current == MovementDirection.Left && previous == MovementDirection.Down))
            {
                return CellType.BottomRightTurn;
            }

            if ((current == MovementDirection.Up && previous == MovementDirection.Left)
                || (current == MovementDirection.Right && previous == MovementDirection.Down))
            {
                return CellType.BottomLeftTurn;
            }

            return CellType.BottomLeftTurn;
        }

        public void MoveSnake()
        {
            Cell cell;
            MovementDirection cellDirection = _currentDirection;
            bool foodIted = false;
            Index previousIndex = new Index(Snake[0].Coorditanes[0], Snake[0].Coorditanes[1]);
            for (int i = 0; i < Snake.Count; i++)
            {
                if (Snake[i].Type == CellType.Head)
                {
                    var nextIndex = GetNextIndex(Snake[0].Coorditanes[0], Snake[0].Coorditanes[1]);
                    cell = this[nextIndex.X, nextIndex.Y];
                    if (cell.Type != CellType.Empty && cell.Type != CellType.Food)
                    {
                        LifeLosed();
                        break;
                    }
                    else if (cell.Type == CellType.Food)
                    {
                        _canCreateFood = true;
                        foodIted = true;
                        FoodIted(cell.Coorditanes);
                    }

                    cell.Type = Snake[i].Type;
                    Snake[i].PreviousDirection = Snake[i].CurrentDirection;
                    Snake[i].CurrentDirection = _currentDirection;
                    cell.CurrentDirection = _currentDirection;
                    Snake[i] = cell;
                }

                if (Snake[i].Type != CellType.Tail && Snake[i].Type != CellType.Head)
                {
                    cell = this[previousIndex.X, previousIndex.Y];
                    previousIndex = new Index(Snake[i].Coorditanes[0], Snake[i].Coorditanes[1]);
                    if (Snake[i - 1].Type == CellType.Head)
                    {
                        cell.Type = GetNextCellType(Snake[i].Type, cell);
                    }

                    Snake[i] = cell;
                }

                if (Snake[i].Type == CellType.Tail)
                {
                    cell = this[previousIndex.X, previousIndex.Y];
                    if (foodIted)
                    {
                        Snake.Add(cell);
                        this.Swap(i, i + 1);

                        CreateBodyPart.Invoke(Snake[i]);
                        break;
                    }
                    
                    var coordinates = Snake[i].Coorditanes;
                    cell.Type = Snake[i].Type;
                    var direction = cell.CurrentDirection;
                    if (tailDirection.TryGetValue(new CellInfo(Snake[i - 1].Type, direction, direction), out direction))
                    {
                        cell.CurrentDirection = direction;
                    }
                    
                    Snake[i] = cell;
                    this[coordinates[0], coordinates[1]].Type = CellType.Empty;
                }
            }
        }

        public Cell GetFoodCell()
        {
            Cell cell;
            do
            {
                cell = this[_randomizer.Next(0, _fieldSize[0]), _randomizer.Next(0, _fieldSize[1])];
            } while (cell.Type != CellType.Empty);

            cell.Type = CellType.Food;
            _canCreateFood = false;
            return cell;
        }

        private void SwapCells(Cell cell1, Cell cell2)
        {
            Cell cell = new Cell(0,0);
            cell.SetData(cell1);
            cell1.SetData(cell2);
            cell2.SetData(cell); 
        }

        private CellType GetNextCellType(CellType type, Cell nextCell)
        {
            var cellInfo = new CellInfo(type, nextCell.CurrentDirection, nextCell.PreviousDirection);
            var result = CellType.Empty;
            if (cellTypes.TryGetValue(cellInfo, out result))
            {
                return result;
            }

            return GetTurnType(nextCell.CurrentDirection, nextCell.PreviousDirection);
        }

        private void Init()
        {
            cellTypes.Add(new CellInfo(CellType.BottomLeftTurn, MovementDirection.Right, MovementDirection.Down), CellType.HorisontalBody);
            cellTypes.Add(new CellInfo(CellType.BottomLeftTurn, MovementDirection.Up, MovementDirection.Left), CellType.VerticalBody);
            
            cellTypes.Add(new CellInfo(CellType.BottomLeftTurn, MovementDirection.Right, MovementDirection.Up), CellType.TopLeftTurn);
            cellTypes.Add(new CellInfo(CellType.BottomLeftTurn, MovementDirection.Left, MovementDirection.Up), CellType.TopRightTurn);
            cellTypes.Add(new CellInfo(CellType.BottomLeftTurn, MovementDirection.Down, MovementDirection.Right), CellType.TopRightTurn);
            cellTypes.Add(new CellInfo(CellType.BottomLeftTurn, MovementDirection.Up, MovementDirection.Right), CellType.BottomRightTurn);


            cellTypes.Add(new CellInfo(CellType.BottomRightTurn, MovementDirection.Left, MovementDirection.Down), CellType.HorisontalBody);
            cellTypes.Add(new CellInfo(CellType.BottomRightTurn, MovementDirection.Up, MovementDirection.Right), CellType.VerticalBody);

            cellTypes.Add(new CellInfo(CellType.BottomRightTurn, MovementDirection.Right, MovementDirection.Up), CellType.TopLeftTurn);
            cellTypes.Add(new CellInfo(CellType.BottomRightTurn, MovementDirection.Left, MovementDirection.Up), CellType.TopRightTurn);
            cellTypes.Add(new CellInfo(CellType.BottomRightTurn, MovementDirection.Down, MovementDirection.Left), CellType.TopLeftTurn);
            cellTypes.Add(new CellInfo(CellType.BottomRightTurn, MovementDirection.Up, MovementDirection.Left), CellType.BottomLeftTurn);


            cellTypes.Add(new CellInfo(CellType.TopLeftTurn, MovementDirection.Down, MovementDirection.Left), CellType.VerticalBody);
            cellTypes.Add(new CellInfo(CellType.TopLeftTurn, MovementDirection.Right, MovementDirection.Up), CellType.HorisontalBody);

            cellTypes.Add(new CellInfo(CellType.TopLeftTurn, MovementDirection.Right, MovementDirection.Down), CellType.BottomLeftTurn);
            cellTypes.Add(new CellInfo(CellType.TopLeftTurn, MovementDirection.Left, MovementDirection.Down), CellType.BottomRightTurn);
            cellTypes.Add(new CellInfo(CellType.TopLeftTurn, MovementDirection.Down, MovementDirection.Right), CellType.TopRightTurn);
            cellTypes.Add(new CellInfo(CellType.TopLeftTurn, MovementDirection.Up, MovementDirection.Right), CellType.BottomRightTurn);

            cellTypes.Add(new CellInfo(CellType.TopRightTurn, MovementDirection.Down, MovementDirection.Right), CellType.VerticalBody);
            cellTypes.Add(new CellInfo(CellType.TopRightTurn, MovementDirection.Left, MovementDirection.Up), CellType.HorisontalBody);


            cellTypes.Add(new CellInfo(CellType.TopRightTurn, MovementDirection.Right, MovementDirection.Down), CellType.BottomLeftTurn);
            cellTypes.Add(new CellInfo(CellType.TopRightTurn, MovementDirection.Left, MovementDirection.Down), CellType.BottomRightTurn);
            cellTypes.Add(new CellInfo(CellType.TopRightTurn, MovementDirection.Down, MovementDirection.Left), CellType.TopLeftTurn);
            cellTypes.Add(new CellInfo(CellType.TopRightTurn, MovementDirection.Up, MovementDirection.Left), CellType.BottomLeftTurn);


            cellTypes.Add(new CellInfo(CellType.BottomLeftTurn, MovementDirection.Up, MovementDirection.Up), CellType.VerticalBody);
            cellTypes.Add(new CellInfo(CellType.BottomLeftTurn, MovementDirection.Down, MovementDirection.Down), CellType.VerticalBody);
            cellTypes.Add(new CellInfo(CellType.BottomLeftTurn, MovementDirection.Right, MovementDirection.Right), CellType.HorisontalBody);
            cellTypes.Add(new CellInfo(CellType.BottomLeftTurn, MovementDirection.Left, MovementDirection.Left), CellType.HorisontalBody);

            cellTypes.Add(new CellInfo(CellType.BottomRightTurn, MovementDirection.Up, MovementDirection.Up), CellType.VerticalBody);
            cellTypes.Add(new CellInfo(CellType.BottomRightTurn, MovementDirection.Down, MovementDirection.Down), CellType.VerticalBody);
            cellTypes.Add(new CellInfo(CellType.BottomRightTurn, MovementDirection.Right, MovementDirection.Right), CellType.HorisontalBody);
            cellTypes.Add(new CellInfo(CellType.BottomRightTurn, MovementDirection.Left, MovementDirection.Left), CellType.HorisontalBody);

            cellTypes.Add(new CellInfo(CellType.TopRightTurn, MovementDirection.Up, MovementDirection.Up), CellType.VerticalBody);
            cellTypes.Add(new CellInfo(CellType.TopRightTurn, MovementDirection.Down, MovementDirection.Down), CellType.VerticalBody);
            cellTypes.Add(new CellInfo(CellType.TopRightTurn, MovementDirection.Right, MovementDirection.Right), CellType.HorisontalBody);
            cellTypes.Add(new CellInfo(CellType.TopRightTurn, MovementDirection.Left, MovementDirection.Left), CellType.HorisontalBody);

            cellTypes.Add(new CellInfo(CellType.TopLeftTurn, MovementDirection.Up, MovementDirection.Up), CellType.VerticalBody);
            cellTypes.Add(new CellInfo(CellType.TopLeftTurn, MovementDirection.Down, MovementDirection.Down), CellType.VerticalBody);
            cellTypes.Add(new CellInfo(CellType.TopLeftTurn, MovementDirection.Right, MovementDirection.Right), CellType.HorisontalBody);
            cellTypes.Add(new CellInfo(CellType.TopLeftTurn, MovementDirection.Left, MovementDirection.Left), CellType.HorisontalBody);

            cellTypes.Add(new CellInfo(CellType.HorisontalBody, MovementDirection.Right, MovementDirection.Right), CellType.HorisontalBody);
            cellTypes.Add(new CellInfo(CellType.HorisontalBody, MovementDirection.Left, MovementDirection.Left), CellType.HorisontalBody);
            cellTypes.Add(new CellInfo(CellType.VerticalBody, MovementDirection.Up, MovementDirection.Up), CellType.VerticalBody);
            cellTypes.Add(new CellInfo(CellType.VerticalBody, MovementDirection.Down, MovementDirection.Down), CellType.VerticalBody);
            //-------------------------------------------------------------------------------------------------------------------------------
            tailDirection.Add(new CellInfo(CellType.BottomLeftTurn, MovementDirection.Right, MovementDirection.Right), MovementDirection.Up);
            tailDirection.Add(new CellInfo(CellType.BottomLeftTurn, MovementDirection.Up, MovementDirection.Up), MovementDirection.Left);
            tailDirection.Add(new CellInfo(CellType.BottomRightTurn, MovementDirection.Left, MovementDirection.Left), MovementDirection.Down);
            tailDirection.Add(new CellInfo(CellType.BottomRightTurn, MovementDirection.Up, MovementDirection.Up), MovementDirection.Right);

            tailDirection.Add(new CellInfo(CellType.TopLeftTurn, MovementDirection.Down, MovementDirection.Down), MovementDirection.Left);
            tailDirection.Add(new CellInfo(CellType.TopLeftTurn, MovementDirection.Right, MovementDirection.Right), MovementDirection.Up);
            tailDirection.Add(new CellInfo(CellType.TopRightTurn, MovementDirection.Down, MovementDirection.Down), MovementDirection.Right);
            tailDirection.Add(new CellInfo(CellType.TopRightTurn, MovementDirection.Left, MovementDirection.Left), MovementDirection.Up);
        }

        private Dictionary<CellInfo, CellType> cellTypes= new Dictionary<CellInfo, CellType>();
        private Dictionary<CellInfo, MovementDirection> tailDirection = new Dictionary<CellInfo, MovementDirection>();

        public struct CellInfo
        {
            public CellInfo(CellType type, MovementDirection direction, MovementDirection previousDirection)
            {
                Type = type;
                Direction = direction;
                PreviousDirection = previousDirection;
            }

            public CellType Type;
            public MovementDirection Direction;
            public MovementDirection PreviousDirection;
        }

        public struct Index
        {
            public Index(int x, int y)
            {
                X = x;
                Y = y;
            }

            public readonly int X;
            public readonly int Y;
        }
         

        private Cell this[int i, int j]
        {
            get
            {
                foreach (var cell in Cells)
                {
                    var coordinates = cell.Coorditanes;
                    if (coordinates[0] == i && coordinates[1] == j)
                    {
                        return cell;
                    }
                }

                return null;
            }
        }
    }
}