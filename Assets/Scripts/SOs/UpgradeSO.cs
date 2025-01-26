using UnityEngine;

namespace SOs
{
    [CreateAssetMenu(fileName = "UpgradeSO", menuName = "Scriptable Objects/UpgradeSO")]
    public class UpgradeSO : ScriptableObject
    {
        public enum UpgradeType
        {
            Health,
            Speed,
            NeedleDamage,
            AbilityDamage,
            LeapDistance,
            NeedleStrength
        }
        
        [Header("Upgrade Info")] 
        public string upgradeName;
        public UpgradeType upgradeType;
        public int upgradeValue;
        public Sprite upgradeIcon;

    }
}
