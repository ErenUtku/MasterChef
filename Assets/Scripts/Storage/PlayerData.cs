using UnityEngine;

namespace Storage
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Player Data", order = 51)]
    public class PlayerData : ScriptableObject
    {
        public int currency;
        public int level;
    }
}
