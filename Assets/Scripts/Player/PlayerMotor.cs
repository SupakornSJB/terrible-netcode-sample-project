using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player
{
    public class PlayerMotor : NetworkBehaviour
    {
        private const float SprintSpeed = 6.0f;
        private const float WalkingSpeed = 2.0f;
        private const float Gravity = -9.8f;
        private const float JumpHeight = 4.0f;

        private new Rigidbody rigidbody;
        private Vector3 playerVelocity;
        private float speed;
        private bool isGrounded;

        [SerializeField] private Vector2 defaultPositionRange = new Vector2(-4, -4);

        public void Start()
        {
            speed = WalkingSpeed;
            rigidbody = GetComponent<Rigidbody>();
            transform.position = new Vector3(Random.Range(defaultPositionRange.x, defaultPositionRange.y), 2,
                Random.Range(defaultPositionRange.x, defaultPositionRange.y));
        }

        public void ProcessMove(Vector2 input)
        {
            var moveDirection = Vector3.zero;
            moveDirection.x = input.x;
            moveDirection.z = input.y;
            rigidbody.AddForce(transform.TransformDirection(moveDirection) * (speed * 10));

            var velocity = rigidbody.velocity;
            var planeVector = new Vector3(velocity.x, 0, velocity.z);
            if (!(planeVector.sqrMagnitude > speed * speed)) return;
            var tempY = rigidbody.velocity.y;
            planeVector = planeVector.normalized * speed;
            planeVector.y = velocity.y;
            rigidbody.velocity = planeVector;
        }

        public void Jump()
        {
            if (!IsGrounded()) return;
            var startVel = Mathf.Sqrt(-2 * Gravity * JumpHeight);
            rigidbody.AddForce(Vector3.up * startVel, ForceMode.VelocityChange);
        }

        public void SetIsSprinting(bool sprinting)
        {
            speed = sprinting ? SprintSpeed : WalkingSpeed;
        }

        private bool IsGrounded()
        {
            return rigidbody.velocity.y == 0;
        }
    }
}
