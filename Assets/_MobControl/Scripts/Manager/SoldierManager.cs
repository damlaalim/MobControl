using System.Collections.Generic;
using _MobControl.Scripts.Controller;
using _MobControl.Scripts.Data;
using UnityEngine;

namespace _MobControl.Scripts.Manager
{
    public class SoldierManager : MonoBehaviour
    {
        [SerializeField] private List<SoldierData> soldierDataList;

        private BuildManager _buildManager;
        private List<SoldierController> _createdSoldierList = new();

        public void Initialize(BuildManager buildManager)
        {
            _buildManager = buildManager;
        }
        
        public void CreateSoldier(SoldierType soldierType, Vector3 soldierPos, Quaternion soldierRot)
        {
            foreach (var soldierData in soldierDataList)
            {
                if (soldierData.soldierType == soldierType)
                {
                    var newSoldier = Instantiate(soldierData.soldierPrefab);
                    var soldierController = newSoldier.GetComponent<SoldierController>();
                    
                    var targetBuildTransform = _buildManager.GetBuildTransform(soldierData.enemyBuildType);
                    
                    if (targetBuildTransform is null)
                        return;
                    
                    soldierController.Initialize(targetBuildTransform, soldierData, this);
                    soldierController.transform.position = soldierPos;
                    soldierController.transform.rotation = soldierRot;
                    
                    _createdSoldierList.Add(soldierController);
                }
            }
        }

        public void CopySoldier(SoldierController soldier, int gateNumber)
        {
            for (var i = 0; i < gateNumber; i++)
            {
                var newSoldier = Instantiate(soldier);
                var targetBuildTransform = _buildManager.GetBuildTransform(soldier.GetSoldierData.enemyBuildType);
                
                if (targetBuildTransform is null)
                    return;
                
                newSoldier.Initialize(targetBuildTransform, soldier.GetSoldierData, this);
                _createdSoldierList.Add(newSoldier);
                newSoldier.soldierCanReproduce = false;
            }
        }

        public void DestroySoldier(SoldierController soldier)
        {
            _createdSoldierList.Remove(soldier);
            Destroy(soldier.gameObject);
        }

        private void DestroySoldier()
        {
            foreach (var soldier in _createdSoldierList)
            {
                Destroy(soldier.gameObject);
            }    
        }
        
        public void FinishGame()
        {
            DestroySoldier();
        }
    }
}