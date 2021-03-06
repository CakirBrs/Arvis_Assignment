using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DragNDrop : MonoBehaviour, IBeginDragHandler,IEndDragHandler,IDragHandler, IPointerEnterHandler, IPointerExitHandler
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
    private bool isCardActive;
    private GameObject InfoWindow;
    [SerializeField]
    private List<GameObject> miniBuildingIco = new List<GameObject>();
    private SaveScript _saveScript;

    private void Start()
    {
        draggedIcon = GameObject.Find("draggedObj");
        InfoWindow = GameObject.Find("InfoWindow");
        _saveScript = GameObject.Find("/GameMaster").GetComponent<SaveScript>();

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        isCardActive = GetComponent<BuildingCardToCanvas>().isActive;

        if (isCardActive)
        {
            GameObject _prefab;
            draggedIcon.GetComponent<RawImage>().enabled = true;
            draggedIcon.GetComponent<RawImage>().texture = _buildingCard.image.texture;
            if (_buildingCard.shapeOfBuilding == BuildingCard.shapeOfBuildingType.Shape2)
            {
                _prefab = Prefab1x2x;
            }
            else if (_buildingCard.shapeOfBuilding == BuildingCard.shapeOfBuildingType.Shape1)
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
            createdGO = Instantiate(_prefab, new Vector3(0, 0), Quaternion.identity);
            rectTransform = draggedIcon.GetComponent<RectTransform>();
            draggedIcon.transform.position = Input.mousePosition;

        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isCardActive)
        {
            rectTransform.anchoredPosition += eventData.delta;
            worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            createdGO.transform.position = new Vector3(Mathf.RoundToInt(worldPosition.x), Mathf.RoundToInt(worldPosition.y), 0f);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isCardActive)
        {
            draggedIcon.GetComponent<RawImage>().enabled = false;
            var _prefabControl = createdGO.GetComponent<PrefabControl>();
            _prefabControl._buildingCard = _buildingCard;
            var _bool = _prefabControl.PlaceBuilding();
            if (!_bool)
            {
                
                Destroy(createdGO);
            }
            else
            {
                var _cardManager = GameObject.Find("/GameMaster").GetComponent<CardManager>();
                _saveScript.AddBuilding(_prefabControl.gameObject.transform.position, _buildingCard);
                _cardManager.DeleteCard(transform.gameObject);

            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var tasdasdaa = InfoWindow.transform.GetChild(0);
        
        tasdasdaa.gameObject.SetActive(true);

        var buildingType = tasdasdaa.gameObject.transform.Find("buildingType");
        var numberOfChild = buildingType.gameObject.transform.childCount;
        miniBuildingIco.Clear();
        for (int i = 0; i < numberOfChild; i++)
        {
            var go = buildingType.GetChild(i);
            miniBuildingIco.Add(go.gameObject);
        }
        var BCTCscirpt = GetComponent<BuildingCardToCanvas>();

        _buildingCard = BCTCscirpt._buildingCard;

        var tmpro = tasdasdaa.GetComponentInChildren<TextMeshProUGUI>();
        tmpro.text = _buildingCard.resorceGenerationDuration.ToString() + "   Second\n" + _buildingCard.generatedGold.ToString() + "\n" + _buildingCard.generatedGem.ToString();
        if (_buildingCard.shapeOfBuilding == BuildingCard.shapeOfBuildingType.Shape1)
        {
            for(int i = 0; i < miniBuildingIco.Count; i++)
            {
                if (i == 2)
                {
                    miniBuildingIco[i].SetActive(true);

                }
                else
                {
                    miniBuildingIco[i].SetActive(false);

                }
            }
        }
        else if (_buildingCard.shapeOfBuilding == BuildingCard.shapeOfBuildingType.Shape2)
        {
            for (int i = 0; i < miniBuildingIco.Count; i++)
            {
                if (i == 0 || i == 1) 
                {
                    miniBuildingIco[i].SetActive(true);

                }
                else
                {
                    miniBuildingIco[i].SetActive(false);

                }
            }

        }
        else if (_buildingCard.shapeOfBuilding == BuildingCard.shapeOfBuildingType.Shape3)
        {
            for (int i = 0; i < miniBuildingIco.Count; i++)
            {
                if (i == 0 || i == 1 || i == 2) 
                {
                    miniBuildingIco[i].SetActive(true);

                }
                else
                {
                    miniBuildingIco[i].SetActive(false);

                }
            }

        }
        else if (_buildingCard.shapeOfBuilding == BuildingCard.shapeOfBuildingType.Shape4)
        {
            for (int i = 0; i < miniBuildingIco.Count; i++)
            {
                if (i == 0 || i == 1 || i == 2 || i == 3) 
                {
                    miniBuildingIco[i].SetActive(true);

                }
                else
                {
                    miniBuildingIco[i].SetActive(false);

                }
            }

        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        var tasdasdaa = InfoWindow.transform.GetChild(0);
        tasdasdaa.gameObject.SetActive(false);

    }
}
