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
    public bool itemInRange = false;
    public GameObject _item;
    private GameManager gm;
    

    public Transform rightHandSlot;
    public Animator animRight;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        animRight = animRight.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rightHandSlot.localPosition = new Vector3(0.34f, 0.47f, 0.71f);
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

        if (itemInRange == true) 
        {
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                PickUpItem();
            }
        }
        if (rightHandSlot.transform.childCount > 0) 
        {
            if (Input.GetKey(KeyCode.RightShift) && Input.GetKeyDown(KeyCode.E)) 
            {
                DropRightItem();
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            animRight.SetBool("swingRight", true);
        }
        else 
        {
            animRight.SetBool("swingRight",false);
        }

    }
    public void DropRightItem() 
    {
        Rigidbody theRg;
        Transform _item = rightHandSlot.transform.GetChild(0);
        theRg = _item.GetComponent<Rigidbody>();
        theRg.useGravity = true;
        theRg.constraints = RigidbodyConstraints.None;
        rightHandSlot.transform.DetachChildren();
        
    }

    public void PickUpItem()
    {
        Rigidbody theRg;
        /*
        GameObject rightItem = Instantiate(_item, rightHandSlot.position, Quaternion.identity);
        rightItem.transform.Rotate(0f, 0f, 0f);
        rightItem.transform.parent = this.gameObject.transform;
        */
        _item.transform.position = rightHandSlot.position;
        theRg = _item.GetComponent<Rigidbody>();
        theRg.useGravity = false;
        theRg.constraints = RigidbodyConstraints.FreezePosition;
        theRg.freezeRotation = true;
        _item.transform.parent = rightHandSlot.gameObject.transform;
        _item.transform.localPosition = new Vector3(0f,0f,-.3f);
        _item.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        gm.Hide_Item_Pickup_Canvas();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item") 
        {
            itemInRange = true;
            gm.Show_Item_Pickup_Canvas();
            _item = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Item") 
        {
            gm.Hide_Item_Pickup_Canvas();
        }
    }
}
