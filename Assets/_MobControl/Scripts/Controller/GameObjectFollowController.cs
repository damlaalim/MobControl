using System.Collections;
using _MobControl.Scripts.Manager;
using UnityEngine;

namespace _MobControl.Scripts.Controller
{
    public class GameObjectFollowController : MonoBehaviour
    {
        public Transform target;
        private Camera _mainCamera;

        public void Initialize(GameManager gameManager)
        {
            _mainCamera = Camera.main;
            gameManager.GameIsStart += GameIsStart;
        }

        private void GameIsStart()
        {
            StartCoroutine(FollowTargetRoutine());
        }

        private IEnumerator FollowTargetRoutine()
        {
            Vector3 screenPos;
            while (target is not null)
            {
                screenPos = _mainCamera.WorldToScreenPoint(target.position);
                transform.position = screenPos;
                yield return 0;
            }
        }
    }
}