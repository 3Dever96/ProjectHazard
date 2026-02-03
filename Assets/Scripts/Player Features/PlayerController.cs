using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectHazard.PlayerFeatures
{
    [RequireComponent(typeof(NetworkObject)), RequireComponent(typeof(NetworkTransform)), RequireComponent(typeof(PlayerInput)), RequireComponent(typeof(CharacterController))]
    public class PlayerController : NetworkBehaviour
    {
        // Information
        CharacterController controller;
        PlayerInput input;

        bool canMove;

        Vector2 move;
        Vector3 lookDirection;

        bool isGrounded;

        [Header("Movement Variables")]
        [SerializeField]
        float moveSpeed;
        [SerializeField]
        float turnSpeed;
        [SerializeField]
        float stickForce;
        [SerializeField]
        float gravity;

        float currentSpeed;
        float verticalSpeed;

        void Awake()
        {
            controller = GetComponent<CharacterController>();
            input = GetComponent<PlayerInput>();

            lookDirection = transform.forward;
        }

        void Update()
        {
            canMove = !IsSpawned || IsOwner;

            isGrounded = verticalSpeed <= 0f && Physics.CheckSphere(transform.position, controller.radius, LayerMask.GetMask("Solid"));
        }

        void FixedUpdate()
        {
            if (canMove)
            {
                Vector3 direction = Vector3.right * move.x + Vector3.forward * move.y;
                direction.y = 0f;
                direction = direction.normalized;

                if (move != Vector2.zero)
                {
                    currentSpeed = moveSpeed;
                    lookDirection = direction;
                }
                else
                {
                    currentSpeed = 0f;
                }

                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lookDirection), turnSpeed * Time.deltaTime);

                if (isGrounded)
                {
                    verticalSpeed = stickForce;
                }
                else
                {
                    verticalSpeed += gravity * Time.deltaTime;
                }

                Vector3 velocity = currentSpeed * lookDirection;
                velocity.y = verticalSpeed;

                controller.Move(velocity * Time.deltaTime);
            }
        }

        void OnEnable()
        {
            input.onActionTriggered += OnAction;
        }

        void OnDisable()
        {
            input.onActionTriggered -= OnAction;
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            if (IsOwner)
            {
                input = GetComponent<PlayerInput>();
            }
        }

        void OnAction(InputAction.CallbackContext context)
        {
            if (context.action.name == "Move")
            {
                move = context.ReadValue<Vector2>();
            }
        }
    }
}
