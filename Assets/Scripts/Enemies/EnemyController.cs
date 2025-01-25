using System;
using Player;
using SOs;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Enemies
{
    public class EnemyController : MonoBehaviour
    {
        [Header("Enemy Stats")] 
        private int _enemyHealth;
        private int _enemyMaxHealth;
        private int _enemyDamage;
        private int _enemyXpGiven;
        
        [Header("Enemy Info")] 
        [SerializeField] private GameObject[] bubbleBodies;
        [SerializeField] private NavMeshAgent[] bubbleAgents;
        private NavMeshAgent _navMeshAgent;
        private Transform _player;

        public static event Action<int> OnBubblePopped;
        public static event Action<GameObject> OnBubbleReset;
        public static event Action<int> OnPlayerDamaged;


        private void Update()
        {
            if (!_navMeshAgent) return;
            
            _navMeshAgent.SetDestination(_player.position);
            if(_navMeshAgent.pathPending) return;
            if (_navMeshAgent.remainingDistance is >= 0.5f or <= 0) return;
            
            DamagePlayer();

        }

        /// <summary>
        /// Spawn in a new enemy based on one of the scriptable objects
        /// </summary>
        /// <param name="enemySo">Enemy Scriptable Object</param>
        /// <param name="target">Enemy Target</param>
        /// <param name="spawnPos">Enemy Spawn</param>
        public void SetEnemy(EnemySO enemySo, Transform target, Transform spawnPos)
        {
            foreach (var body in bubbleBodies)
            {
                body.SetActive(false);
            }

            bubbleBodies[enemySo.bubbleBodyIndex].transform.position = spawnPos.position;
            bubbleBodies[enemySo.bubbleBodyIndex].SetActive(true);
            _navMeshAgent = bubbleAgents[enemySo.bubbleBodyIndex];
            
            _enemyHealth = enemySo.health;
            _enemyMaxHealth = _enemyHealth;
            _enemyDamage = enemySo.damage;
            _enemyXpGiven = enemySo.xpGiven;
            
            _navMeshAgent.speed = enemySo.speed;
            _player = target;
        }
        
        /// <summary>
        /// Do Damage to the Enemy
        /// </summary>
        /// <param name="damage">Damage Amount</param>
        public void TakeDamage(int damage)
        {
            _enemyHealth -= damage;
            if (_enemyHealth > 0) return;
            BubblePopped();
        }
        
        private void BubblePopped()
        {
            OnBubblePopped?.Invoke(_enemyXpGiven);
            OnBubbleReset?.Invoke(gameObject);
        }

        private void DamagePlayer()
        {
            OnPlayerDamaged?.Invoke(_enemyDamage);
            OnBubbleReset?.Invoke(gameObject);
        }
    }
}
