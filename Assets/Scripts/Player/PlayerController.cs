using System;
using System.Collections;
using Enemies;
using Items;
using SOs;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        #region Player Variables
        
        [Header("Player Attributes")] 
        public int playerHealth;
        public int playerHealthRegen;
        public int playerSpeed;
        public int playerDamage;
        public int projectileStrength;
        public int dashDistance;
        public int playerMaxHealth;
        
        private const float DashCooldown = 3f;
        private float _dashTimer;
        private bool _dashReady;
        private const float FireCooldown = 0.6f;
        private float _needleTimer;
        private bool _fireReady;
        private const int RegenInterval = 1;
        private bool _regenerating;
        
        [Header("Player Abilities")]
        private const float AbilityCooldown = 1.5f;
        public int abilityDamage;
        private float _abilityTimer;
        private bool _abilityReady;

        [Header("Player Stats")] 
        private int _playerLevel;
        private int _playerExperience;
        private int _levelUpThreshold;
        private int _playerCash;

        [Header("Script References")] 
        private PlayerMovement _playerMovement;
        private PlayerFiring _playerFiring;
        public AbilityDisplay abilityDisplay;
        
        #endregion

        #region Events

        public static event Action OnPlayerLevelUp;
        public static event Action<int> OnPlayerDamageTaken;
        public static event Action OnPlayerFire;
        public static event Action OnAbilityFire;
        public static event Action OnPlayerDeath;
        public static event Action<int, int> OnPlayerLevelUpdate;
        public static event Action<int> OnPlayerExpUpdate;
        public static event Action OnPlayerDash;
        public static event Action<int> OnCashUpdate;
        
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
            SetPlayerLevel(0);
            SetPlayerExperience(0);
            SetPlayerCash(0);
            SetLevelUpThreshold(100);
            SetPlayerMaxHealth(playerHealth);
            abilityDisplay.SetInitialSliderValues(FireCooldown, AbilityCooldown, DashCooldown);
        }

        private void OnEnable()
        {
            CoinController.OnPlayerPickup += AddCash;
            EnemyController.OnBubblePopped += AddPlayerExperience;
            EnemyController.OnPlayerDamaged += PlayerTakeDamage;
            CardDisplay.OnCardSelected += HandleUpgradeSelection;
            CoinController.OnPlayerPickup += AddCash;
        }
        
        private void OnDisable()
        {
            CoinController.OnPlayerPickup -= AddCash;
            EnemyController.OnBubblePopped -= AddPlayerExperience;
            EnemyController.OnPlayerDamaged -= PlayerTakeDamage;
            CardDisplay.OnCardSelected -= HandleUpgradeSelection;
            CoinController.OnPlayerPickup -= AddCash;
        }

        private void Update()
        {
            if (playerHealth < playerMaxHealth && !_regenerating)
            {
                StartCoroutine(RegenerateHealth());
            }
            
            // Spray Logic
            if (!_abilityReady)
            {
                _abilityTimer -= Time.deltaTime;
                if (_abilityTimer <= 0)
                {
                    _abilityReady = true;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _playerFiring.FireAbility(abilityDamage);
                    _abilityTimer = AbilityCooldown;
                    OnAbilityFire?.Invoke();
                    _abilityReady = false;

                }
            }
            
            // Needle Logic
            if (!_fireReady)
            {
                _needleTimer -= Time.deltaTime;
                if (_needleTimer <= 0)
                {
                    _fireReady = true;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _playerFiring.FireNeedle(playerDamage, projectileStrength);
                    OnPlayerFire?.Invoke();
                    _needleTimer = FireCooldown;
                    _fireReady = false;
                }
            }
            
            // Dash Logic
            if (!_dashReady)
            {
                _dashTimer -= Time.deltaTime;
                if (_dashTimer <= 0)
                {
                    _dashReady = true;
                }
            }
            else
            {
                if (!Input.GetKeyDown(KeyCode.R)) return;
                OnPlayerDash?.Invoke();
                _playerMovement.Dash(dashDistance);
                _dashTimer = DashCooldown;
                _dashReady = false;
            }
        }

        #endregion

        #region Variable Modifiers

        private void HandleUpgradeSelection(UpgradeSO upg, int amount)
        {
            var upgradeType = upg.upgradeType;
            switch (upgradeType)
            {
                case UpgradeSO.UpgradeType.Health:
                    AddPlayerHealth(amount);
                    break;
                case UpgradeSO.UpgradeType.Speed:
                    AddPlayerSpeed(amount);
                    break;
                case UpgradeSO.UpgradeType.NeedleDamage:
                    AddPlayerDamage(amount);
                    break;
                case UpgradeSO.UpgradeType.AbilityDamage:
                    AddSprayDamage(amount);
                    break;
                case UpgradeSO.UpgradeType.LeapDistance:
                    AddDashDistance(amount);
                    break;
                case UpgradeSO.UpgradeType.NeedleStrength:
                    AddNeedleStrength(amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
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
            _levelUpThreshold *= 2;
            OnPlayerLevelUp?.Invoke();
            OnPlayerLevelUpdate?.Invoke(_playerLevel, _levelUpThreshold);
        }

        private void AddPlayerDamage(int damageAdded)
        {
            playerDamage += damageAdded;
        }

        private void AddPlayerSpeed(int speedAdded)
        {
            playerSpeed += speedAdded;
        }

        private void AddDashDistance(int amount)
        {
            dashDistance += amount;
        }

        private void AddNeedleStrength(int amount)
        {
            projectileStrength += amount;
        }
        
        private void AddSprayDamage(int amount)
        {
            abilityDamage += amount;
        }
        private void AddCash(int cashAdded)
        {
            _playerCash += cashAdded;
            OnCashUpdate?.Invoke(_playerCash);
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
            playerMaxHealth = health;
            playerHealth = playerMaxHealth;
            _regenerating = false;
        }

        private void AddPlayerHealth(int health)
        {
            playerMaxHealth += health;
            playerHealth = playerMaxHealth;
        }

        private IEnumerator RegenerateHealth()
        {
            _regenerating = true;
            if (playerHealth + playerHealthRegen > playerMaxHealth)
            {
                playerHealth = playerMaxHealth;
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
