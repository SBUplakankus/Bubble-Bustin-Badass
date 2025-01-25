using System;
using Enemies;
using UnityEngine;

namespace Player
{
    public class Projectile : MonoBehaviour
    {
        private const float Speed = 10f;
        private int _damage;
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

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Hit");
            if (other.gameObject.CompareTag("Bubble"))
            {
                other.GetComponent<EnemyController>().TakeDamage(_damage);
                OnNeedleHit?.Invoke(gameObject);
                
            }
            else
            {
                OnNeedleHit?.Invoke(gameObject);
            }
        }
    }
}
