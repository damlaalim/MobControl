using System.Collections.Generic;
using _MobControl.Scripts.Controller;
using _MobControl.Scripts.Data;
using UnityEngine;

namespace _MobControl.Scripts.Manager
{
    public class BuildManager : MonoBehaviour
    {
        [SerializeField] private List<BuildController> buildControllerList;

        public void Initialize(SoldierManager soldierManager, GameManager gameManager, CanvasManager canvasManager)
        {
            foreach (var buildController in buildControllerList)
            {
                buildController.Initialize(soldierManager, gameManager, canvasManager);
            }    
        }
        
        public Transform GetBuildTransform(BuildType buildType)
        {
            foreach (var build in buildControllerList)
            {
                if (build.GetBuildData.type == buildType)
                    return build.transform;
            }

            return null;
        }
    }
}