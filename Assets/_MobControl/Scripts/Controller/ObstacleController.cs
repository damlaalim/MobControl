using _MobControl.Scripts.Controller.CanvasController;
using TMPro;
using UnityEngine;

namespace _MobControl.Scripts.Controller
{
    public class ObstacleController : MonoBehaviour
    {
        [SerializeField] private int hp;
        [SerializeField] private TextMeshProUGUI obstacleText;

        private int _currentHp;
        private InGameCanvas _inGameCanvas;
        
        public void Initialize()
        {
            _currentHp = hp;
            obstacleText.text = _currentHp.ToString();
        }

        private void Destroy()
        {
            obstacleText.gameObject.SetActive(false);
            Destroy(gameObject);
        }
        
        public void CollisionDetection(int damage)
        {
            _currentHp -= damage;
            if (_currentHp <= 0)
                Destroy();
            else
                obstacleText.text = _currentHp.ToString();
        }
    }
}