using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Object.Destroy(gameObject, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime*100, transform.position.z);
    }
}
