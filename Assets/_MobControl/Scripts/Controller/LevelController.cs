using _MobControl.Scripts.Manager;
using UnityEngine;

namespace _MobControl.Scripts.Controller
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        
        public void Initialize()
        {
            gameManager.Initialize();    
        }
    }
}