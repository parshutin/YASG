using System;
using System.Collections.Generic;
using Assets.Scripts.Snake;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public class SnakeSpritesHelper : MonoBehaviour
    {
        private Dictionary<SnakePart, Sprite> _bodyPartSprites = new Dictionary<SnakePart, Sprite>();
        
        [SerializeField]
        private List<Sprite> _sprites;

        private void Start()
        {
           // var enymType = typeof (SnakePart);  Enum.GetNames(enymType).Length
            for (int i = 0; i < _sprites.Count; i++)
            {
                _bodyPartSprites.Add((SnakePart)i, _sprites[i]);
            }

            /*_bodyPartSprites.Add(SnakePart.Head, _sprites[0]);
            _bodyPartSprites.Add(SnakePart.Tait, _sprites[1]);
            _bodyPartSprites.Add(SnakePart.Body, _sprites[2]);
            _bodyPartSprites.Add(SnakePart.TurnedBody, _sprites[3]);*/
        }

        public Sprite GetSprite(SnakePart snakePart)
        {
            return _bodyPartSprites[snakePart];
        }
    }
}
