using System;
using Enemies;
using Items;
using UnityEngine;

namespace Player
{
    public class PlayerStats : MonoBehaviour
    {
        [Header("Game Stats")] 
        private float _gameDuration;
        private int _bubblesPopped;
        private int _damageTaken;
        private int _needlesFired;
        private int _abilitiesUsed;
        private int _playerLevel;
        private int _playerExperience;
        private int _cashCollected;
        private int _enemyWave;

        private void OnEnable()
        {
            PlayerController.OnPlayerDamageTaken += HandleDamageTaken;
            PlayerController.OnPlayerFire += HandleNeedleFired;
            PlayerController.OnPlayerLevelUp += HandlePlayerLevel;
            CoinController.OnPlayerPickup += HandleCashCollected;
            EnemyController.OnBubblePopped += HandleBubblePopped;
        }

        private void OnDisable()
        {
            PlayerController.OnPlayerDamageTaken -= HandleDamageTaken;
            PlayerController.OnPlayerFire -= HandleNeedleFired;
            PlayerController.OnPlayerLevelUp -= HandlePlayerLevel;
            CoinController.OnPlayerPickup -= HandleCashCollected;
            EnemyController.OnBubblePopped -= HandleBubblePopped;
        }

        private void Update()
        {
            _gameDuration += Time.deltaTime;
        }

        private void HandleDamageTaken(int damage)
        {
            _damageTaken += damage;
        }

        private void HandleNeedleFired()
        {
            _needlesFired++;
        }

        private void HandleAbilitiesUsed()
        {
            _abilitiesUsed++;
        }

        private void HandleNewWave()
        {
            _enemyWave++;
        }

        private void HandleCashCollected(int cash)
        {
            _cashCollected += cash;
        }

        private void HandlePlayerLevel()
        {
            _playerLevel++;
        }

        private void HandleBubblePopped(int xp)
        {
            _playerExperience += xp;
            _bubblesPopped++;
        }
    }
}
