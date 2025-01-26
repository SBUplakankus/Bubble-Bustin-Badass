using System;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class ButtonGrow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private const float GrowSize = 1.04f;
        private const float AnimationDuration = 0.2f;
        private const Ease AnimationEase = Ease.OutCubic;

        [SerializeField] private TMP_Text buttonText;
        [SerializeField] private Color32[] textColours;

        private void OnEnable()
        {
            transform.localScale = Vector2.one;
            buttonText.color = textColours[0];
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Tween.Scale(transform, GrowSize, AnimationDuration, AnimationEase, useUnscaledTime: true);
            buttonText.color = textColours[1];
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Tween.Scale(transform, Vector2.one, AnimationDuration, AnimationEase, useUnscaledTime: true);
            buttonText.color = textColours[0];
        }
    }
}
