using System;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public Item item;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<InventoryController>().AddItem(item);
            Destroy(gameObject);
        }
    }
}
