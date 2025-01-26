using System;
using Enemies;
using TMPro;
using UnityEngine;

namespace UI
{
    public class WaveDisplay : MonoBehaviour
    {
        public TMP_Text waveNum, bubbleNum;

        private void OnEnable()
        {
            WaveSpawner.OnWaveStart += HandleNewWave;
            WaveSpawner.OnBubbleCountUpdate += HandleBubbleCountUpdate;
        }
        
        private void OnDisable()
        {
            WaveSpawner.OnWaveStart -= HandleNewWave;
            WaveSpawner.OnBubbleCountUpdate -= HandleBubbleCountUpdate;
        }

        private void HandleNewWave(int wave, int bubbles)
        {
            waveNum.text = wave.ToString();
        }

        private void HandleBubbleCountUpdate(int bubbles)
        {
            bubbleNum.text = bubbles.ToString();
        }
    }
}
