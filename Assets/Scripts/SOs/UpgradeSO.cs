using UnityEngine;

namespace SOs
{
    [CreateAssetMenu(fileName = "UpgradeSO", menuName = "Scriptable Objects/UpgradeSO")]
    public class UpgradeSO : ScriptableObject
    {
        [Header("Upgrade Info")] 
        [SerializeField] private string upgradeName;
        [SerializeField] [TextArea(3,10)] private string upgradeDescription;
        
        [Header("Attribute Modifiers")] 
        [SerializeField] private int healthChange;
        [SerializeField] private int speedChange;
        [SerializeField] private int damageChange;
        [SerializeField] private int abilityDamageChange;
        [SerializeField] private float abilityCooldownChange;
        [SerializeField] private float fireRateChange;
    }
}
