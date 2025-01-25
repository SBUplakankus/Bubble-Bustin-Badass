using System;
using Player;
using UnityEngine;

namespace Enemies
{
    public class EnemyHitDetection : MonoBehaviour
    {
        [SerializeField] private EnemyController parentController;
        
        /// <summary>
        /// Do damage to the bubble on hit detection
        /// </summary>
        /// <param name="damage">Damage Amount</param>
        public void BubbleTakeDamage(int damage)
        {
            Debug.Log(damage);
            parentController.TakeDamage(damage);
        }
    }
}
