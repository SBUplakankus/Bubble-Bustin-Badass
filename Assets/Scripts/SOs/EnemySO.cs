using UnityEngine;
using UnityEngine.Serialization;

namespace SOs
{
    [CreateAssetMenu(fileName = "EnemySO", menuName = "Scriptable Objects/EnemySO")]
    public class EnemySO : ScriptableObject
    {
        [Header("Enemy Info")] 
        public string enemyName;
        [TextArea (2,10)] public string description;
        public int bubbleBodyIndex;
        
        [Header("Enemy Stats")] 
        public int health;
        public int speed;
        public int damage;
        public int xpGiven;
    }
}
