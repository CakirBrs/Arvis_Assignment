using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField]
    private List<BuildingCard> buildingCards = new List<BuildingCard>();
    [SerializeField]
    private GameObject cardPrefab;
    private List<GameObject> buildingCardsGO = new List<GameObject>();
    [SerializeField]
    private int NumberOfCards;

    private void Start()
    {
        for (int i = 1; i < NumberOfCards; i++)
        {
            
            AddCard();
        }

    }

    public void DeleteCard(GameObject gameObject)
    {
        Destroy(gameObject);
        AddCard();
    }

    public void AddCard()
    {
        var randomInt = Random.Range(0, buildingCards.Count);
        var card = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        card.transform.parent = GameObject.Find("BuildingCardsHolder").transform;
        buildingCardsGO.Add(card);

        card.name = "BuildingCard (" + card.GetInstanceID().ToString()+")";
        card.GetComponent<BuildingCardToCanvas>()._buildingCard = buildingCards[randomInt];

    }
}
