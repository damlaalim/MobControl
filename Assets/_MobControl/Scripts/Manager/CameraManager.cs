using System.Collections.Generic;
using _MobControl.Scripts.Controller;
using UnityEngine;

namespace _MobControl.Scripts.Manager
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private List<GameObjectFollowController> followControllerList;

        public void Initialize()
        {
            foreach (var followController in followControllerList)
            {
                followController.Initialize();
            }
        }
    }
}