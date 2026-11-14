using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Scriptable Objects/Card")]
public class Card : ScriptableObject
{
    [SerializeField] private string cardName;
    [SerializeField] private Sprite image;
    [SerializeField] private Sprite border;
    [SerializeField] private Rarity rarity;

}
