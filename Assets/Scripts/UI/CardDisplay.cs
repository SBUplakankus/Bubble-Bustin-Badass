using System;
using PrimeTween;
using SOs;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class CardDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        
        public TMP_Text upgradeName, attribute, oldValue, newValue, rarity;
        public Image border, upgradeIcon;

        private const float AnimationDuration = 0.2f;
        private const float GrowSize = 1.04f;
        private const float ShrinkSize = 1.0f;
        private const Ease AnimationEase = Ease.OutCubic;

        private UpgradeSO _upgradeSo;
        private int _upgradeValue;

        public static event Action<UpgradeSO, int> OnCardSelected;
        

        /// <summary>
        /// Set the details of the card for Display
        /// </summary>
        /// <param name="cardName">Name of the Cards Upgrade</param>
        /// <param name="cardAttr">Attribute Changed</param>
        /// <param name="oldNum">Old Attribute Value</param>
        /// <param name="newNum">New Attribute Value</param>
        /// <param name="cardRarity">Card Rarity</param>
        /// <param name="colour">Card Colour</param>
        /// <param name="upg">Upgrade SO</param>
        /// <param name="upgValue">Upgrade Value</param>
        public void SetCardDetails(string cardName, string cardAttr, int oldNum, int newNum, string cardRarity, 
            Color32 colour, UpgradeSO upg, int upgValue)
        {
            upgradeName.text = cardName;
            attribute.text = cardAttr;
            oldValue.text = oldNum.ToString();
            newValue.text = newNum.ToString();
            rarity.text = cardRarity;
            UpdateColours(colour);
            upgradeIcon.sprite = upg.upgradeIcon;

            _upgradeSo = upg;
            _upgradeValue = upgValue;
        }
        
        
        private void UpdateColours(Color32 colour)
        {
            border.color = colour;
            newValue.color = colour;
            rarity.color = colour;
            upgradeIcon.color = colour;
        }

        private void OnEnable()
        {
            transform.localScale = Vector2.one;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Tween.Scale(transform, GrowSize, AnimationDuration, AnimationEase, useUnscaledTime: true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Tween.Scale(transform, ShrinkSize, AnimationDuration, AnimationEase, useUnscaledTime: true);
        }

        public void SelectCard()
        {
            OnCardSelected?.Invoke(_upgradeSo, _upgradeValue);
        }
    }
}
