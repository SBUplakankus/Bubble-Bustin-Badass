using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private float _movementSpeed;
        private const float RotationSpeed = 600f;
        private const float DashDuration = 0.6f;
        private Rigidbody _rb;
        private Vector2 _movementInput;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (!(Time.timeScale > 0)) return;
            
            _movementInput.x = Input.GetAxisRaw("Horizontal");
            _movementInput.y = Input.GetAxisRaw("Vertical");
            
            _movementInput.Normalize();
            
            var movement = new Vector3(_movementInput.x, 0f, _movementInput.y) * (_movementSpeed * Time.deltaTime);
            
            _rb.linearVelocity = new Vector3(movement.x / Time.deltaTime, _rb.linearVelocity.y, movement.z / Time.deltaTime);

            if (!(_movementInput.sqrMagnitude > 0f)) return;
            var targetRotation = Quaternion.LookRotation(new Vector3(_movementInput.x, 0f, _movementInput.y));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
        }

        public void SetMovementSpeed(float speed)
        {
            _movementSpeed = speed;
        }
        
        /// <summary>
        /// Dash the player forward based on the force
        /// </summary>
        /// <param name="dashDistance">Distance to Dash</param>
        public void Dash(int dashDistance)
        {
            StartCoroutine(DashCoroutine(dashDistance));
        }
        
        private IEnumerator DashCoroutine(int dashDistance)
        {
            var startPosition = transform.position;
            var dashDirection = transform.forward;
            var targetPosition = startPosition + dashDirection * dashDistance;

            var elapsedTime = 0f;

            while (elapsedTime < DashDuration)
            {
                elapsedTime += Time.deltaTime;
                var t = elapsedTime / DashDuration;

                // Smoothly move the player towards the target position
                transform.position = Vector3.Lerp(startPosition, targetPosition, t);

                yield return null;
            }
        }
    }
}