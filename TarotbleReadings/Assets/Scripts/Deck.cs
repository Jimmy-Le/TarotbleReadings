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
    
}
