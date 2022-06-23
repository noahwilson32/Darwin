using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            PlayerController pc = other.GetComponent<PlayerController>();
            if (Input.GetKeyDown(KeyCode.E))
            {
                pc.PickUpItem(this.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}
