using System;
using Player;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AbilityDisplay : MonoBehaviour
    {
        public Slider needle, spray, dash;

        private void OnEnable()
        {
            PlayerController.OnAbilityFire += ResetSpraySlider;
            PlayerController.OnPlayerFire += ResetNeedleSlider;
            PlayerController.OnPlayerDash += ResetDashSlider;
        }
        
        private void OnDisable()
        {
            PlayerController.OnAbilityFire -= ResetSpraySlider;
            PlayerController.OnPlayerFire -= ResetNeedleSlider;
            PlayerController.OnPlayerDash -= ResetDashSlider;
        }

        /// <summary>
        /// Set the initial values for the ability cooldown sliders
        /// </summary>
        /// <param name="needleMax">Needle Cooldown</param>
        /// <param name="sprayMax">Spray Cooldown</param>
        /// <param name="dashMax">Dash Cooldown</param>
        public void SetInitialSliderValues(float needleMax, float sprayMax, float dashMax)
        {
            needle.maxValue = needleMax;
            needle.value = needleMax;

            spray.maxValue = sprayMax;
            spray.value = sprayMax;

            dash.maxValue = dashMax;
            dash.value = dashMax;
        }

        private void ResetNeedleSlider()
        {
            needle.value = 0;
            Tween.UISliderValue(needle, needle.maxValue, needle.maxValue);
        }

        private void ResetSpraySlider()
        {
            spray.value = 0;
            Tween.UISliderValue(spray, spray.maxValue, spray.maxValue);
        }

        private void ResetDashSlider()
        {
            dash.value = 0;
            Tween.UISliderValue(dash, dash.maxValue, dash.maxValue);
        }
    }
}
