using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyScript : MonoBehaviour
{
    public int Gold;
    public Transform GoldPos;
    public int Gem;
    public Transform GemPos;
    [SerializeField]
    private TextMeshProUGUI goldText;
    [SerializeField]
    private TextMeshProUGUI GemText;
    [SerializeField]
    private GameObject FloatingTextPrefab;
    private SaveScript _saveScript;
    private void Start()
    {
        
        _saveScript= GameObject.Find("/GameMaster").GetComponent<SaveScript>();


    }
    private void Update()
    {
        goldText.text = Gold.ToString();
        GemText.text = Gem.ToString();
    }
    public void GoldChange(int amount,Vector3 position,bool isPositive)
    {

        if (amount != 0)
        {
            _saveScript.Save();

            var sign = "";
            if (isPositive)
            {
                sign = "+";
                Gold += amount;

            }
            if (!isPositive)
            {
                sign = "-";
                Gold -= amount;

            }
            var floatingTextGO = Instantiate(FloatingTextPrefab, position, Quaternion.identity);
            floatingTextGO.transform.parent = GameObject.Find("/Canvas").transform;
            floatingTextGO.GetComponent<TextMeshProUGUI>().text = sign + amount.ToString()+"Gold";
        }
    }
    public void GemChange(int amount, Vector3 position, bool isPositive)
    {
        
        if (amount != 0)
        {
            _saveScript.Save();
            var sign = "";
            if (isPositive)
            {
                sign = "+";
                Gem += amount;
            }
            if (!isPositive)
            {
                sign = "-";
                Gem -= amount;
            }
            var floatingTextGO = Instantiate(FloatingTextPrefab, position, Quaternion.identity);
            floatingTextGO.transform.parent = GameObject.Find("/Canvas").transform;
            floatingTextGO.GetComponent<TextMeshProUGUI>().text = sign + amount.ToString()+"Gem";
        }
    }
}
