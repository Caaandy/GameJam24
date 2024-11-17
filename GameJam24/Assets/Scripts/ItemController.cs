using UnityEngine;

public class ItemController : MonoBehaviour
{
    public Item item;
    public float bobbingIntensity = 0.2f;
    public float bobbingSpeed = 1.0f;
    private Vector3 fixedPosition;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fixedPosition = transform.position;
        GetComponent<SpriteRenderer>().sprite = item.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = fixedPosition + Mathf.Cos(Time.time * bobbingSpeed) * bobbingIntensity * Vector3.up;
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
