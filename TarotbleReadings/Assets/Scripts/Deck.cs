using UnityEngine;
using System.Collections.Generic;



[CreateAssetMenu(fileName = "Deck", menuName = "Scriptable Objects/Deck")]
public class Deck : ScriptableObject
{
    [SerializeField] private List<Card> cards;
    private List<Card> _pulledCards;

    void Awake(){
        _pulledCards = new List<Card>();
    }

    public void AddToPulledCards(Card card){
        _pulledCards.Add(card);
    }

    public void ClearPulledCards(){
        _pulledCards.Clear();
    }
    
    public Card pickCard(){
        int randomIndex;
        do{
        //generate a random index 
        randomIndex = Random.Range(0, cards.Count);

        //prevent duplicate cards being pulled
        }while(checkCopies(cards[randomIndex]));

        //rarity?
        cards[randomIndex].SetRarity(GenerateRarity());
        //reversed?
        cards[randomIndex].SetReverse(GenerateReverse());

        //put it in the pulled cards list
        _pulledCards.Add(cards[randomIndex]);

        return cards[randomIndex];
    }

    private Rarity GenerateRarity(){
        //probability stuff

        float silverProb = 70f;                                 // Get a silver rarity under 70%
        float goldProb = 90f;                                   // Get a gold rarity from 70-90% (20% chance)
        float prismaticProb = 100f;                             // Get a prismatic rarity from 90-100% (10% chance)
      
        float randomChance = Random.Range(0f,prismaticProb);

        if (randomChance <= silverProb)
        {
            return Rarity.silver;
        }
        else if (randomChance > silverProb && randomChance <= goldProb)
        {
            return Rarity.gold;
        }
        else
        {
            return Rarity.prismatic;
        }

    }

    private bool GenerateReverse(){
        int coinFlip = Random.Range(0, 2);
        return coinFlip == 1;
        // return true;
    }

    private bool checkCopies(Card card){
        return _pulledCards.Contains(card);
    }

}
