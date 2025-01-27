using System;
using System.Globalization;
using Enemies;
using Player;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameOverDisplay : MonoBehaviour
    {
        public PlayerStats playerStats;
        public TMP_Text score, wave, popped, needles, abilities, dashes, cash, damage, experience, time;

        private void OnEnable()
        {
            PlayerController.OnPlayerDeath += SetFinalScore;
        }
        
        private void OnDisable()
        {
            PlayerController.OnPlayerDeath -= SetFinalScore;
        }

        private void SetFinalScore()
        {
            var finalScore = 0;
            wave.text = playerStats.enemyWave.ToString();
            finalScore += playerStats.enemyWave * 1000;
            popped.text = playerStats.bubblesPopped.ToString();
            finalScore += playerStats.bubblesPopped * 50;
            needles.text = playerStats.needlesFired.ToString();
            abilities.text = playerStats.abilitiesUsed.ToString();
            dashes.text = playerStats.dashesUsed.ToString();
            cash.text = "$" + playerStats.cashCollected;
            finalScore += playerStats.cashCollected * 10;
            damage.text = playerStats.damageTaken.ToString();
            finalScore -= playerStats.damageTaken * 10;
            experience.text = playerStats.playerExperience.ToString("N0");
            finalScore += playerStats.playerExperience * 10;
            
            var gameTime = TimeSpan.FromSeconds(playerStats.gameDuration);
            time.text = $"{(int)gameTime.TotalMinutes:00}:{gameTime.Seconds:00}";
            finalScore += (int)playerStats.gameDuration;

            score.text = finalScore.ToString("N0");

        }
    }
}
