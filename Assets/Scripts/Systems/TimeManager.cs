using System;
using Player;
using UI;
using UnityEngine;

namespace Systems
{
    public class TimeManager : MonoBehaviour
    {
        private void Start()
        {
            Time.timeScale = 0;
        }

        private void OnEnable()
        {
            PlayerController.OnPlayerLevelUp += HandleGamePause;
            PlayerController.OnPlayerDeath += HandleGamePause;
            UIController.OnGameResume += HandleGameResume;
            UIController.OnGamePause += HandleGamePause;
            UIController.OnGameUnpause += HandleGameResume;
        }
        private void OnDisable()
        {
            PlayerController.OnPlayerLevelUp -= HandleGamePause;
            PlayerController.OnPlayerDeath -= HandleGamePause;
            UIController.OnGameResume -= HandleGameResume;
            UIController.OnGamePause -= HandleGamePause;
            UIController.OnGameUnpause -= HandleGameResume;
        }

        private void HandleGamePause()
        {
            Time.timeScale = 0;
        }

        private void HandleGameResume()
        {
            Time.timeScale = 1;
        }
    }
}
