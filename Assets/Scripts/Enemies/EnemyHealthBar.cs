using System;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

namespace Enemies
{
    public class EnemyHealthBar : MonoBehaviour
    {
        [Header("Bubble UI Lists")] 
        [SerializeField] private Slider[] bubbleHealthBars;
        
        
        [Header("UI Elements")]
        private Slider _healthBar;
        private Camera _playerCamera;
        
        [Header("Tween Attributes")] 
        private const float AnimationDuration = 1.2f;
        private const float GrowDuration = 0.6f;
        private const float GrowSize = 1f;
        private const Ease AnimationEase = Ease.OutCubic;
        private bool _healthBarDisplay;

        private void Awake()
        {
            _playerCamera = Camera.main;
            
        }

        private void LateUpdate()
        {
            if(!_healthBarDisplay) return;
            
            var direction = _playerCamera.transform.position - _healthBar.transform.position;
            _healthBar.transform.rotation = Quaternion.LookRotation(direction);
            
        }

        /// <summary>
        /// Set the initial values of the health bar
        /// </summary>
        /// <param name="health">Max Health</param>
        /// <param name="bubbleIndex">Bubble Index</param>
        public void SetInitialHealthBarValues(int health, int bubbleIndex)
        {
            _healthBar = bubbleHealthBars[bubbleIndex];
            _healthBar.gameObject.SetActive(false);
            _healthBarDisplay = false;
            _healthBar.maxValue = health;
            _healthBar.value = health;
        }
        
        /// <summary>
        /// Update the enemy health bar and show if hidden
        /// </summary>
        /// <param name="newHealth">New Health Amount</param>
        public void UpdateHealthBarValues(int newHealth)
        {
            if (!_healthBar.enabled) return;
            if (Mathf.Approximately(_healthBar.value, _healthBar.maxValue))
            {
                ShowHealthBar();
            }
            
            Tween.UISliderValue(_healthBar, newHealth, AnimationDuration, AnimationEase);
        }

        private void ShowHealthBar()
        {
            _healthBarDisplay = true;
            _healthBar.gameObject.SetActive(true);
            _healthBar.transform.localScale = Vector2.zero;
            Tween.Scale(_healthBar.transform, GrowSize, GrowDuration, AnimationEase);
        }
    }
}
