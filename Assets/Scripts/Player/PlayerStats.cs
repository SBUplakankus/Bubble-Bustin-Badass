using System;
using Enemies;
using Items;
using UnityEngine;

namespace Player
{
    public class PlayerStats : MonoBehaviour
    {
        [Header("Game Stats")] 
        public float gameDuration;
        public int bubblesPopped;
        public int damageTaken;
        public int needlesFired;
        public int abilitiesUsed;
        public int playerLevel;
        public int playerExperience;
        public int cashCollected;
        public int enemyWave;
        public int dashesUsed;

        private void OnEnable()
        {
            PlayerController.OnPlayerDamageTaken += HandleDamageTaken;
            PlayerController.OnPlayerFire += HandleNeedleFired;
            PlayerController.OnPlayerLevelUp += HandlePlayerLevel;
            CoinController.OnPlayerPickup += HandleCashCollected;
            EnemyController.OnBubblePopped += HandleBubblePopped;
            PlayerController.OnAbilityFire += HandleAbilitiesUsed;
            PlayerController.OnPlayerDash += HandlePlayerDash;
            WaveSpawner.OnWaveStart += HandleNewWave;

        }

        private void OnDisable()
        {
            PlayerController.OnPlayerDamageTaken -= HandleDamageTaken;
            PlayerController.OnPlayerFire -= HandleNeedleFired;
            PlayerController.OnPlayerLevelUp -= HandlePlayerLevel;
            CoinController.OnPlayerPickup -= HandleCashCollected;
            EnemyController.OnBubblePopped -= HandleBubblePopped;
            PlayerController.OnAbilityFire -= HandleAbilitiesUsed;
            PlayerController.OnPlayerDash -= HandlePlayerDash;
            WaveSpawner.OnWaveStart -= HandleNewWave;
        }

        private void Update()
        {
            gameDuration += Time.deltaTime;
        }

        private void HandlePlayerDash()
        {
            dashesUsed++;
        }
        
        private void HandleDamageTaken(int damage)
        {
            damageTaken += damage;
        }

        private void HandleNeedleFired()
        {
            needlesFired++;
        }

        private void HandleAbilitiesUsed()
        {
            abilitiesUsed++;
        }

        private void HandleNewWave(int a, int b)
        {
            enemyWave++;
        }

        private void HandleCashCollected(int cash)
        {
            cashCollected += cash;
        }

        private void HandlePlayerLevel()
        {
            playerLevel++;
        }

        private void HandleBubblePopped(int xp)
        {
            playerExperience += xp;
            bubblesPopped++;
        }

        public PlayerStats GetPlayerStats()
        {
            return this;
        }
    }
}
