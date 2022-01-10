using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabControl : MonoBehaviour
{
    public bool isAvailable = true;
    private List<SpriteRenderer> spirteList=new List<SpriteRenderer>();
    private void Start()
    {
        var numberOfChild = transform.childCount;
        spirteList.Add(GetComponent<SpriteRenderer>());
        for (int i = 0; i < numberOfChild; i++)
        {
            spirteList.Add(transform.GetChild(i).GetComponent<SpriteRenderer>());
        }
    }
    private void Update()
    {
        if (isAvailable)
        {
            foreach(SpriteRenderer spriteRenderer in spirteList)
            {
                spriteRenderer.color = Color.green;

            }
        }
        else
        {
            foreach (SpriteRenderer spriteRenderer in spirteList)
            {
                spriteRenderer.color = Color.red;

            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Buildings"))
        {
            isAvailable = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Buildings"))
        {
            isAvailable = false;
        }

    }
    public bool PlaceBuilding()
    {
        if (isAvailable)
        {
            Destroy(GetComponent<Rigidbody2D>());
            foreach (SpriteRenderer spriteRenderer in spirteList)
            {
                spriteRenderer.color = Color.white;

            }

            Destroy(GetComponent<PrefabControl>());
            return true;
        }
        else
        {


            return false;
        }
    }
}
