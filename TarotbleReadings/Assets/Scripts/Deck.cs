using UnityEngine;
using System.Collections.Generic;



[CreateAssetMenu(fileName = "Deck", menuName = "Scriptable Objects/Deck")]
public class Deck : ScriptableObject
{
    [SerializeField] private List<Card> cards;
    private List<Card> _pulledCards;

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
        return cards[randomIndex];
    }

    private Rarity GenerateRarity(){
        //probability stuff
        return Rarity.silver;
    }

    private bool GenerateReverse(){
        int coinFlip = Random.Range(0, 2);
        return coinFlip == 1;
    }

    private bool checkCopies(Card card){
        return _pulledCards.Contains(card);
    }

}
