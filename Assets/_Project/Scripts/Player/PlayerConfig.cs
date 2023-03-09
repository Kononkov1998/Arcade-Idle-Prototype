using UnityEngine;

namespace _Project.Scripts.Player
{
    [CreateAssetMenu(menuName = "PlayerConfig", fileName = "PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        public float PickUpRadius;
    }
}
