using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragNDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler,IEndDragHandler,IDragHandler
{
    private RectTransform rectTransform;
    [SerializeField]
    private GameObject go;
    [SerializeField]
    private GameObject Prefab1x2x;
    [SerializeField]
    private GameObject Prefab2x;
    [SerializeField]
    private GameObject Prefab2x2x;
    [SerializeField]
    private GameObject Prefab3x2x;

    private void Awake()
    {
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject obj;
        Debug.Log("dragStart");
        go.SetActive(true);
        var BCtC = GetComponent<BuildingCardToCanvas>();
        var bc = BCtC.bc;
        go.GetComponent<RawImage>().texture = bc.image.texture;
        if(bc.shapeOfBuilding== BuildingCard.shapeOfBuildingType.Shape1)
        {
            obj = Prefab1x2x;
        }
        else if (bc.shapeOfBuilding == BuildingCard.shapeOfBuildingType.Shape2)
        {
            obj = Prefab2x;

        }
        else if (bc.shapeOfBuilding == BuildingCard.shapeOfBuildingType.Shape3)
        {
            obj = Prefab2x2x;

        }
        else if (bc.shapeOfBuilding == BuildingCard.shapeOfBuildingType.Shape4)
        {
            obj = Prefab3x2x;

        }
        else
        {
            obj = null;

        }
        Instantiate(obj, new Vector3(0, 0), Quaternion.identity);
        rectTransform = go.GetComponent<RectTransform>();
        go.transform.position = Input.mousePosition;
        


    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("drag");
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        go.SetActive(false);

        Debug.Log("dragStop");

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Týk");
    }
}
