using System.Collections;
using _MobControl.Scripts.Data;
using _MobControl.Scripts.Manager;
using UnityEngine;

namespace _MobControl.Scripts.Controller
{
    public class InputController : MonoBehaviour
    {
        private bool IsPressLeft => Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        private bool IsPressRight => Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

        private bool IsPressUp => Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow) ||
                                  Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow);
        
        [SerializeField] private float swipeRange;
        [SerializeField] private float tapRange;

        private Vector2 _startTouchPos, _currentPos, _endTouchPos;
        private bool _swipeHorizontal, _horizontalControl;
        private Vector3 _inputValue = Vector3.zero;
        private CannonMachineController _machineController;
        private GameManager _gameManager;
        private Coroutine _inputControlRoutine;

        public void Initialize(CannonMachineController machineController, GameManager gameManager)
        {
            _machineController = machineController;
            _gameManager = gameManager;

            gameManager.GameIsStart += StartGame;
        }

        private void StartGame()
        {
            _inputControlRoutine = StartCoroutine(InputControlRoutine());
        }

        private IEnumerator InputControlRoutine()
        {
            while (_gameManager.gameIsStart)
            {
                if (IsPressLeft || IsPressRight)
                    MoveHorizontal(IsPressLeft);
                else if (IsPressUp)
                    _machineController.CreateBigSoldier();     
                else
                    SwipeControl();

                yield return null;
            }
        }

        private void SwipeControl()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _startTouchPos = Input.GetTouch(0).position;
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                _currentPos = Input.GetTouch(0).position;
                var dis = _currentPos - _startTouchPos;
                var moveIsLeft = dis.x < -swipeRange;
                MoveHorizontal(moveIsLeft);
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                _endTouchPos = Input.GetTouch(0).position;
                _machineController.CreateBigSoldier();
            }
        }

        private void MoveHorizontal(bool moveIsLeft)
        {
            _machineController.Move(moveIsLeft);
            var soldierCount = _machineController.GetBuildData.createSoldierCount;
            _machineController.CreateSoldier(SoldierType.PartisanSoldierSmall, soldierCount);
        }
    }
}