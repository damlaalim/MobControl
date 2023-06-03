using System;
using _MobControl.Scripts.Controller;
using UnityEngine;

namespace _MobControl.Scripts.Manager
{
    public class GameManager : MonoBehaviour
    {
        public Action StartGame;
        
        [SerializeField] private InputController inputController;
        [SerializeField] private CannonMachineController machineController;
        [SerializeField] private SoldierManager soldierManager;
        [SerializeField] private BuildManager buildManager;

        public bool gameIsStart;
        
        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        { 
            soldierManager.Initialize(buildManager);
            buildManager.Initialize(soldierManager, this);
            inputController.Initialize(machineController, this);

            gameIsStart = true;
            StartGame?.Invoke();
        }
    }
}