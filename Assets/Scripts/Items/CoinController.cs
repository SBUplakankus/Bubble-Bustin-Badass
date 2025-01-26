using System;
using UnityEngine;

namespace Items
{
    public class CoinController : MonoBehaviour
    {
        public static event Action<int> OnPlayerPickup;
        public int coinValue;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            OnPlayerPickup?.Invoke(coinValue);
            gameObject.SetActive(false);
        }
    }
}
