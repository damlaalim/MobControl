using _MobControl.Scripts.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _MobControl.Scripts.Controller.CanvasController
{
    public class InGameCanvas : CanvasController
    {
        [SerializeField] private Slider cannonSlider;
        [SerializeField] private TextMeshProUGUI hpText;

        public override void Initialize(GameManager pGameManager)
        {
            base.Initialize(pGameManager);
            cannonSlider.value = 0;
        }

        public void UpdateCannonSliderValue(float value)
        {
            cannonSlider.value = value;
        }

        public void UpdateCastleHpValue(string value)
        {
            hpText.text = value;
        }
    }
}