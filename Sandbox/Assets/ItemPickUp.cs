using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
   // public Canvas itemPickupCanvas;
    //private GameManager gm;
    private void Start()
    {
        //gm = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //gm.Show_Item_Pickup_Canvas();
            other.gameObject.GetComponentInParent<PlayerController>().PickUpItem(gameObject);
        }   
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //gm.Hide_Item_Pickup_Canvas();
        }
    }
}
