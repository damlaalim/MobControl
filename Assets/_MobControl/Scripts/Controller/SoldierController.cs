using System.Collections;
using _MobControl.Scripts.Data;
using UnityEngine;
using UnityEngine.AI;

namespace _MobControl.Scripts.Controller
{
    public class SoldierController : MonoBehaviour
    {
        public SoldierData GetSoldierData => _soldierData;
        
        private Transform _target;
        private SoldierData _soldierData;
        private NavMeshAgent _navMeshAgent;
        private int _currentHp;
        private Renderer _renderer;

        public void Initialize(Transform targetObject, SoldierData data)
        {
            _target = targetObject;
            _soldierData = data;
            _currentHp = data.hp;
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _renderer = GetComponent<Renderer>();

            _renderer.material = data.material;
            
            StartCoroutine(MoveToTarget());
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }
        
        private IEnumerator MoveToTarget()
        {
            while (true)
            {
                _navMeshAgent.destination = _target.position;
                yield return null;
            }
        }

        public void CollisionDetection(int damage)
        {
            _currentHp -= damage;
            if (_currentHp <= 0)
                Destroy();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<BuildController>(out var buildController) && 
                buildController.GetBuildData.type == GetSoldierData.enemyBuildType)
            {
                buildController.CollisionDetection(_soldierData.damageNumber);
                --_currentHp;
                if (_currentHp <= 0)
                    Destroy();
            }
            else if (collision.gameObject.TryGetComponent<SoldierController>(out var soldierController) && 
                     soldierController.GetSoldierData.soldierType != GetSoldierData.soldierType)
            {
                soldierController.CollisionDetection(_soldierData.damageNumber);
            }
        }
    }
}