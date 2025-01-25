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
            if (other.gameObject.CompareTag("Bubble"))
            {
                other.GetComponent<EnemyController>().TakeDamage(_damage);
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
