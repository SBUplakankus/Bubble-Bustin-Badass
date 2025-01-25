using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerFiring : MonoBehaviour
    {
        [Header("Base Attack")]
        public GameObject needlePrefab;
        public Transform baseFirePoint;

        [Header("Ability Attack")]
        public Transform[] abilityFirePoints;

        [Header("Needle Pool")] 
        public List<GameObject> inactiveNeedlesPool;
        private List<GameObject> _activeNeedlesPool;

        private void OnEnable()
        {
            _activeNeedlesPool = new List<GameObject>();
            Projectile.OnNeedleHit += HandleNeedleHit;
        }

        private void OnDisable()
        {
            Projectile.OnNeedleHit -= HandleNeedleHit;
        }

        private void HandleNeedleHit(GameObject needle)
        {
            _activeNeedlesPool.Remove(needle);
            inactiveNeedlesPool.Add(needle);
            needle.SetActive(false);
        }
        
        /// <summary>
        /// Fire the needle projectile from the hedgehogs back
        /// </summary>
        public void FireNeedle()
        {
            if (inactiveNeedlesPool.Count > 0)
            {
                var needleToFire = inactiveNeedlesPool[0];
                needleToFire.transform.position = baseFirePoint.transform.position;
                needleToFire.transform.rotation = baseFirePoint.transform.rotation;
                needleToFire.SetActive(true);
                _activeNeedlesPool.Add(needleToFire);
                inactiveNeedlesPool.Remove(needleToFire);
            }
            else
            {
                var needleToFire = Instantiate(needlePrefab, baseFirePoint.transform.position, baseFirePoint.transform.rotation);
                _activeNeedlesPool.Add(needleToFire);
                inactiveNeedlesPool.Remove(needleToFire);
            }
        }
    }
}
