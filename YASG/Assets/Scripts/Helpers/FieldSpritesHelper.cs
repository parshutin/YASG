using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public class FieldSpritesHelper : MonoBehaviour
    {
        private Dictionary<CellType, Sprite> _fieldSprites = new Dictionary<CellType, Sprite>();

        [SerializeField]
        private List<Sprite> _sprites;

        private void Awake()
        {
            _fieldSprites.Add(CellType.Empty, _sprites[0]);
            _fieldSprites.Add(CellType.Wall, _sprites[1]);
            _fieldSprites.Add(CellType.Food, _sprites[2]);
        }

        public Sprite GetSprite(CellType type)
        {
            return _fieldSprites[type];
        }
    }
}
