using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Player;
using PrimeTween;
using SOs;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UI
{
    public class CardManager : MonoBehaviour
    {
        [Header("Card Display")]
        public CardDisplay[] cards;
        public Color32[] rarityColours;
        public string[] rarityNames = { "Common", "Uncommon", "Rare", "Ultra Rare", "Legendary" };
        public GameObject postProcess;

        [Header("Upgrades")] 
        public List<UpgradeSO> upgrades;
        public PlayerController player;
        
        [Header("Card Animation")]
        public RectTransform[] cardPositions;
        private const float AnimationDuration = 1f;
        private const Ease AnimationEase = Ease.OutCubic;
        private const float CardInterval = 0.25f;
        
        public static event Action OnCardsDisplay;

        private void Start()
        {
            postProcess.SetActive(true);
            HideCards();
        }

        private void OnEnable()
        {
            PlayerController.OnPlayerLevelUp += HandleLevelUp;
            UIController.OnGameResume += HandleGameResume;
            HideCards();
        }

        private void OnDisable()
        {
            PlayerController.OnPlayerLevelUp -= HandleLevelUp;
            UIController.OnGameResume -= HandleGameResume;
        }

        public void DisableBloom()
        {
            postProcess.SetActive(false);
        }
        private void HandleLevelUp()
        {
            postProcess.SetActive(true);
            OnCardsDisplay?.Invoke();
            StartCoroutine(CardDisplayCoroutine());
        }

        private void HandleGameResume()
        {
            postProcess.SetActive(false);
            HideCards();
        }

        private void HideCards()
        {
            foreach (var card in cardPositions)
            {
                var newPos = card.anchoredPosition;
                newPos.y -= 1400;
                card.anchoredPosition = newPos;
            }
        }

        private IEnumerator CardDisplayCoroutine()
        {
            var unusedUpgrades = upgrades.ToList();
            HideCards();
            
            for (var i = 0; i < cards.Length; i++)
            {
                var rng = Random.Range(0, unusedUpgrades.Count);
                var upg = unusedUpgrades[rng];
                unusedUpgrades.Remove(upg);

                var upgradeValue = 0;
                var value = upg.upgradeValue;
                var rarity = 0;
                
                rng = Random.Range(0, 101);
                switch (rng)
                {
                    case < 40:
                        upgradeValue = value;
                        rarity = 0;
                        break;
                    case < 70:
                        upgradeValue = (int)(value * 2);
                        rarity = 1;
                        break;
                    case < 85:
                        upgradeValue = value * 3;
                        rarity = 2;
                        break;
                    case < 95:
                        upgradeValue = value * 4;
                        rarity = 3;
                        break;
                    case < 101:
                        upgradeValue = value * 5;
                        rarity = 4;
                        break;
                    default:
                        break;
                }

                string upgradeText;
                int oldStat;
                int newStat;
                
                switch (upg.upgradeType)
                {
                    case UpgradeSO.UpgradeType.Health:
                        upgradeText = "Increased Health";
                        oldStat = player.playerMaxHealth;
                        newStat = oldStat + upgradeValue;
                        break;
                    case UpgradeSO.UpgradeType.Speed:
                        upgradeText = "Increased Movement Speed";
                        oldStat = player.playerSpeed;
                        newStat = oldStat + upgradeValue;
                        break;
                    case UpgradeSO.UpgradeType.AbilityDamage:
                        upgradeText = "Increased Spray Damage";
                        oldStat = player.abilityDamage;
                        newStat = oldStat + upgradeValue;
                        break;
                    case UpgradeSO.UpgradeType.LeapDistance:
                        upgradeText = "Increased Dash Distance";
                        oldStat = player.dashDistance;
                        newStat = oldStat + upgradeValue;
                        break;
                    case UpgradeSO.UpgradeType.NeedleDamage:
                        upgradeText = "Increased Needle Damage";
                        oldStat = player.playerDamage;
                        newStat = oldStat + upgradeValue;
                        break;
                    case UpgradeSO.UpgradeType.NeedleStrength:
                        upgradeText = "Increased Needle Penetration";
                        oldStat = player.projectileStrength;
                        newStat = oldStat + upgradeValue;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                cards[i].SetCardDetails(upg.upgradeName, upgradeText, oldStat, newStat, rarityNames[rarity],
                    rarityColours[rarity], upg, upgradeValue);

                Tween.UIAnchoredPositionY(cardPositions[i], 0, AnimationDuration, AnimationEase, useUnscaledTime: true);

                yield return new WaitForSecondsRealtime(CardInterval);
            }
        }
    }
}
