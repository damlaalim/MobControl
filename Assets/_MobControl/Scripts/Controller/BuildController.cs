using System.Collections;
using _MobControl.Scripts.Data;
using _MobControl.Scripts.Manager;
using UnityEngine;

namespace _MobControl.Scripts.Controller
{
    public class BuildController : MonoBehaviour
    {
        public BuildData GetBuildData => data;
        [SerializeField] private BuildData data;

        protected int currentHp;
        private bool _creatingSoldier;
        private SoldierManager _soldierManager;

 
        public virtual void Initialize(SoldierManager soldierManager, GameManager gameManager)
        {
            currentHp = data.maxHp;
            _soldierManager = soldierManager;
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }
        
        public void CollisionDetection(int damage)
        {
            currentHp -= damage;
            if (currentHp <= 0)
                Destroy();
        }

        public void CreateSoldier(SoldierType type, int soldierCount)
        {
            if (!_creatingSoldier)
                StartCoroutine(CreateSoldierRoutine(type, soldierCount));
        }
        
        private IEnumerator CreateSoldierRoutine(SoldierType type, int soldierCount)
        {
            _creatingSoldier = true;
            
            for (var i = 0; i < soldierCount; i++)
            {
                _soldierManager.CreateSoldier(type, transform.position);
            }
            yield return new WaitForSeconds(data.soldierCreateDelay);
            
            _creatingSoldier = false;
        }
    }
}