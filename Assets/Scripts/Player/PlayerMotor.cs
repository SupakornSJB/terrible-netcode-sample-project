using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Components;

public class PlayerMotor : NetworkBehaviour
{
    private const float SPRINT_SPEED = 6.0f;
    private const float WALKING_SPEED = 2.0f;

    /* private CharacterController controller; */
    private new Rigidbody rigidbody;
    private Vector3 playerVelocity;
    private float speed;
    private float gravity = -9.8f;
    private bool isGrounded;
    private float jumpHeight = 4.0f;

    [SerializeField] private Vector2 defaultPositionRange = new Vector2(-4, -4);

    void Start()
    {
        speed = WALKING_SPEED;
        rigidbody = GetComponent<Rigidbody>();
        /* controller = GetComponent<CharacterController>(); */
        transform.position = new Vector3(Random.Range(defaultPositionRange.x, defaultPositionRange.y), 2,
            Random.Range(defaultPositionRange.x, defaultPositionRange.y));
    }

    void Update()
    {
        /* isGrounded = controller.isGrounded; */
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        rigidbody.AddForce(transform.TransformDirection(moveDirection) * speed * 10);

        if (rigidbody.velocity.magnitude > speed)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * speed;
        }

        /* controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime); */


        /* playerVelocity.y += gravity * Time.deltaTime; */
        /* if (isGrounded && playerVelocity.y < 0) */
        /* { */
        /*     playerVelocity.y = -2.0f; */
        /* } */
        /* controller.Move(playerVelocity * Time.deltaTime); */
    }

    public void Jump()
    {
        float startVel = Mathf.Sqrt(-2 * gravity * jumpHeight);
        rigidbody.AddForce(Vector3.up * startVel, ForceMode.VelocityChange);
        /* if (isGrounded) */
        /* { */
        /*     playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity); */
        /* } */
    }

    public void SetIsSprinting(bool sprinting)
    {
        speed = sprinting ? SPRINT_SPEED : WALKING_SPEED;
    }
}
