using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSense = 100f;
    public Transform playerTransform;

    float xRotation = 0f;

    public float bobSpeed = 14f;
    public float bobAmount = .05f;
    public PlayerController controller;

    float defaultYPos = 0;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        defaultYPos = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSense * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSense * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerTransform.Rotate(Vector3.up * mouseX);


        if (Mathf.Abs(controller.moveDirection.x) > .1f || Mathf.Abs(controller.moveDirection.z) > .1f)
        {
            timer += Time.deltaTime * bobSpeed;
            transform.localPosition = new Vector3(transform.localPosition.x, defaultYPos + Mathf.Sin(timer) * bobAmount, transform.localPosition.z);
        }
        else 
        {
            timer = 0;
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, defaultYPos, Time.deltaTime * bobAmount), transform.localPosition.z);
        }
    }
}
