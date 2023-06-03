using UnityEngine;

namespace _MobControl.Scripts.Data
{
    [CreateAssetMenu(fileName = "SoldierData", menuName = "Data/Soldier", order = 1)]
    public class SoldierData : ScriptableObject
    {
        public SoldierType soldierType;
        public EnemyBuildType enemyBuildType;
        public float speed;
        public int damageNumber;
        public int hp;
        public Material material;
    }
}