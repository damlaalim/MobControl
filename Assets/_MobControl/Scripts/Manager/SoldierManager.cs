using System.Collections.Generic;
using _MobControl.Scripts.Controller;
using _MobControl.Scripts.Data;
using UnityEngine;

namespace _MobControl.Scripts.Manager
{
    public class SoldierManager : MonoBehaviour
    {
        [SerializeField] private List<SoldierData> soldierDataList;

        [SerializeField] private GameObject soldierPrefab;

        private BuildManager _buildManager;
        
        public void Initialize(BuildManager buildManager)
        {
            _buildManager = buildManager;
        }
        
        public void CreateSoldier(SoldierType soldierType, Vector3 soldierPos)
        {
            foreach (var soldierData in soldierDataList)
            {
                if (soldierData.soldierType == soldierType)
                {
                    var newSoldier = Instantiate(soldierPrefab);
                    var soldierController = newSoldier.GetComponent<SoldierController>();
                    
                    var targetBuildTransform = _buildManager.GetBuildTransform(soldierData.enemyBuildType);
                    
                    if (targetBuildTransform is null)
                        return;
                    
                    soldierController.Initialize(targetBuildTransform, soldierData);
                    soldierController.transform.position = soldierPos;
                }
            }
        }
    }
}