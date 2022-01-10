using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingResourceGenerationScript : MonoBehaviour
{
    public BuildingCard _buildingCard;
    private float _time;
    private int generatedGem;
    private int generatedGold;
    private float generateTime;
    private CurrencyScript _currencyScript;

    private void Start()
    {
        _currencyScript = GameObject.Find("/GameMaster").GetComponent<CurrencyScript>();
        generatedGem = _buildingCard.generatedGem;
        generatedGold = _buildingCard.generatedGold;
        generateTime = _buildingCard.resorceGenerationDuration;
        _time = 0f;
    }
    private void Update()
    {
        _time += Time.deltaTime;
        if(_time>= generateTime)
        {
            AddCurrency();
            _time = 0;
        }
    }

    private void AddCurrency()
    {
        _currencyScript.Gem += generatedGem;
        _currencyScript.Gold += generatedGold;

    }
}
