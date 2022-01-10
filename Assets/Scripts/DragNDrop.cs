using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragNDrop : MonoBehaviour, IBeginDragHandler,IEndDragHandler,IDragHandler
{
    private RectTransform rectTransform;
    [SerializeField]
    private GameObject draggedIcon;
    [SerializeField]
    private GameObject Prefab1x2x;
    [SerializeField]
    private GameObject Prefab2x;
    [SerializeField]
    private GameObject Prefab2x2x;
    [SerializeField]
    private GameObject Prefab3x2x;

    public Vector3 worldPosition;
    GameObject createdGO;

    BuildingCard _buildingCard;

    private void Awake()
    {
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject _prefab;
        draggedIcon.SetActive(true);
        var BCTCscirpt = GetComponent<BuildingCardToCanvas>();
        _buildingCard = BCTCscirpt._buildingCard;
        draggedIcon.GetComponent<RawImage>().texture = _buildingCard.image.texture;
        if(_buildingCard.shapeOfBuilding== BuildingCard.shapeOfBuildingType.Shape1)
        {
            _prefab = Prefab1x2x;
        }
        else if (_buildingCard.shapeOfBuilding == BuildingCard.shapeOfBuildingType.Shape2)
        {
            _prefab = Prefab2x;

        }
        else if (_buildingCard.shapeOfBuilding == BuildingCard.shapeOfBuildingType.Shape3)
        {
            _prefab = Prefab2x2x;

        }
        else if (_buildingCard.shapeOfBuilding == BuildingCard.shapeOfBuildingType.Shape4)
        {
            _prefab = Prefab3x2x;

        }
        else
        {
            _prefab = null;

        }
        createdGO= Instantiate(_prefab, new Vector3(0, 0), Quaternion.identity);
        rectTransform = draggedIcon.GetComponent<RectTransform>();
        draggedIcon.transform.position = Input.mousePosition;
        


    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
        worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        createdGO.transform.position = new Vector3(((int)worldPosition.x), ((int)worldPosition.y), 0f);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        draggedIcon.SetActive(false);
        var _prefabControl = createdGO.GetComponent<PrefabControl>();
        _prefabControl._buildingCard = _buildingCard;
        var _bool = _prefabControl.PlaceBuilding();
        if (!_bool)
        {
            Destroy(createdGO);
        }
    }

    
}
