using System;
using Player;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerLevelDisplay : MonoBehaviour
    {
        public TMP_Text levelNum, cashNum;
        public Slider expSlider;
        
        private const float AnimationDuration = 0.7f;
        private const Ease AnimationEase = Ease.OutCubic;
        
                        
        private void OnEnable()
        {
            PlayerController.OnPlayerLevelUpdate += HandleLevelUpdate;
            PlayerController.OnPlayerExpUpdate += HandleExpUpdate;
            PlayerController.OnCashUpdate += HandleCashUpdate;
        }
        
        private void OnDisable()
        {
            PlayerController.OnPlayerLevelUpdate -= HandleLevelUpdate;
            PlayerController.OnPlayerExpUpdate -= HandleExpUpdate;
            PlayerController.OnCashUpdate -= HandleCashUpdate;
        }

        private void HandleLevelUpdate(int level, int newThreshold)
        {
            levelNum.text = level.ToString();
            expSlider.maxValue = newThreshold;
        }

        private void HandleCashUpdate(int cash)
        {
            cashNum.text = "$" + cash;
        }

        private void HandleExpUpdate(int exp)
        {
            Tween.UISliderValue(expSlider, exp, AnimationDuration, AnimationEase);
        }
    }
}
