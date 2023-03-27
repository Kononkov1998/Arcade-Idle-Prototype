using UnityEngine;

namespace _Project.Scripts.Player
{
    [CreateAssetMenu(menuName = "PlayerConfig", fileName = "PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        public float PickUpRadius;
        public float SpreadRadius;
        public float TransferDelay;
        public float TransferToHoldPointDuration;
        public float TransferDuration;
    }
}
