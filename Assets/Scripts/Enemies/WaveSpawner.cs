using System;
using System.Collections;
using System.Collections.Generic;
using SOs;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class WaveSpawner : MonoBehaviour
    {
        [Header("Enemy Objects")] 
        public EnemySO[] enemyTypes;
        public GameObject bubblePrefab;
        public List<GameObject> inactiveBubblePool;
        private List<GameObject> _activeBubblePool;
        
        
        [Header("Wave Info")] 
        public int waveIndex;
        private const float SpawnInterval = 1f;
        private const int WaveInterval = 0;
        private int _activeBubbleTotal;

        [Header("Spawn Info")] 
        private Dictionary<int, int> _enemiesToSpawn;

        private bool _spawningWave;
        public Transform[] spawnPositions;
        public Transform player;

        public static event Action<int, int> OnWaveStart;
        public static event Action<int> OnBubbleCountUpdate; 

        private void Awake()
        {
            waveIndex = 0;
            _activeBubbleTotal = 0;
            _spawningWave = false;
            _activeBubblePool = new List<GameObject>();
            _enemiesToSpawn = new Dictionary<int, int>
            {
                { 0, 0 },
                { 1, 0 },
                { 2, 0 },
                { 3, 0 },
                { 4, 0 }
            };
        }

        private void Update()
        {
            if (_activeBubbleTotal > 0 || _spawningWave) return;
            
            SpawnWave();
        }

        private void OnEnable()
        {
            EnemyController.OnBubbleReset += HandleBubbleReset;
        }

        private void OnDisable()
        {
            EnemyController.OnBubbleReset -= HandleBubbleReset;
        }

        private void HandleBubbleReset(GameObject bubble)
        {
            _activeBubbleTotal--;
            _activeBubblePool.Remove(bubble);
            inactiveBubblePool.Add(bubble);
            bubble.gameObject.SetActive(false);
            OnBubbleCountUpdate?.Invoke(_activeBubbleTotal);
        }

        private void SpawnWave()
        {
            _spawningWave = true;
            waveIndex++;
            UpdateBubblesToSpawn();
            StartCoroutine(WaveSpawnCoroutine());
        }

        private void UpdateBubblesToSpawn()
        {
            _enemiesToSpawn[0] += 5; // Basic
            if (waveIndex >= 2) _enemiesToSpawn[1] += 2; // Speed
            if (waveIndex >= 4 && waveIndex % 2 == 1) _enemiesToSpawn[2]++; // Tank
            if (waveIndex >= 9 && waveIndex % 2 == 0) _enemiesToSpawn[3]++; // Elite
            if (waveIndex >= 15 && waveIndex % 5 == 0) _enemiesToSpawn[4]++; // Boss
            
            for(var i = 0; i < enemyTypes.Length; i++)
            {
                _activeBubbleTotal += _enemiesToSpawn[i];
            }
        }
        
        private IEnumerator WaveSpawnCoroutine()
        {
            yield return new WaitForSeconds(WaveInterval);
            OnWaveStart?.Invoke(waveIndex, _activeBubbleTotal);
            
            foreach (var spawnType in _enemiesToSpawn)
            {
                var enemyType = spawnType.Key;
                var count = spawnType.Value;
                
                while (count > 0)
                {
                    GameObject bubbleBody;

                    var spawnIndex = Random.Range(0, spawnPositions.Length);

                    if (inactiveBubblePool.Count > 0)
                    {
                        bubbleBody = inactiveBubblePool[0];
                        inactiveBubblePool.RemoveAt(0);
                        bubbleBody.SetActive(true);
                        bubbleBody.GetComponent<EnemyController>().SetEnemy(enemyTypes[enemyType], player, spawnPositions[spawnIndex]);
                    }
                    else
                    {
                        bubbleBody = Instantiate(bubblePrefab, spawnPositions[spawnIndex]);
                        var bubbleCode = bubbleBody.GetComponent<EnemyController>();
                        bubbleCode.SetEnemy(enemyTypes[enemyType], player, spawnPositions[spawnIndex]);
                        Debug.Log("New Bubble Needed");
                    }
                    
                    _activeBubblePool.Add(bubbleBody);
                    count--;

                    yield return new WaitForSeconds(SpawnInterval);
                }
            }

            _spawningWave = false;

        }
    }
    
}
