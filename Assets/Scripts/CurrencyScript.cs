using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyScript : MonoBehaviour
{
    public int Gold;
    public int Gem;
    [SerializeField]
    private TextMeshProUGUI goldText;
    [SerializeField]
    private TextMeshProUGUI GemText;
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

}
