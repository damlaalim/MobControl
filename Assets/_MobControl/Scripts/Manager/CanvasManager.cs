using System.Collections.Generic;
using _MobControl.Scripts.Controller.CanvasController;
using _MobControl.Scripts.Data;
using UnityEngine;

namespace _MobControl.Scripts.Manager
{
    public class CanvasManager : MonoBehaviour
    {
        [SerializeField] private List<CanvasController> canvasList;
        
        public void Initialize()
        {
            foreach (var canvas in canvasList)
            {
                canvas.Initialize();
            }
        }

        public void OpenCanvas(CanvasType type)
        {
            foreach (var canvas in canvasList)
            {
                if (canvas.type == type)
                    canvas.Open();
                else 
                    canvas.Close();
            }
        }

        public CanvasController GetCanvasController(CanvasType type)
        {
            foreach (var canvas in canvasList)
            {
                if (canvas.type == type)
                    return canvas;
            }

            return null;
        }
    }
}