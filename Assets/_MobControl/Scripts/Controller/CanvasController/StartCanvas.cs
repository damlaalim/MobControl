using _MobControl.Scripts.Manager;
using TMPro;
using UnityEngine;

namespace _MobControl.Scripts.Controller.CanvasController
{
    public class StartCanvas : CanvasController
    {
        [SerializeField] private TextMeshProUGUI levelText;

        public override void Open()
        {
            levelText.text = (LevelManager.Instance.GetLevel + 1).ToString();
            base.Open();
        }
    }
}