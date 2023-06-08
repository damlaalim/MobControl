using System.Collections;
using _MobControl.Scripts.Data;
using _MobControl.Scripts.Manager;
using UnityEngine;

namespace _MobControl.Scripts.Controller
{
    public class CastleBuildController : BuildController
    {
        private GameManager _gameManager;
        
        public override void Initialize(SoldierManager soldierManager, GameManager gameManager, CanvasManager canvasManager)
        {
            _gameManager = gameManager;
            base.Initialize(soldierManager, gameManager, canvasManager);
            gameManager.GameIsStart += StartGame;
            inGameCanvas.UpdateCastleHpValue(currentHp.ToString());
        }

        protected override void Destroy()
        {
            inGameCanvas.UpdateCastleHpValue(currentHp.ToString());
            base.Destroy();
        }

        private void StartGame()
        {
            StartCoroutine(CreateSoldier());
        }

        private IEnumerator CreateSoldier()
        {
            while (currentHp > 0 && _gameManager.gameIsStart)
            {
                CreateSoldier(SoldierType.EnemySoldierSmall, GetBuildData.createSoldierCount);
                yield return new WaitForSeconds(.2f);
            }
        }

        public override void CollisionDetection(int damage)
        {
            base.CollisionDetection(damage);
            inGameCanvas.UpdateCastleHpValue(currentHp.ToString());
        }
    }
}