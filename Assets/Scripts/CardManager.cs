using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [Header("Settings:")]
    public bool isRandomizedAndReplaceEachUsedCardWithNewCard;//If this flag is activated, cards will be randomly generated and used cards will be deleted.
    public List<BuildingCard> buildingCardsWantedToBeUsed = new List<BuildingCard>(); //If the random flag is deactivated, this list should be filled with the cards to be used.

    [Header("Do not touch:")]

    public List<BuildingCard> buildingCards = new List<BuildingCard>();
    public List<int> indexOfBuildingCardList = new List<int>();
    [SerializeField]
    private GameObject cardPrefab;
    [SerializeField]
    private List<GameObject> buildingCardsGO = new List<GameObject>();
    public int NumberOfCards;

   

    public void DeleteCard(GameObject gameObject)
    {
        if (isRandomizedAndReplaceEachUsedCardWithNewCard)
        {
            var index = buildingCardsGO.IndexOf(gameObject);
            indexOfBuildingCardList.RemoveAt(index);
            buildingCardsGO.Remove(gameObject);
            Destroy(gameObject);
            AddCard(true, 0);
        }
        
    }

    public void AddCard(bool isRandom,int indexOfBuildingCards)
    {
        int indexInt;
        if (isRandom)
        {
            indexInt = Random.Range(0, buildingCards.Count);
            

        }
        else
        {
            indexInt = indexOfBuildingCards;
        }

        var card = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        card.transform.parent = GameObject.Find("BuildingCardsHolder").transform;
        buildingCardsGO.Add(card);
        indexOfBuildingCardList.Add(indexInt);
        card.name = "BuildingCard (" + card.GetInstanceID().ToString() + ")";
        card.GetComponent<BuildingCardToCanvas>()._buildingCard = buildingCards[indexInt];

    }
}
