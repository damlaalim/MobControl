using UnityEngine;

namespace _MobControl.Scripts.Data
{
    [CreateAssetMenu(fileName = "BuildData", menuName = "Data/Build", order = 2)]
    public class BuildData : ScriptableObject
    {
        public EnemyBuildType type;
        public int maxHp;
        public float soldierCreateDelay;
        public int createSoldierCount;
    }
}