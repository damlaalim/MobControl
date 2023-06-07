using System.Collections;
using _MobControl.Scripts.Data;
using _MobControl.Scripts.Manager;
using UnityEngine;

namespace _MobControl.Scripts.Controller
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private float swipeRange;
        [SerializeField] private float tapRange;
        
        private Vector2 _startTouchPos, _currentPos, _endTouchPos;
        private bool _swipeHorizontal, _horizontalControl;
        private Vector3 _inputValue = Vector3.zero;
        private CannonMachineController _machineController;
        private GameManager _gameManager;

        public void Initialize(CannonMachineController machineController, GameManager gameManager)
        {
            _machineController = machineController;
            _gameManager = gameManager;
            
            gameManager.GameIsStart += StartGame;
        }

        private void StartGame()
        {
            StartCoroutine(SwipeControl());
        }

        private IEnumerator SwipeControl()
        {
            while (_gameManager.gameIsStart)
            {
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    _startTouchPos = Input.GetTouch(0).position;
                }
            
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    _currentPos = Input.GetTouch(0).position;
                    var dis = _currentPos - _startTouchPos;

                    _machineController.Move(dis.x < -swipeRange);
                    var soldierCount = _machineController.GetBuildData.createSoldierCount;
                    _machineController.CreateSoldier(SoldierType.PartisanSoldierSmall, soldierCount);
                }

                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    _endTouchPos = Input.GetTouch(0).position;
                    _machineController.CreateBigSoldier();
                }
                
                yield return 0;
            }
        }
    }
}
