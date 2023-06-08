using System.Collections.Generic;
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
        [SerializeField] private List<TextMeshProUGUI> gateNumberTextList;
        [SerializeField] private List<GateController> gateList;

        public override void Initialize(GameManager pGameManager)
        {
            base.Initialize(pGameManager);
            cannonSlider.value = 0;

            for (var i = 0; i < gateList.Count; i++)
            {
                gateNumberTextList[i].GetComponent<GameObjectFollowController>().target = gateList[i].textTransform;
                gateNumberTextList[i].text = gateList[i].gatePoint.ToString();
            }
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