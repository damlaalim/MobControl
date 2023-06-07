using UnityEngine;

namespace _MobControl.Scripts.Controller.CanvasController
{
    public class EndCanvas : CanvasController
    {
        [SerializeField] private Canvas successCanvas;
        [SerializeField] private Canvas failCanvas;
        
        public override void Open()
        {
            successCanvas.enabled = gameManager.finishIsSuccess;
            failCanvas.enabled = !gameManager.finishIsSuccess;
            
            base.Open();
        }
    }
}