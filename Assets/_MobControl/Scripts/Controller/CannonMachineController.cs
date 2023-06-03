using UnityEngine;

namespace _MobControl.Scripts.Controller
{
    public class CannonMachineController : BuildController
    {
        [SerializeField] private float moveRange, moveSpeed;
        
        public void Move(bool moveIsLeft)
        {
            var direction = moveIsLeft ? -1 : 1; 
            var pos = transform.position;
            pos.x += (moveRange * moveSpeed) * direction;

            transform.position = pos;
        }
    }
}