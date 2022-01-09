using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BuildingCardToCanvas : MonoBehaviour
{
    public BuildingCard bc;
    public int goldC;
    public int gemC;
    public Image asd;

    void Start()
    {
        asd = GetComponent<Image>();
        Debug.Log(bc.name);
        transform.GetChild(0).gameObject.GetComponent<RawImage>().texture=bc.image.texture;
        var goldGO = transform.GetChild(1).GetChild(0).gameObject;
        var gemGO = transform.GetChild(1).GetChild(1).gameObject;
        if (bc.costGem > 0)
        {
            gemGO.SetActive(true);
            gemGO.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = bc.costGem.ToString();
        }
        else
        {
            gemGO.SetActive(false);
        }

        if (bc.costGold > 0)
        {
            goldGO.SetActive(true);
            goldGO.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = bc.costGold.ToString();

        }
        else
        {
            goldGO.SetActive(false);
        }


    }

    void Update()
    {
        if (goldC >= bc.costGold)
        {

            asd.color = new Color(0.6886792f, 0.6886792f, 0.6886792f, 1f);

        }
        else
        {
            asd.color = new Color(0.286792f, 0.286792f, 0.286792f, 1f);

        }
    }
}
