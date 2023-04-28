using UnityEngine;

namespace ChargeGame
{
    public class TagComponent : MonoBehaviour
    {
        public enum ObjectTag
        {
            Ground,
            Wall,
            Player
        }

        public ObjectTag Tag;
    }
}
