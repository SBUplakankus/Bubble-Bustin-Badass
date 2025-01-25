using System;
using Player;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayerLevelDisplay : MonoBehaviour
    {
        public TMP_Text levelNum, expNum;

        private void OnEnable()
        {
            PlayerController.OnPlayerLevelUpdate += HandleLevelUpdate;
            PlayerController.OnPlayerExpUpdate += HandleExpUpdate;
        }
        
        private void OnDisable()
        {
            PlayerController.OnPlayerLevelUpdate -= HandleLevelUpdate;
            PlayerController.OnPlayerExpUpdate -= HandleExpUpdate;
        }

        private void HandleLevelUpdate(int level)
        {
            levelNum.text = level.ToString();
        }

        private void HandleExpUpdate(int exp)
        {
            expNum.text = exp.ToString();
        }
    }
}
