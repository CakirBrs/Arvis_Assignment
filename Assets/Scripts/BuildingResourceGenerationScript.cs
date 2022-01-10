using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingResourceGenerationScript : MonoBehaviour
{
    public BuildingCard _buildingCard;
    private float _time;
    private int generatedGem;
    private int generatedGold;
    private float generateTime;
    private CurrencyScript _currencyScript;
    public GameObject sliderGO;
    private Slider slider;

    private void Start()
    {
        _currencyScript = GameObject.Find("/GameMaster").GetComponent<CurrencyScript>();
        generatedGem = _buildingCard.generatedGem;
        generatedGold = _buildingCard.generatedGold;
        generateTime = _buildingCard.resorceGenerationDuration;
        _time = 0f;
        slider = sliderGO.GetComponent<Slider>();
        slider.maxValue = generateTime;
    }
    private void Update()
    {
        _time += Time.deltaTime;
        slider.value = _time;
        if(_time>= generateTime)
        {
            AddCurrency();
            _time = 0;
        }
    }

    private void AddCurrency()
    {
        var test = WorldSpaceToCanvas();
        _currencyScript.GoldChange(generatedGold, test);
        _currencyScript.GemChange(generatedGem, test);
        

    }
    private Vector3 WorldSpaceToCanvas()
    {

        var cam = Camera.main;
        Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
        return screenPos;
    }
}
