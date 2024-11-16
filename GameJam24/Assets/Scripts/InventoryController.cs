using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private int _currentItems = 0;
    private Item[] _items = new Item[16];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void AddItem(Item item)
    {
        _items[_currentItems] = item;
        _currentItems += 1 << item.id;
        Debug.Log("Added item " + item.name + " to inventory.");
    }
}
