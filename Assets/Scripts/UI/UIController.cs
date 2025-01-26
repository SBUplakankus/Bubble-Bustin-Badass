using System;
using Player;
using PrimeTween;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [Header("UI Objects")]
        public GameObject uiBlur;
        public GameObject bloom;

        public RectTransform xpPanel,
            enemyInfoPanel,
            abilitiesPanel,
            levelUpPanel,
            tutorialPanel,
            pausePanel,
            statsPanel;
        
        private const float AnimationDuration = 0.3f;
        private const Ease AnimationEase = Ease.OutCubic;

        [Header("UI Positions")] 
        private const int HideXpPanelX = -800;
        private const int HideLevelUpPanelY = 240;
        private const int HideEnemyInfoPanelX = 460;
        private const int HideTutorialPanelY = 1200;
        private const int HidePausePanelX = -650;
        private const int HideStatsX = 780;
        private const int HideAbilityY = -320;
        

        public static event Action OnGameResume;
        public static event Action OnGamePause;
        public static event Action OnGameUnpause;

        private void Start()
        {
            uiBlur.SetActive(true);
            ShowTutorial();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ShowPauseMenu();
            }
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
            HideAbilityPanel();
            uiBlur.SetActive(true);
            ShowLevelUpPanel();
        }

        private void HideAbilityPanel()
        {
            var pos = abilitiesPanel.anchoredPosition;
            pos.y = HideAbilityY;
            abilitiesPanel.anchoredPosition = pos;
        }

        private void ShowAbilityPanel()
        {
            Tween.UIAnchoredPosition(abilitiesPanel, Vector2.zero, AnimationDuration, AnimationEase, useUnscaledTime: true);
        }
        
        public void HidePauseMenu()
        {
            OnGameUnpause?.Invoke();
            var pos = statsPanel.anchoredPosition;
            pos.x = HideStatsX;
            statsPanel.anchoredPosition = pos;
            
            pos = pausePanel.anchoredPosition;
            pos.x = HidePausePanelX;
            pausePanel.anchoredPosition = pos;
            
            uiBlur.SetActive(false);
            bloom.SetActive(false);
            
            ShowXpPanel();
            ShowEnemyInfoPanel();
            ShowAbilityPanel();
        }

        private void ShowPauseMenu()
        {
            bloom.SetActive(true);
            uiBlur.SetActive(true);
            HideEnemyInfoPanel();
            HideXpPanel();
            HideAbilityPanel();
            
            Tween.UIAnchoredPosition(statsPanel, Vector2.zero, AnimationDuration, AnimationEase, useUnscaledTime: true);
            Tween.UIAnchoredPosition(pausePanel, Vector2.zero, AnimationDuration, AnimationEase, useUnscaledTime: true);
            OnGamePause?.Invoke();
        }
        
        private void HideXpPanel()
        {
            var pos = xpPanel.anchoredPosition;
            pos.x = HideXpPanelX;
            xpPanel.anchoredPosition = pos;
        }

        private void ShowTutorial()
        {
            Tween.UIAnchoredPosition(tutorialPanel, Vector2.zero, AnimationDuration, AnimationEase, useUnscaledTime: true);
        }
    
        public void HideTutorial()
        {
            var pos = tutorialPanel.anchoredPosition;
            pos.y = HideTutorialPanelY;
            tutorialPanel.anchoredPosition = pos;
            ResumeGame();
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
            ShowAbilityPanel();
            uiBlur.SetActive(false);
            
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(0);
        }
    }
}
