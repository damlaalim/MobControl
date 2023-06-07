using _MobControl.Scripts.Data;
using UnityEngine;

namespace _MobControl.Scripts.Controller
{
    public class GateController : MonoBehaviour
    {
        public GateType GetGateType => type;
        public GatePoint GatePoint => gatePoint; 
        [SerializeField] private GatePoint gatePoint;
        [SerializeField] private GateType type;
    }
}