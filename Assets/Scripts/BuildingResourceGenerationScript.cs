using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    private TextMeshProUGUI countdown;
    private void Start()
    {
        _currencyScript = GameObject.Find("/GameMaster").GetComponent<CurrencyScript>();
        generatedGem = _buildingCard.generatedGem;
        generatedGold = _buildingCard.generatedGold;
        generateTime = _buildingCard.resorceGenerationDuration;
        _time = 0f;
        slider = sliderGO.GetComponent<Slider>();
        slider.maxValue = generateTime;
        countdown = sliderGO.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        countdown.text = generateTime.ToString();
    }
    private void Update()
    {
        _time += Time.deltaTime;
        countdown.text = ((int)(generateTime + 1 - _time)).ToString();
        slider.value = _time;
        if(_time>= generateTime)
        {
            AddCurrency();
            _time = 0;
            countdown.text = generateTime.ToString();

        }
    }

    private void AddCurrency()
    {
        var test = WorldSpaceToCanvas();
        _currencyScript.GoldChange(generatedGold, test,true);
        _currencyScript.GemChange(generatedGem, test,true);
        

    }
    private Vector3 WorldSpaceToCanvas()
    {

        var cam = Camera.main;
        Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
        return screenPos;
    }
}
