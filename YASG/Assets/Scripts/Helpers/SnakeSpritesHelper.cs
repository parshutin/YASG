using System;
using System.Collections.Generic;
using Assets.Scripts.Core;
using Assets.Scripts.Snake;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public class SnakeSpritesHelper : MonoBehaviour
    {
        private static SnakeSpritesHelper _instance;

        private Dictionary<CellType, Sprite> _bodyPartSprites = new Dictionary<CellType, Sprite>();
        
        [SerializeField]
        private List<Sprite> _sprites;

        [SerializeField]
        private GameObject _gameField;

        public static SnakeSpritesHelper Instance
        {
            get { return _instance; }
        }

        private void Awake()
        {
            if (Instance != null)
            {
                return;
            }

            _instance = this;
           // var enymType = typeof (SnakePart);  Enum.GetNames(enymType).Length
            /*for (int i = 0; i < _sprites.Count; i++)
            {
                _bodyPartSprites.Add((CellType)i, _sprites[i]);
            }*/

            _bodyPartSprites.Add(CellType.Head, _sprites[0]);
            _bodyPartSprites.Add(CellType.Tail, _sprites[1]);
            _bodyPartSprites.Add(CellType.HorisontalBody, _sprites[2]);
            _bodyPartSprites.Add(CellType.VerticalBody, _sprites[3]);
            _bodyPartSprites.Add(CellType.BottomLeftTurn, _sprites[4]);
            _bodyPartSprites.Add(CellType.BottomRightTurn, _sprites[5]);
            _bodyPartSprites.Add(CellType.TopLeftTurn, _sprites[6]);
            _bodyPartSprites.Add(CellType.TopRightTurn, _sprites[7]);
           // _gameField.SetActive(true);
        }

        public Sprite GetSprite(CellType type)
        {
            return _bodyPartSprites[type];
        }
    }
}
