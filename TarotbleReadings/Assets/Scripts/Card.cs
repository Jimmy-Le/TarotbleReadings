using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Scriptable Objects/Card")]
public class Card : ScriptableObject
{
    [SerializeField] private string cardName;
    [SerializeField] private Material image;
    [SerializeField] private Material border;
    [SerializeField] private Rarity rarity;
    [SerializeField] private Transform cardTransform;
    //true means reversed
    private bool _reverse;


    public void SetRarity(Rarity rarity){
        this.rarity = rarity;
    }

    public void SetBorder(Material border){
        this.border = border;
    }

    public void SetReverse(bool reverse){
        _reverse = reverse;
    }

    //getters
    public Material GetImage(){
        return image;
    }

    public Material GetBorder(){
        return border;
    }

    public string GetCardName(){
        return cardName;
    }

    public bool GetReverse(){
        return _reverse;
    }

    public Transform GetTransform(){
        return cardTransform;
    }
}
