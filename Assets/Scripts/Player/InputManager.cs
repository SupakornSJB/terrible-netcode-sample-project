using UnityEngine;

namespace Player
{
    public class InputManager : MonoBehaviour 
    {
        private PlayerInput playerInput;
        public PlayerInput.OnFootActions OnFoot;
        private PlayerMotor motor;
        private PlayerLook look;
        private PlayerShoot shoot;

        private void Awake()
        {
            playerInput = new PlayerInput();
            OnFoot = playerInput.OnFoot;
            motor = GetComponent<PlayerMotor>();
            look = GetComponent<PlayerLook>();
            shoot = GetComponent<PlayerShoot>();
            OnFoot.Fire.performed += (ctx) => shoot.ShootBullet();
            OnFoot.Jump.performed += (ctx) => motor.Jump();
            OnFoot.Sprint.performed += (ctx) => motor.SetIsSprinting(true);
            OnFoot.Sprint.canceled += (ctx) => motor.SetIsSprinting(false);
        }

        private void FixedUpdate()
        {
            motor.ProcessMove(OnFoot.Movement.ReadValue<Vector2>());
        }

        public void LateUpdate()
        {
            look.ProcessLook(OnFoot.Look.ReadValue<Vector2>());
        }

        private void OnEnable()
        {
            OnFoot.Enable();
        }

        private void OnDisable()
        {
            OnFoot.Disable();
        }
    }
}
