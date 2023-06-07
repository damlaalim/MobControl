using System.Collections;
using _MobControl.Scripts.Controller.CanvasController;
using _MobControl.Scripts.Data;
using _MobControl.Scripts.Manager;
using UnityEngine;

namespace _MobControl.Scripts.Controller
{
    public class BuildController : MonoBehaviour
    {
        public BuildData GetBuildData => data;
        [SerializeField] private BuildData data;
        [SerializeField] private Transform soldierCreateTransform;

        protected int currentHp;
        protected CanvasManager _canvasManager;
        protected InGameCanvas inGameCanvas;
        private bool _creatingSoldier;
        private SoldierManager _soldierManager;
        private GameManager _gameManager;
        
        public virtual void Initialize(SoldierManager soldierManager, GameManager gameManager, CanvasManager canvasManager)
        {
            currentHp = data.maxHp;
            _soldierManager = soldierManager;
            _gameManager = gameManager;
            _canvasManager = canvasManager;
            inGameCanvas = (InGameCanvas)canvasManager.GetCanvasController(CanvasType.InGame);
        }

        protected virtual void Destroy()
        {
            _gameManager.FinishGame(data.type != BuildType.CannonMachine);
        }
        
        public virtual void CollisionDetection(int damage)
        {
            currentHp -= damage;
            if (currentHp <= 0)
                Destroy();
        }

        public void CreateSoldier(SoldierType type, int soldierCount, bool controlToCreatingSoldier = true)
        {
            if (!_creatingSoldier || !controlToCreatingSoldier)
                StartCoroutine(CreateSoldierRoutine(type, soldierCount));
        }
        
        protected virtual IEnumerator CreateSoldierRoutine(SoldierType type, int soldierCount, float soldierCreateDelay = 0)
        {
            _creatingSoldier = true;

            if (soldierCreateDelay > 0)
                yield return new WaitForSeconds(soldierCreateDelay);
            
            for (var i = 0; i < soldierCount; i++)
            {
                _soldierManager.CreateSoldier(type, soldierCreateTransform.position, soldierCreateTransform.rotation);
            }
            yield return new WaitForSeconds(data.soldierCreateWaitTime);
            
            _creatingSoldier = false;
        }
    }
}