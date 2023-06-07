using System.Collections.Generic;
using UnityEngine;

namespace _MobControl.Scripts.Data
{
    [CreateAssetMenu(fileName = "SoldierData", menuName = "Data/Soldier", order = 1)]
    public class SoldierData : ScriptableObject
    {
        public SoldierType soldierType;
        public List<SoldierType> partisanSoldierList;
        public BuildType enemyBuildType;
        public float speed;
        public int damageNumber;
        public int hp;
        public Material material;
        public bool canReproduce;
    }
}