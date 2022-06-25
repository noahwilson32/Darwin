using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Canvas item_pickup_canvas;
    

    void Awake()
    {
        item_pickup_canvas.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Show_Item_Pickup_Canvas()
    {
        item_pickup_canvas.gameObject.SetActive(true);
    }
    public void Hide_Item_Pickup_Canvas()
    {
        item_pickup_canvas.gameObject.SetActive(false);
    }
}
