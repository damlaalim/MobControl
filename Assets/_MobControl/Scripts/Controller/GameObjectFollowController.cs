using System.Collections;
using UnityEngine;

namespace _MobControl.Scripts.Controller
{
    public class GameObjectFollowController : MonoBehaviour
    {
        [SerializeField] private Transform target;
        private Camera _mainCamera;

        public void Initialize()
        {
            _mainCamera = Camera.main;
            StartCoroutine(FollowTargetRoutine());
        }

        private IEnumerator FollowTargetRoutine()
        {
            Vector3 screenPos;
            while (true)
            {
                screenPos = _mainCamera.WorldToScreenPoint(target.position);
                transform.position = screenPos;
                yield return 0;
            }
        }
    }
}