using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class PlayerController : BaseGameObject
    {
        public static PlayerController instance;

        public float rotationSpeed = 5f;

        private Rigidbody rb;

        private Vector3 lastPosition, startPosition;
        private Vector3 moveDirection;
        private float currentAngle = 0f;
        private bool isMoving = false;
        private bool isGrounded = false;

        private PlatformController currentPlatform;
        private TouchController touch;

        public void Move(Vector3 direction)
        {
            if (isMoving || !isGrounded)
                return;

            moveDirection = direction;
            lastPosition = transform.position;
            isMoving = true;
        }

        void Rotate()
        {
            currentAngle += rotationSpeed * Time.deltaTime;

            if (moveDirection == Vector3.right || moveDirection == Vector3.left)
            {
                float dir = moveDirection.x;
                transform.RotateAround(lastPosition + new Vector3(0.5f * dir, -0.5f, 0), Vector3.forward, -rotationSpeed * (dir) * Time.deltaTime);
            }
            else
            {
                float dir = moveDirection.z;
                transform.RotateAround(lastPosition + new Vector3(0.5f, -0.5f, 0.5f * dir), Vector3.right, rotationSpeed * (dir) * Time.deltaTime);
            }

            if (currentAngle > 90.0f)
            {
                float fix = currentAngle - 90.0f;
                if (moveDirection == Vector3.right || moveDirection == Vector3.left)
                {
                    float dir = moveDirection.x;
                    transform.RotateAround(lastPosition + new Vector3(0.5f * dir, -0.5f, -0.5f), Vector3.forward, fix * (dir));
                }
                else
                {
                    float dir = moveDirection.z;
                    transform.RotateAround(lastPosition + new Vector3(0.5f, -0.5f, 0.5f * dir), Vector3.right, -fix * (dir));
                }

                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                lastPosition = transform.position;
                currentAngle = 0;
                isMoving = false;
                if (currentPlatform != null)
                {
                    currentPlatform.Kill();
                }
                CheckGround();
            }
        }

        public void CheckGround()
        {
            RaycastHit hit;
            if (!Physics.Raycast(transform.position, Vector3.down, out hit, 2f, LayerMask.GetMask("Platform")))
            {
                rb.isKinematic = false;
                isGrounded = false;
                return;
            }

            isGrounded = true;
            currentPlatform = hit.collider.GetComponent<PlatformController>();
        }

        private void Awake()
        {
            touch = GetComponent<TouchController>();
        }

        public void Respawn()
        {
            currentPlatform = null;
            transform.position = lastPosition = startPosition;
            transform.rotation = Quaternion.identity;
            rb.MovePosition(transform.position);
            rb.isKinematic = true;
            gameObject.SetActive(true);
            isMoving = false;
            moveDirection = Vector3.zero;

            if (touch != null)
                touch.ResetController();

            CheckGround();
        }

        void Start()
        {
            instance = this;
            gameObject.SetActive(false);
            lastPosition = startPosition = transform.position;
            rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            if (transform.position.y < -20)
            {
                GameManager.instance.GameOver();
                gameObject.SetActive(false);
            }

            if (isMoving)
                Rotate();

            if (Input.GetKey(KeyCode.D))
                Move(Vector3.right);

            if (Input.GetKey(KeyCode.A))
                Move(Vector3.left);

            if (Input.GetKey(KeyCode.W))
                Move(Vector3.forward);

            if (Input.GetKey(KeyCode.S))
                Move(Vector3.back);
        }
    }
}