using System;
using _MobControl.Scripts.Controller;
using _MobControl.Scripts.Data;
using UnityEngine;

namespace _MobControl.Scripts.Manager
{
    public class GameManager : MonoBehaviour
    {
        public Action GameIsStart;
        
        [SerializeField] private InputController inputController;
        [SerializeField] private CannonMachineController machineController;
        [SerializeField] private SoldierManager soldierManager;
        [SerializeField] private BuildManager buildManager;
        [SerializeField] private CameraManager cameraManager;
        [SerializeField] private CanvasManager canvasManager;

        public bool gameIsStart;
        
        public void Initialize()
        { 
            canvasManager.Initialize();
            soldierManager.Initialize(buildManager);
            buildManager.Initialize(soldierManager, this, canvasManager);
            inputController.Initialize(machineController, this);
            cameraManager.Initialize();
            
            canvasManager.OpenCanvas(CanvasType.Start);
        }

        public void StartFirstGame()
        {
            gameIsStart = true;
            GameIsStart?.Invoke();
            canvasManager.OpenCanvas(CanvasType.InGame);
        }

        public void StartNextGame()
        {
            LevelManager.Instance.Load();
        }

        public void FinishGame(bool success)
        {
            gameIsStart = false;
            soldierManager.FinishGame();
            LevelManager.Instance.FinishGame(success);
            canvasManager.OpenCanvas(CanvasType.End);
        }
    }
}