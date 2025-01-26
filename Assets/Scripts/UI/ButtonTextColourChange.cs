using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class ButtonTextColourChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        
        [SerializeField] private TMP_Text buttonText;
        [SerializeField] private Color32[] textColours;
        
        
        private void OnEnable()
        {
            buttonText.color = textColours[0];
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            buttonText.color = textColours[1];
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            buttonText.color = textColours[0];
        }
    }
}
