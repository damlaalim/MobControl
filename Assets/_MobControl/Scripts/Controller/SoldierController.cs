using System.Collections;
using _MobControl.Scripts.Data;
using _MobControl.Scripts.Manager;
using UnityEngine;
using UnityEngine.AI;

namespace _MobControl.Scripts.Controller
{
    public class SoldierController : MonoBehaviour
    {
        public SoldierData GetSoldierData => _soldierData;
        public bool soldierCanReproduce;
        
        [SerializeField] private Renderer soldierRenderer;
        
        private Transform _target;
        private SoldierData _soldierData;
        private NavMeshAgent _navMeshAgent;
        private int _currentHp;
        private SoldierManager _soldierManager;

        public void Initialize(Transform targetObject, SoldierData data, SoldierManager soldierManager)
        {
            _soldierManager = soldierManager;
            _target = targetObject;
            _soldierData = data;
            _currentHp = data.hp;
            _navMeshAgent = GetComponent<NavMeshAgent>();
            soldierCanReproduce = true;

            soldierRenderer.material = data.material;
            
            StartCoroutine(MoveToTarget());
        }

        private void Destroy()
        {
            _soldierManager.DestroySoldier(this);
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

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<BuildController>(out var buildController) && 
                buildController.GetBuildData.type == GetSoldierData.enemyBuildType)
            {
                buildController.CollisionDetection(_soldierData.damageNumber);
                --_currentHp;
                if (_currentHp <= 0)
                    Destroy();
            }
            else if (other.TryGetComponent<SoldierController>(out var soldierController) && 
                     !GetSoldierData.partisanSoldierList.Contains(soldierController.GetSoldierData.soldierType))
            {
                soldierController.CollisionDetection(_soldierData.damageNumber);
            }
            else if (other.TryGetComponent<GateController>(out var gateController) && soldierCanReproduce && GetSoldierData.canReproduce && gateController.GetGateType == GateType.Copy)
            {
                soldierCanReproduce = false;
                _soldierManager.CopySoldier(this, (int)gateController.GatePoint);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<GateController>(out var gateController) && !soldierCanReproduce)
            {
                soldierCanReproduce = true;
            }
        }
    }
}