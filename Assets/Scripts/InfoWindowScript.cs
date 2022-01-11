using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoWindowScript : MonoBehaviour
{
    [SerializeField]
    private GameObject infoWindow;

    private void Update()
    {

        infoWindow.transform.position = Input.mousePosition;
    }
}
