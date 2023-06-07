using UnityEngine;

namespace _MobControl.Scripts.Data
{
    [CreateAssetMenu(fileName = "BuildData", menuName = "Data/Build", order = 2)]
    public class BuildData : ScriptableObject
    {
        public BuildType type;
        public int maxHp;
        public float soldierCreateWaitTime;
        public int createSoldierCount;
    }
}