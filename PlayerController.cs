using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] protected float speed = 5.0f;

    [Header("Look")]
    [SerializeField] private Transform cameraObject;
    [SerializeField] private float sensitivity = 2;
    [SerializeField] private float smoothing = 1.5f;

    Vector2 velocity;
    Vector2 frameVelocity;

    Rigidbody characterRB;

    void Awake()
    {
        characterRB = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        Move();
        RotateCamera();
    }

    void Update()
    {
        
    }

    void Move()
    {
        Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);
        characterRB.velocity = transform.rotation * new Vector3(targetVelocity.x, characterRB.velocity.y, targetVelocity.y);
    }

    void RotateCamera()
    {
        Vector2 mouseData = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Vector2 newFrameVelocity = Vector2.Scale(mouseData, Vector2.one * sensitivity);
        frameVelocity = Vector2.Lerp(frameVelocity, newFrameVelocity, 1 / smoothing);

        velocity += frameVelocity;
        velocity.y = Mathf.Clamp(velocity.y, -90, 90);

        cameraObject.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
        transform.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
    }
}
