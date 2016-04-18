using System;
using System.Collections.Generic;
using Assets.Scripts.Core;
using Assets.Scripts.Helpers;
using Assets.Scripts.Snake;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Assets.Scripts
{
    public class GameFieldManager : MonoBehaviour
    {
        private Field _field;
        private Timer _timer;
        private List<SnakeBodyPart> _snake = new List<SnakeBodyPart>();
        [SerializeField]
        private GameObject Cell;
        [SerializeField]
        private GameObject Food;
        [SerializeField]
        private GameObject _snakeBodyPart;
        [SerializeField]
        private InputHelper _inputHelper;

        private Dictionary<Cell,GameObject> _cells = new Dictionary<Cell, GameObject>();

        private GameObject _foodGameObject;

        private bool _gameStarted;

        private float _deltaSpeed;

        public Transform SnakeHead
        {
            get { return _snake[0].transform; }
        }

        public int SizeX
        {
            get
            {
                if (_field != null)
                {
                    return _field.SizeX;
                }

                return 0;
            }
        }

        public int SizeY
        {
            get
            {
                if (_field != null)
                {
                    return _field.SizeY;
                }

                return 0;
            }
        }

        public float Speed
        {
            get
            {
                if (_field != null)
                {
                    return _field.Speed;
                }

                return 0;
            }
        }

        public float DeltaSpeed
        {
            get { return _deltaSpeed; }
        }



        public void Play()
        {
            _gameStarted = true;
            if (_timer == null)
            {
                _timer = new Timer();
            }

            _timer.loop = true;
            _deltaSpeed = -0.02f;
            _timer.Start(0.5f);
        }

        public void Init(Field field)
        {
            _field = field;
            _field.CreateBodyPart += FieldOnCreateBodyPart;
            _field.FoodIted += OnFoodIted;
            _field.Initialize();
            GenerateField();
           /* for (int i = 0; i < _field.Snake.Count; i++)
            {
                _snake[i].Cell = _field.Snake[i];
                _snake[i].Init();
            }*/

            //Play();
        }

        public void ChangeEnabledState(bool isEnabled)
        {
            _gameStarted = isEnabled;
            _timer.ChangeEnabledState(isEnabled);
        }

        public void Restart()
        {
            Stop();
            _field.CreateSnake();
            Play();
        }

        public void Stop()
        {
            foreach (var part in _snake)
            {
                Destroy(part.gameObject);
            }

            _snake.Clear();
            _gameStarted = false;
            _timer.Stop();
            _field.Clear();
        }

        private void OnFoodIted(int[] coordinates)
        {
            _timer.ChangeTimeInterval(_deltaSpeed);
            _foodGameObject.SetActive(false);
        }

        private void FieldOnCreateBodyPart(Cell cell)
        {
            var go = Instantiate(_snakeBodyPart);
            _snake.Add(go.GetComponent<SnakeBodyPart>());
            _snake[_snake.Count - 1].Cell = cell;
            _snake[_snake.Count - 1].Init();
            if (_snake.Count > 4)
            {
                Swap(_snake.Count - 1, _snake.Count - 2);
            }
        }

        private void GenerateField()
        {
            foreach (var cell in _field.Cells)
            {
                //GameObject instance = Instantiate(Cell, new Vector3(cell.Coorditanes[0] * 64f, cell.Coorditanes[1] * 64f, 20f), Quaternion.identity) as GameObject;
                GameObject instance = Instantiate(Cell, new Vector3(cell.Coorditanes[0], cell.Coorditanes[1], cell.Type == CellType.Wall ? 20f : 21f), Quaternion.identity) as GameObject;
                _cells.Add(cell,instance);
               /* var renderer = instance.GetComponent<SpriteRenderer>();
                renderer.sprite =
                    _fieldSpritesHelper.GetSprite(cell.Type == CellType.Wall ? CellType.Wall : CellType.Empty);*/
                instance.transform.SetParent(transform);
            }
        }

        private void Update()
        {
            if (_gameStarted)
            {
                CheckField();
               // _gameStarted = false;
               // _timer.Stop();
            }
        }

        private void Start()
        {
            _inputHelper.DirectionChanged += OnDirectionChanged;
        }

        private void OnDirectionChanged(MovementDirection movementDirection)
        {
            _field.ChangeMovementDirection(movementDirection);
        }

        private void CheckField()
        {
            if (_timer != null)
            {
                if (_timer.Update(Time.deltaTime))
                {
                    if (_field.CanCreateFood)
                    {
                        CreateFood();
                    }
                    
                    _field.MoveSnake();
                    for (int i = 0; i < _snake.Count; i++)
                    {
                        _snake[i].Cell = _field.Snake[i];
                        _snake[i].Move();
                    }
                }
            }
        }

        private void CreateFood()
        {
            var cell = _field.GetFoodCell();
            if (_foodGameObject == null)
            {
                //_foodGameObject = Instantiate(Food, new Vector3(cell.Coorditanes[0]*64f, cell.Coorditanes[1]*64f, 1f), Quaternion.identity) as GameObject;
                _foodGameObject = Instantiate(Food, new Vector3(cell.Coorditanes[0], cell.Coorditanes[1], 20f), Quaternion.identity) as GameObject;
                _foodGameObject.name = "Food";
                //var renderer = _foodGameObject.GetComponent<SpriteRenderer>();
                //renderer.sprite = _fieldSpritesHelper.GetSprite(CellType.Food);
            }
            else
            {
                //_foodGameObject.transform.position = new Vector3(cell.Coorditanes[0]*64f, cell.Coorditanes[1]*64f, 1f);
                _foodGameObject.transform.position = new Vector3(cell.Coorditanes[0], cell.Coorditanes[1], 20f);
                _foodGameObject.SetActive(true);
            }
        }

        private void Swap(int index1, int index2)
        {
            var temp = _snake[index1];
            _snake[index1] = _snake[index2];
            _snake[index2] = temp;
        }
    }
}