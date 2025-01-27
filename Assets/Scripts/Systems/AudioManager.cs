using System;
using Enemies;
using Player;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Systems
{
    public class AudioManager : MonoBehaviour
    {
        public AudioSource playerAudio, levelAudio, sfxAudio, bubbleAudio;
        
        public AudioClip needle, spray, shopOpen, levelUp, waveIncoming, cardDisplay;

        private void OnEnable()
        {
            PlayerController.OnAbilityFire += PlayAbilityAudio;
            PlayerController.OnPlayerFire += PlayNeedleAudio;
            PlayerController.OnPlayerLevelUp += PlayLevelUp;
            EnemyController.OnPopNoise += PlayBubblePop;
            WaveSpawner.OnWaveStart += PlayNewWave;
            CardManager.OnCardsDisplay += PlayCardDisplayAudio;
        }

        private void OnDisable()
        {
            PlayerController.OnAbilityFire -= PlayAbilityAudio;
            PlayerController.OnPlayerFire -= PlayNeedleAudio;
            PlayerController.OnPlayerLevelUp -= PlayLevelUp;
            EnemyController.OnPopNoise -= PlayBubblePop;
            WaveSpawner.OnWaveStart -= PlayNewWave;
            CardManager.OnCardsDisplay += PlayCardDisplayAudio;
        }

        private void PlayCardDisplayAudio()
        {
            levelAudio.PlayOneShot(cardDisplay);
        }
        
        private void PlayAbilityAudio()
        {
            playerAudio.PlayOneShot(spray);
        }

        private void PlayNeedleAudio()
        {
            playerAudio.PlayOneShot(needle);
        }

        private void PlayLevelUp()
        {
            sfxAudio.PlayOneShot(levelUp);
        }

        private void PlayNewWave(int a, int b)
        {
            sfxAudio.PlayOneShot(waveIncoming);
        }

        private void PlayBubblePop(AudioClip pop)
        {
            bubbleAudio.pitch = Random.Range(0.9f, 1.1f);
            bubbleAudio.PlayOneShot(pop);
        }
    }
}
