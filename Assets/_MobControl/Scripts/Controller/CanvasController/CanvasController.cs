using _MobControl.Scripts.Data;
using UnityEngine;

namespace _MobControl.Scripts.Controller.CanvasController
{
    public class CanvasController : MonoBehaviour
    {
        public CanvasType type;
        [SerializeField] private Canvas canvas;

        public virtual void Open() => canvas.enabled = true;
        public void Close() => canvas.enabled = false;

        public virtual void Initialize()
        {
            
        }
    }
}