using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Assets.Scripts.Helpers;
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Managers
{
    public class CameraController : MonoBehaviour
    {
        private Vector3 _ortographicQuaternionVector = new Vector3(360f, 0f, 0f);

        private Vector3 _perspectiveQuaternionVector = new Vector3(330f, 0f, 0f);

        private Vector3 _ortographicCameraPosition = new Vector3(10f, 10.5f, -10f);

        private const float TimeValue = 0.5f;

        [SerializeField]
        private Camera _camera;

        private bool _isOrtographic;

        private bool _canFollow;

        private bool _isMoving;

        private Transform _snake;

        public Vector2 maxXAndY = new Vector2(12f, 9f);
        public Vector2 minXAndY = new Vector2(5f, -2f);

        public void Init(Transform snakeTransform)
        {
            _snake = snakeTransform;
             _isOrtographic = SettingsHelper.IsOrtographicCamera;
             //if (_isOrtographic)
             //{
                 _camera.transform.position = _ortographicCameraPosition;
                 _camera.transform.rotation = Quaternion.Euler(_ortographicQuaternionVector);
             /*}
             else
             {
                 _camera.transform.position = new Vector3(_snake.position.x - 2, _snake.position.y - 4, 10f);
                 _camera.transform.rotation = Quaternion.Euler(_perspectiveQuaternionVector);
                _canFollow = true;
            }*/
        }

          private void LateUpdate()
          {
            /*  if (!_isMoving && _canFollow)
              {
                 TrackHead();
              }*/
          }

        private void TrackHead()
        {
            if (!_isMoving)
            {
                float targetX = _snake.position.x - 2;
                float targetY = _snake.position.y - 4;
                targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
                targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);
                _camera.transform.position = new Vector3(targetX, targetY, 10f);
            }
        }

        public void StopTrack()
        {
            _canFollow = false;
        }

        public void ChangeCameraType()
        {
            _isMoving = true;
            _canFollow = false;
            if (_isOrtographic)
            {
                SetPerspectiveState();
            }
            else
            {
                SetOrtographicState();
            }

            _isOrtographic = !_isOrtographic;
            SettingsHelper.IsOrtographicCamera = _isOrtographic;
        }

        private void SetOrtographicState()
        {
            var pos = new Vector3(_camera.transform.position.x, _camera.transform.position.y, _camera.transform.position.z);
            StartCoroutine(Move(pos,
                    _ortographicCameraPosition, _perspectiveQuaternionVector, _ortographicQuaternionVector, false));
        }

        public void SetPerspectiveState()
        {
            var pos = new Vector3(_camera.transform.position.x, _camera.transform.position.y, _camera.transform.position.z);
            StartCoroutine(Move(pos,
                     new Vector3(_snake.position.x - 2, _snake.position.y - 4, 10f), _ortographicQuaternionVector, _perspectiveQuaternionVector, true));
        }

        private IEnumerator Move(Vector3 startPos, Vector3 endPos, Vector3 startQuaternion, Vector3 endQuaternion, bool follow)
        {
            var i = 0.0f;
            var rate = 1.0f / TimeValue;
            while (i < 1.0f)
            {
                i += Time.deltaTime * rate;
                _camera.transform.position = Vector3.Lerp(startPos, endPos, i);
                _camera.transform.rotation = Quaternion.Euler(Vector3.Lerp(startQuaternion, endQuaternion, i));
                yield return null;
            }

            _canFollow = follow;
            _isMoving = false;
        }
    }
}
