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
        private readonly Vector3 _baseNeedleSize = new Vector3(0.19f, 0.19f, 0.19f);

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
            needle.transform.localScale = _baseNeedleSize;
            needle.SetActive(false);
        }
        
        /// <summary>
        /// Fire the needle projectile from the hedgehogs back
        /// </summary>
        public void FireNeedle(int damage, int strength)
        {
            GameObject needleToFire;
            
            if (inactiveNeedlesPool.Count > 0)
            {
                needleToFire = inactiveNeedlesPool[0];
                needleToFire.transform.position = baseFirePoint.transform.position;
                needleToFire.transform.rotation = baseFirePoint.transform.rotation;
                needleToFire.SetActive(true);
            }
            else
            {
                needleToFire = Instantiate(needlePrefab, baseFirePoint.transform.position, baseFirePoint.transform.rotation);
            }
            
            needleToFire.GetComponent<Projectile>().SetProjectileParameters(damage, strength);
            _activeNeedlesPool.Add(needleToFire);
            inactiveNeedlesPool.Remove(needleToFire);
        }
        
        /// <summary>
        /// Fire the ability needles
        /// </summary>
        /// <param name="damage">Ability Needle Damage</param>
        public void FireAbility(int damage)
        {
            foreach (var point in abilityFirePoints)
            {
                GameObject needleToFire;
            
                if (inactiveNeedlesPool.Count > 0)
                {
                    needleToFire = inactiveNeedlesPool[0];
                    needleToFire.transform.position = point.transform.position;
                    needleToFire.transform.rotation = point.transform.rotation;
                    needleToFire.SetActive(true);
                }
                else
                {
                    needleToFire = Instantiate(needlePrefab, point.transform.position, point.transform.rotation);
                }

                needleToFire.transform.localScale /= 2;
                needleToFire.GetComponent<Projectile>().SetProjectileParameters(damage, 1);
                _activeNeedlesPool.Add(needleToFire);
                inactiveNeedlesPool.Remove(needleToFire);
            }
        }
    }
}
