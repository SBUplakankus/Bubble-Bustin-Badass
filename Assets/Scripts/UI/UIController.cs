using System;
using Player;
using PrimeTween;
using UnityEngine;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [Header("UI Objects")]
        public GameObject uiBlur;
        public RectTransform xpPanel, enemyInfoPanel, abilitiesPanel, levelUpPanel;
        private const float AnimationDuration = 0.5f;
        private const Ease AnimationEase = Ease.OutCubic;

        [Header("UI Positions")] 
        private const int HideXpPanelX = -800;
        private const int HideLevelUpPanelY = 240;
        private const int HideEnemyInfoPanelX = 460;
        

        public static event Action OnGameResume;

        private void Start()
        {
            uiBlur.SetActive(false);
            HideLevelUpPanel();
            ShowXpPanel();
            ShowEnemyInfoPanel();
        }

        private void OnEnable()
        {
            PlayerController.OnPlayerLevelUp += HandleLevelUp;
        }
        
        private void OnDisable()
        {
            PlayerController.OnPlayerLevelUp -= HandleLevelUp;
        }

        private void HandleLevelUp()
        {
            HideXpPanel();
            HideEnemyInfoPanel();
            uiBlur.SetActive(true);
            ShowLevelUpPanel();
        }

        private void HideXpPanel()
        {
            var pos = xpPanel.anchoredPosition;
            pos.x = HideXpPanelX;
            xpPanel.anchoredPosition = pos;
        }

        private void ShowXpPanel()
        {
            Tween.UIAnchoredPosition(xpPanel, Vector2.zero, AnimationDuration, AnimationEase, useUnscaledTime: true);
        }

        private void HideEnemyInfoPanel()
        {
            var pos = enemyInfoPanel.anchoredPosition;
            pos.x = HideEnemyInfoPanelX;
            enemyInfoPanel.anchoredPosition = pos;
        }

        private void ShowEnemyInfoPanel()
        {
            Tween.UIAnchoredPosition(enemyInfoPanel, Vector2.zero, AnimationDuration, AnimationEase, useUnscaledTime: true);
        }

        private void ShowLevelUpPanel()
        {
            Tween.UIAnchoredPosition(levelUpPanel, Vector2.zero, AnimationDuration, AnimationEase, useUnscaledTime: true);
        }

        private void HideLevelUpPanel()
        {
            var pos = levelUpPanel.anchoredPosition;
            pos.y = HideLevelUpPanelY;
            levelUpPanel.anchoredPosition = pos;
        }
        
        public void ResumeGame()
        {
            OnGameResume?.Invoke();
            ShowEnemyInfoPanel();
            HideLevelUpPanel();
            ShowXpPanel();
            uiBlur.SetActive(false);
            
        }
    }
}
