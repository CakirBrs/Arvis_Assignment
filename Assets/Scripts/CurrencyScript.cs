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
    private void Start()
    {
        Gold = 10;
        Gem = 10;
        
    }
    private void Update()
    {
        goldText.text = Gold.ToString();
        GemText.text = Gem.ToString();
    }
    public void GoldChange(int amount,Vector3 position)
    {

        if (amount != 0)
        {
            var sign = "";
            if (amount > 0)
                sign = "+";
            if (amount < 0)
                sign = "-";
            Gold += amount;
            var floatingTextGO = Instantiate(FloatingTextPrefab, position, Quaternion.identity);
            floatingTextGO.transform.parent = GameObject.Find("/Canvas").transform;
            floatingTextGO.GetComponent<TextMeshProUGUI>().text = sign + amount.ToString()+"Gold";
        }
    }
    public void GemChange(int amount, Vector3 position)
    {
        
        if (amount != 0)
        {
            var sign = "";
            if (amount > 0)
                sign = "+";
            if (amount < 0)
                sign = "-";
            Gem += amount;
            var floatingTextGO = Instantiate(FloatingTextPrefab, position, Quaternion.identity);
            floatingTextGO.transform.parent = GameObject.Find("/Canvas").transform;
            floatingTextGO.GetComponent<TextMeshProUGUI>().text = sign + amount.ToString()+"Gem";
        }
    }
}
