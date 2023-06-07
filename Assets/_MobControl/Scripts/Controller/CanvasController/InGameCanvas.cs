using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _MobControl.Scripts.Controller.CanvasController
{
    public class InGameCanvas : CanvasController
    {
        [SerializeField] private Slider cannonSlider;
        [SerializeField] private TextMeshProUGUI hpText;

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