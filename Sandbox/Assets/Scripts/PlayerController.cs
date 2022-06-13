using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 12f;
    public CharacterController cc;

    public float gravity = -9.81f;

    Vector3 velocity;

    public Transform groundCheck;
    public float groundCheckDistance = .4f;
    public LayerMask groundLayer;
    public float jumpHeight = 3f;

    bool isgrounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isgrounded = Physics.CheckSphere(groundCheck.position,groundCheckDistance,groundLayer);
        if (isgrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * x) + (transform.forward * z);
        cc.Move(move * Time.deltaTime * moveSpeed);

        if (Input.GetButtonDown("Jump") && isgrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);

    }
}
