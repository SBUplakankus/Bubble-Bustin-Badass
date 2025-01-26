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
            Time.timeScale = 1;
        }

        private void OnEnable()
        {
            PlayerController.OnPlayerLevelUp += HandleGamePause;
            UIController.OnGameResume += HandleGameResume;
        }
        private void OnDisable()
        {
            PlayerController.OnPlayerLevelUp -= HandleGamePause;
            UIController.OnGameResume -= HandleGameResume;
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
