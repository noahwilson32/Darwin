using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 12f;
    public Vector3 moveDirection;
    public CharacterController cc;

    public float gravity = -9.81f;

    Vector3 velocity;

    public Transform groundCheck;
    public float groundCheckDistance = .4f;
    public LayerMask groundLayer;
    public float jumpHeight = 3f;

    public bool isgrounded;

    public Transform rightHandSlot;

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
            velocity = Vector3.zero;
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        moveDirection = (transform.right * x) + (transform.forward * z);
        cc.Move(moveDirection * Time.deltaTime * moveSpeed);

        if (Input.GetButtonDown("Jump") && isgrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        if (!isgrounded)
        {

            float launchAngle = Input.GetAxis("Vertical") * 30f;
            velocity.z = Mathf.Sqrt(Mathf.Pow((Time.deltaTime * moveSpeed * transform.forward * Mathf.Cos(launchAngle)).z,2));
            velocity.y -= (Mathf.Sqrt(Mathf.Pow((Time.deltaTime * moveSpeed * transform.up * Mathf.Sin(launchAngle)).y, 2)));
        }

        cc.Move(velocity * Time.deltaTime);

    }

    public void PickUpItem(GameObject _item)
    {
        GameObject rightItem = Instantiate(_item, rightHandSlot.position, Quaternion.identity);
        rightItem.transform.Rotate(0f, 0f, 0f);
        rightItem.transform.parent = this.gameObject.transform;
    }
}
