using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using strange.framework.api;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public class ResourcesHelper : IInstanceProvider
    {
        GameObject _prototype;

        private string _resourceName;

        private int _layer;

        public ResourcesHelper(string name)
        {
            _resourceName = name;
        }

        #region IInstanceProvider implementation

        public T GetInstance<T>()
        {
            object instance = GetInstance(typeof(T));
            T retv = (T)instance;
            return retv;
        }

        public object GetInstance(Type key)
        {
            if (_prototype == null)
            {
                _prototype = Resources.Load<GameObject>(_resourceName);
                _prototype.transform.localScale = Vector3.one;
            }

            GameObject go = GameObject.Instantiate(_prototype) as GameObject;

            return go;
        }
        #endregion
    }
}
