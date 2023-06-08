using System.Collections.Generic;
using _MobControl.Scripts.Controller;
using UnityEngine;

namespace _MobControl.Scripts.Manager
{
    public class ObstacleManager : MonoBehaviour
    {
        [SerializeField] private List<ObstacleController> obstacleList;

        public void Initialize(CanvasManager canvasManager)
        {
            foreach (var obstacle in obstacleList)
            {
                obstacle.Initialize();
            }
        }
    }
}