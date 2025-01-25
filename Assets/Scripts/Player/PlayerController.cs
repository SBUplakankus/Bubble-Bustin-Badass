using System;
using System.Collections;
using Enemies;
using Items;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        #region Player Variables
        
        [Header("Player Attributes")] 
        [SerializeField] private int playerHealth;
        private int _playerMaxHealth;
        [SerializeField] private int playerHealthRegen;
        [SerializeField] private int playerSpeed;
        [SerializeField] private int playerDamage;
        [SerializeField] private float playerFireRate;
        private float _fireCooldown;
        private bool _fireReady;
        private const int RegenInterval = 1;
        private bool _regenerating;
        
        [Header("Player Abilities")]
        [SerializeField] private float abilityCooldown;
        [SerializeField] private int abilityDamage;
        private float _abilityTimer;
        private bool _abilityReady;

        [Header("Player Stats")] 
        private int _playerLevel;
        private int _playerExperience;
        private float _levelUpThreshold;
        private int _playerCash;

        [Header("Script References")] 
        private PlayerMovement _playerMovement;
        private PlayerFiring _playerFiring;
        
        #endregion

        #region Events

        public static event Action OnPlayerLevelUp;
        public static event Action<int> OnPlayerDamageTaken;
        public static event Action OnPlayerFire;
        public static event Action OnPlayerDeath;
        public static event Action<int> OnPlayerLevelUpdate;
        public static event Action<int> OnPlayerExpUpdate;
        
        #endregion
        
        #region Game Logic

        private void Awake()
        {
            _playerFiring = GetComponent<PlayerFiring>();
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void Start()
        {
            _playerMovement.SetMovementSpeed(playerSpeed);
            SetPlayerLevel(1);
            SetPlayerExperience(0);
            SetPlayerCash(0);
            SetLevelUpThreshold(100);
            SetPlayerMaxHealth(playerHealth);
        }

        private void OnEnable()
        {
            CoinController.OnPlayerPickup += AddCash;
            EnemyController.OnBubblePopped += AddPlayerExperience;
            EnemyController.OnPlayerDamaged += PlayerTakeDamage;
        }
        
        private void OnDisable()
        {
            CoinController.OnPlayerPickup -= AddCash;
            EnemyController.OnBubblePopped -= AddPlayerExperience;
            EnemyController.OnPlayerDamaged -= PlayerTakeDamage;
        }

        private void Update()
        {
            if (playerHealth < _playerMaxHealth && !_regenerating)
            {
                StartCoroutine(RegenerateHealth());
            }

            if (!_abilityReady)
            {
                _abilityTimer -= Time.deltaTime;
                if (_abilityTimer <= 0)
                {
                    _abilityReady = true;
                }
            }

            if (!_fireReady)
            {
                _fireCooldown -= Time.deltaTime;
                if (_fireCooldown <= 0)
                {
                    _fireReady = true;
                }
            }
            else
            {
                if (!Input.GetKeyDown(KeyCode.Space)) return;
                
                _playerFiring.FireNeedle();
                OnPlayerFire?.Invoke();
                _fireReady = false;
            }
        }

        #endregion

        #region Variable Modifiers
        
        private void SetPlayerLevel(int level)
        {
            _playerLevel = level;
        }
        
        private void SetPlayerExperience(int xp)
        {
            _playerExperience = xp;
        }
        
        private void SetLevelUpThreshold(int threshold)
        {
            _levelUpThreshold = threshold;
        }
        
        private void SetPlayerCash(int cash)
        {
            _playerCash = cash;
        }
        
        private void AddPlayerExperience(int xp)
        {
            _playerExperience += xp;
            OnPlayerExpUpdate?.Invoke(_playerExperience);
            if (_playerExperience < _levelUpThreshold) return;
            LevelUp();
        }
        
        private void LevelUp()
        {
            _playerLevel++;
            _levelUpThreshold *= 1.5f;
            OnPlayerLevelUp?.Invoke();
            OnPlayerLevelUpdate?.Invoke(_playerLevel);
        }

        private void AddPlayerDamage(int damageAdded)
        {
            playerDamage += damageAdded;
        }

        private void AddPlayerSpeed(int speedAdded)
        {
            playerSpeed += speedAdded;
        }

        private void ImproveFireRate(float fireRateChange)
        {
            playerFireRate /= fireRateChange;
        }

        private void AddCash(int cashAdded)
        {
            _playerCash += cashAdded;
        }

        private void AddHealthRegen(int regenAdded)
        {
            playerHealthRegen += regenAdded;
        }

        private void PlayerTakeDamage(int damage)
        {
            playerHealth -= damage;
            OnPlayerDamageTaken?.Invoke(damage);
            if (playerHealth > 0) return;
            OnPlayerDeath?.Invoke();
        }

        private void SetPlayerMaxHealth(int health)
        {
            _playerMaxHealth = health;
            _regenerating = false;
        }

        private void AddPlayerHealth(int health)
        {
            _playerMaxHealth += health;
        }

        private IEnumerator RegenerateHealth()
        {
            _regenerating = true;
            if (playerHealth + playerHealthRegen > _playerMaxHealth)
            {
                playerHealth = _playerMaxHealth;
            }
            else
            {
                playerHealth += playerHealthRegen;
            }
            
            yield return new WaitForSeconds(RegenInterval);
            _regenerating = false;
        }
        
        #endregion
    }
}
