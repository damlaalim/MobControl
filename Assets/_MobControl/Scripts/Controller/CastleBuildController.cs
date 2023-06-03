using System.Collections;
using _MobControl.Scripts.Data;
using _MobControl.Scripts.Manager;
using UnityEngine;

namespace _MobControl.Scripts.Controller
{
    public class CastleBuildController : BuildController
    {
        public override void Initialize(SoldierManager soldierManager, GameManager gameManager)
        {
            base.Initialize(soldierManager, gameManager);
            gameManager.StartGame += StartGame;
        }

        private void StartGame()
        {
            StartCoroutine(CreateSoldier());
        }

        private IEnumerator CreateSoldier()
        {
            while (currentHp > 0)
            {
                CreateSoldier(SoldierType.EnemySoldierSmall, GetBuildData.createSoldierCount);
                yield return new WaitForSeconds(.2f);
            }
        }
    }
}