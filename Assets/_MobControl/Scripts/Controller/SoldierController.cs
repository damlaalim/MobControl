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
        [SerializeField] private float bounceForce, bounceTime, deadDelay;
        [SerializeField] private Animator animator;
        
        private Transform _target;
        private SoldierData _soldierData;
        private NavMeshAgent _navMeshAgent;
        private int _currentHp;
        private SoldierManager _soldierManager;
        private Coroutine _moveRoutine;
        private bool _isMove;
        private Collider _collider;

        private static readonly int Die = Animator.StringToHash("Die");

        public void Initialize(Transform targetObject, SoldierData data, SoldierManager soldierManager)
        {
            _soldierManager = soldierManager;
            _target = targetObject;
            _soldierData = data;
            _currentHp = data.hp;
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _collider = GetComponent<Collider>();
            soldierCanReproduce = true;

            soldierRenderer.material = data.material;
            _isMove = true;
            _moveRoutine = StartCoroutine(MoveToTarget());
        }

        public void Destroy()
        {
            _collider.enabled = false;
            _navMeshAgent.updatePosition = false;
            _isMove = false;
            if (_moveRoutine is not null)
                StopCoroutine(_moveRoutine);
            
            animator.CrossFade(Die, 0f, 0);
  
            StartCoroutine(DestroyRoutine());            
            IEnumerator DestroyRoutine()
            {
                yield return new WaitForSeconds(deadDelay);
                _soldierManager.DestroySoldier(this);
            }
        }
        
        private IEnumerator MoveToTarget()
        {
            while (_isMove)
            {
                if (_target != null)
                    _navMeshAgent.SetDestination(_target.position);
                yield return null;
            }
        }

        public void CollisionDetection(int damage)
        {
            _currentHp -= damage;
            if (_currentHp <= 0)
                Destroy();
        }

        private IEnumerator BounceRoutine(Vector3 direction)
        {
            var elapsedTime = 0f;
            var startPos = transform.position;
            var endPos = startPos + direction * bounceForce;
            
            while (elapsedTime < bounceTime)
            {
                var normalized = elapsedTime / bounceTime;
                transform.position = Vector3.Lerp(startPos, endPos, normalized);
                elapsedTime += Time.deltaTime;
                yield return 0;
            }

            transform.position = endPos;
        }

        private void CollisionDetection(Transform collisionTransform)
        {
            --_currentHp;
            if (_currentHp <= 0)
                Destroy();
            else
            {
                var dir = (transform.position - collisionTransform.position).normalized;
                StartCoroutine(BounceRoutine(dir));
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<BuildController>(out var buildController) && 
                buildController.GetBuildData.type == GetSoldierData.enemyBuildType)
            {
                buildController.CollisionDetection(_soldierData.damageNumber);
                CollisionDetection(other.transform);
            }
            else if (other.TryGetComponent<ObstacleController>(out var obstacleController))
            {
                if (GetSoldierData.canKillByObstacle)
                {
                    obstacleController.CollisionDetection(_soldierData.damageNumber);
                    CollisionDetection(other.transform);
                }
                else
                {
                    var dir = (transform.position - other.transform.position).normalized;
                    StartCoroutine(BounceRoutine(dir));
                }
            }
            else if (other.TryGetComponent<SoldierController>(out var soldierController) && 
                     !GetSoldierData.partisanSoldierList.Contains(soldierController.GetSoldierData.soldierType))
            {
                soldierController.CollisionDetection(_soldierData.damageNumber);
            }
            else if (other.TryGetComponent<GateController>(out var gateController) && soldierCanReproduce && GetSoldierData.canReproduce && gateController.type == GateType.Copy)
            {
                soldierCanReproduce = false;
                _soldierManager.CopySoldier(this, (int)gateController.gatePoint - 1);
            }
            else if (other.TryGetComponent<GateController>(out var deadGateController) &&
                     deadGateController.type == GateType.Dead && GetSoldierData.canKillByGate)
            {
                Destroy();
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