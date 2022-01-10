using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefabControl : MonoBehaviour
{
    public bool isAvailable = true;
    public BuildingCard _buildingCard;
    private List<SpriteRenderer> spirteList=new List<SpriteRenderer>();
    [SerializeField]
    private GameObject slider;
    private CurrencyScript _currencyScript;
    private void Start()
    {
        var numberOfChild = transform.childCount;
        spirteList.Add(GetComponent<SpriteRenderer>());
        _currencyScript= GameObject.Find("/GameMaster").GetComponent<CurrencyScript>();
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
            var _buildingResourceGenerationScript = GetComponent<BuildingResourceGenerationScript>();
            _buildingResourceGenerationScript._buildingCard = _buildingCard;
            _buildingResourceGenerationScript.enabled = true;

            var _canvasPosition = WorldSpaceToCanvas();

            var SliderGameObject  = Instantiate(slider, new Vector3(_canvasPosition.x, _canvasPosition.y - 40, _canvasPosition.z), Quaternion.identity);
            SliderGameObject.transform.parent = GameObject.Find("/Canvas").transform;
            _buildingResourceGenerationScript.sliderGO = SliderGameObject;

            GameObject image = new GameObject();
            var imageGO = Instantiate(image, _canvasPosition, Quaternion.identity);
            imageGO.transform.parent = GameObject.Find("/Canvas").transform;
            var imageRawimage= imageGO.AddComponent<RawImage>();
            imageRawimage.texture = _buildingCard.image.texture;
            imageRawimage.color = Color.black;
            Destroy(GetComponent<PrefabControl>());
            _currencyScript.Gem -= _buildingCard.costGem;
            _currencyScript.Gold -= _buildingCard.costGold;

            

            return true;
        }
        else
        {


            return false;
        }
    }

    private Vector3 WorldSpaceToCanvas()
    {
         
        var cam = Camera.main;
        Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
        return screenPos;
    }
}
