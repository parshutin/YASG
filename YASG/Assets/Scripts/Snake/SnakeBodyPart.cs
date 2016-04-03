using System.Collections;
using Assets.Scripts.Core;
using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts.Snake
{
    public class SnakeBodyPart : MonoBehaviour
    {
        public Transform GameField;

        //public SnakeSpritesHelper _spritesHelper;

        public Cell Cell;

        private int[] _coordinates;

        private CellType _cellType;

        public SpriteRenderer SpriteRenderer;

        private Vector3 _destinition;

        private Vector3 _start;

        public int I;
        public int J;

        public bool IsMoving { get; set; }

        public void Init()
        {
            _coordinates = Cell.Coorditanes;
            ChangeSprite();
            transform.SetParent(GameField);
            transform.position = new Vector3(_coordinates[0], _coordinates[1] , 20f);
            
            //Rotate();
        }

        public void ChangeSprite()
        {
            if (_cellType != Cell.Type)
            {
                //SpriteRenderer.sprite = SnakeSpritesHelper.Instance.GetSprite(Cell.Type);
                _cellType = Cell.Type;
            }
        }

        private void Rotate()
        {
            if (_cellType == CellType.Head)
            {
                if (Cell.CurrentDirection == MovementDirection.Right)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 90f);
                }
                else if (Cell.CurrentDirection == MovementDirection.Left)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 270f);
                }
                else if (Cell.CurrentDirection == MovementDirection.Up)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 180f);
                }
                else if (Cell.CurrentDirection == MovementDirection.Down)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }

            if (_cellType == CellType.Tail)
            {
                if (Cell.CurrentDirection == MovementDirection.Right)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 180f);
                }
                else if (Cell.CurrentDirection == MovementDirection.Left)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else if (Cell.CurrentDirection == MovementDirection.Up)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 270f);
                }
                else if (Cell.CurrentDirection == MovementDirection.Down)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 90f);
                }
            }
        }

        public void Move()
        {
            I = Cell.Coorditanes[0];
            J = Cell.Coorditanes[1];
            var newCoordinates = Cell.Coorditanes;
            /*  _start = transform.position;
              _destinition = new Vector3(transform.position.x + GetNextValue(_coordinates[0], newCoordinates[0]),
                  transform.position.y + GetNextValue(_coordinates[1], newCoordinates[1]), 1f);*/
            /*  transform.position = new Vector3(transform.position.x + GetNextValue(_coordinates[0], newCoordinates[0]),
                  transform.position.y + GetNextValue(_coordinates[1], newCoordinates[1]), 1f);*/


            /*  if (Cell.Type == CellType.Head || Cell.Type == CellType.HorisontalBody || Cell.Type == CellType.Tail || Cell.Type == CellType.VerticalBody)
              {
                  _start = transform.position;
                  _destinition = new Vector3(transform.position.x + GetNextValue(_coordinates[0], newCoordinates[0]),
                      transform.position.y + GetNextValue(_coordinates[1], newCoordinates[1]), 1f);
                  IsMoving = true;
              }
              else
              {
                  IsMoving = false;
                  transform.position = new Vector3(transform.position.x + GetNextValue(_coordinates[0], newCoordinates[0]),
                  transform.position.y + GetNextValue(_coordinates[1], newCoordinates[1]), 1f);
              }*/

            //IsMoving = false;
           // transform.position = new Vector3(Cell.Coorditanes[0], Cell.Coorditanes[1], 20f);

                _start = transform.position;
                _destinition = new Vector3(Cell.Coorditanes[0], Cell.Coorditanes[1], 20f);                
                IsMoving = true;
            _coordinates = Cell.Coorditanes;
            //Rotate();
            //ChangeSprite();
        }

        IEnumerator Move(Vector3 startPos, Vector3 endPos, float time)
        {
            IsMoving = false;
            var i = 0.0f;
            var rate = 1.0f / time;
            while (i < 1.0f)
            {
                i += Time.deltaTime * rate;
                transform.position = Vector3.Lerp(startPos, endPos, i);
                yield return null;
            }

            //ChangeSprite();
        }

        private float GetNextValue(float a, float b)
        {
            if (a < b)
            {
                return 64f;
            }

            if(a > b)
            {
                return -64f;
            }

            return 0;
        }

        private void Update()
        {
            if (IsMoving)
            {
                StartCoroutine(Move(_start, _destinition, 0.5f));
            }
        }
    }
}
