using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BuildingCardToCanvas : MonoBehaviour
{
    public BuildingCard _buildingCard;
    private int goldC;
    private int gemC;
    private Image buildingCardBorder;
    private CurrencyScript _currencyScript;
    public bool isActive;
    [SerializeField]
    private TextMeshProUGUI nameText;
    void Start()
    {
        _currencyScript= GameObject.Find("/GameMaster").GetComponent<CurrencyScript>();
        buildingCardBorder = GetComponent<Image>();

        
        nameText.text = "Building "+_buildingCard.name;

        transform.GetChild(0).gameObject.GetComponent<RawImage>().texture=_buildingCard.image.texture;
        var goldGO = transform.GetChild(1).GetChild(0).gameObject;
        var gemGO = transform.GetChild(1).GetChild(1).gameObject;
        if (_buildingCard.costGem > 0)
        {
            gemGO.SetActive(true);
            gemGO.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = _buildingCard.costGem.ToString();
        }
        else
        {
            gemGO.SetActive(false);
        }

        if (_buildingCard.costGold > 0)
        {
            goldGO.SetActive(true);
            goldGO.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = _buildingCard.costGold.ToString();

        }
        else
        {
            goldGO.SetActive(false);
        }


    }

    void Update()
    {
        goldC = _currencyScript.Gold;
        gemC = _currencyScript.Gem;
        if (goldC >= _buildingCard.costGold && gemC >= _buildingCard.costGem)
        {
            isActive = true;
            buildingCardBorder.color = new Color(0.6886792f, 0.6886792f, 0.6886792f, 1f);

        }
        else
        {
            isActive = false;
            buildingCardBorder.color = new Color(0.286792f, 0.286792f, 0.286792f, 1f);

        }
    }
}
