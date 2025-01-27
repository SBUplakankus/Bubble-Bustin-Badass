using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class ButtonAudio : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
    {
        private AudioSource _audioSource;
        public AudioClip hover, select;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _audioSource.PlayOneShot(hover);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _audioSource.PlayOneShot(select);
        }
    }
}
