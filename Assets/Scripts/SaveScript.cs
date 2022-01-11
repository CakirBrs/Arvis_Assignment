using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SaveScript : MonoBehaviour
{
    [SerializeField]
    private List<Vector3> buildingsPositions = new List<Vector3>();
    [SerializeField]
    private List<BuildingCard> buildingsTypes = new List<BuildingCard>();
    private CurrencyScript _currencyScript;
    private CardManager _cardManager;
    private List<int> IndexOfbuildingCards = new List<int>();

    public GameObject Prefab1x2x;
    public GameObject Prefab2x;
    public GameObject Prefab2x2x;
    public GameObject Prefab3x2x;

    [SerializeField]
    private GameObject slider;

    public void Begin()
    {
        _currencyScript = GameObject.Find("/GameMaster").GetComponent<CurrencyScript>();
        _cardManager = GameObject.Find("/GameMaster").GetComponent<CardManager>();
        Load();
    }
    private void Update()
    {
        //for debugging
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }
    public void Save()
    {
        PlayerPrefs.SetString("NumberOfBuildings", buildingsPositions.Count.ToString());//bunu int yap
        for (int i = 0; i < buildingsPositions.Count; i++)
        {
            PlayerPrefs.SetFloat(("buildingsPositions" + i.ToString() + "x"), buildingsPositions[i].x);
            PlayerPrefs.SetFloat(("buildingsPositions" + i.ToString() + "y"), buildingsPositions[i].y);
            PlayerPrefs.SetFloat(("buildingsPositions" + i.ToString() + "z"), buildingsPositions[i].z);
            PlayerPrefs.SetString(("buildingsTypes" + i.ToString()), buildingsTypes[i].name);

        }

        var BuildingCardList = _cardManager.indexOfBuildingCardList;
        PlayerPrefs.SetInt("NumberOfCards", BuildingCardList.Count);
        for (int index = 0; index < BuildingCardList.Count; index++)
        {
            PlayerPrefs.SetInt("IndexOfbuildingCards"+index.ToString(), BuildingCardList[index]);
        }

        PlayerPrefs.SetInt("gold", _currencyScript.Gold);
        PlayerPrefs.SetInt("Gem", _currencyScript.Gem);
    }
    public void Load()
    {
        buildingsPositions.Clear();
        buildingsTypes.Clear();

        var gold = PlayerPrefs.GetInt("gold", _currencyScript.Gold);
        var gem = PlayerPrefs.GetInt("Gem", _currencyScript.Gem);


        var NumberOfBuildings = int.Parse(PlayerPrefs.GetString("NumberOfBuildings", buildingsPositions.Count.ToString()));
        for (int i = 0; i < NumberOfBuildings; i++)
        {
            var x = PlayerPrefs.GetFloat(("buildingsPositions" + i.ToString() + "x"), 0);
            var y = PlayerPrefs.GetFloat(("buildingsPositions" + i.ToString() + "y"), 0);
            var z = PlayerPrefs.GetFloat(("buildingsPositions" + i.ToString() + "z"), 0);
            buildingsPositions.Add(new Vector3(x, y, z));

            var _buildingName = PlayerPrefs.GetString(("buildingsTypes" + i.ToString()), "none");
            var buildingScriptableObject = FindScriptableObject(_buildingName);
            buildingsTypes.Add(buildingScriptableObject);
        }


        var NumberOfCards = PlayerPrefs.GetInt("NumberOfCards", 0);
        for (int index = 0; index < NumberOfCards; index++)
        {
            IndexOfbuildingCards.Add(PlayerPrefs.GetInt("IndexOfbuildingCards" + index.ToString(), 0));
        }




        LoadWorld(gold,gem, NumberOfBuildings);
        LoadCardMenu();
    }
    private void LoadCardMenu()
    {
        if (IndexOfbuildingCards.Count > 0)
        {
            foreach (int i in IndexOfbuildingCards)
            {
                _cardManager.AddCard(false, i);
            }
        }
        else
        {
            _currencyScript.Gold = 10;
            _currencyScript.Gem = 10;
            for (int i = 1; i < _cardManager.NumberOfCards+1; i++)
            {
                if (i == 1)
                {
                    _cardManager.AddCard(false, 1);

                }
                else
                {
                    _cardManager.AddCard(true, 0);

                }
            }
        }
    }

    private BuildingCard FindScriptableObject(string name)
    {

        foreach (BuildingCard _bcard in _cardManager.buildingCards)
        {
            if (name == _bcard.name)
            {
                return _bcard;
            }
        }
        return null;
        
    }

    private void LoadWorld(int gold,int gem,int NumberOfBuildings)
    {
        _currencyScript.Gold = gold;
        _currencyScript.Gem = gem;
        for(int i = 0; i < NumberOfBuildings; i++)
        {
            var buildingType=DefineBuildingType(buildingsTypes[i]);
            var createdGO = Instantiate(buildingType, buildingsPositions[i], Quaternion.identity);

            Destroy(createdGO.GetComponent<Rigidbody2D>());
            var _buildingResourceGenerationScript = createdGO.GetComponent<BuildingResourceGenerationScript>();
            _buildingResourceGenerationScript._buildingCard = buildingsTypes[i];
            _buildingResourceGenerationScript.enabled = true;

            var _canvasPosition = WorldSpaceToCanvas(buildingsPositions[i]);

            var SliderGameObject = Instantiate(slider, new Vector3(_canvasPosition.x, _canvasPosition.y - 40, _canvasPosition.z), Quaternion.identity);
            SliderGameObject.transform.parent = GameObject.Find("/Canvas").transform;
            _buildingResourceGenerationScript.sliderGO = SliderGameObject;

            GameObject image = new GameObject();
            var imageGO = Instantiate(image, _canvasPosition, Quaternion.identity);
            imageGO.transform.parent = GameObject.Find("/Canvas").transform;
            var imageRawimage = imageGO.AddComponent<RawImage>();
            imageRawimage.texture = buildingsTypes[i].image.texture;
            imageRawimage.color = Color.black;
            Destroy(createdGO.GetComponent<PrefabControl>());


        }
    }

    public void AddBuilding(Vector3 position, BuildingCard _buildingCard)
    {
        buildingsPositions.Add(position);
        buildingsTypes.Add(_buildingCard);
        Save();
    }


    private GameObject DefineBuildingType(BuildingCard _buildingCard)
    {
        GameObject _prefab;
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
        return _prefab;
    }

    private Vector3 WorldSpaceToCanvas(Vector3 position)
    {

        var cam = Camera.main;
        Vector3 screenPos = cam.WorldToScreenPoint(position);
        return screenPos;
    }
}
