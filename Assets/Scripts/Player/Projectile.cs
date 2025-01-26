using System;
using Enemies;
using UnityEngine;

namespace Player
{
    public class Projectile : MonoBehaviour
    {
        private const float Speed = 10f;
        private int _damage;
        private int _strength;
        private Rigidbody _rb;

        public static event Action<GameObject> OnNeedleHit; 

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            _rb.linearVelocity = transform.forward * Speed;
        }
        
        /// <summary>
        /// Set the variables for the projectiles before firing
        /// </summary>
        /// <param name="damage">Projectile Damage</param>
        /// <param name="strength">Projectile Penetration Strength</param>
        public void SetProjectileParameters(int damage, int strength)
        {
            _damage = damage;
            _strength = strength;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Bubble"))
            {
                other.GetComponent<EnemyHitDetection>().BubbleTakeDamage(_damage);
                _strength--;
                if (_strength <= 0)
                {
                    OnNeedleHit?.Invoke(gameObject);
                }
                
            }
            else
            {
                OnNeedleHit?.Invoke(gameObject);
            }
        }
    }
}
