using System;
using Player;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayerStatsDisplay : MonoBehaviour
    {
        public PlayerController player;
        public TMP_Text health, speed, damage, strength, spray, dash;

        private void OnEnable()
        {
            UIController.OnGamePause += SetStatsDisplay;
        }
        
        private void OnDisable()
        {
            UIController.OnGamePause -= SetStatsDisplay;
        }

        /// <summary>
        /// Update the Stats Display with the players attributes
        /// </summary>
        private void SetStatsDisplay()
        {
            health.text = player.playerMaxHealth.ToString();
            speed.text = player.playerSpeed.ToString();
            damage.text = player.playerDamage.ToString();
            strength.text = player.projectileStrength.ToString();
            spray.text = player.abilityDamage.ToString();
            dash.text = player.dashDistance.ToString();
        }
    }
}
