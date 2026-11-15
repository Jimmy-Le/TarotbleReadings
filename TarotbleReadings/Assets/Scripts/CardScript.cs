using UnityEngine;
using TMPro;

public class CardScript : MonoBehaviour
{
    [SerializeField] private Card cardData;
    [SerializeField] private MeshRenderer meshRendererImage;
    [SerializeField] private MeshRenderer meshRendererBorder;
    [SerializeField] private TextMeshPro textMeshProName;
    [SerializeField] private Transform cardTransform;
    private Spot spot;
   
    void Awake()
    {
        if(cardData != null){
            UpdateCard();
        }
    }

    public Transform GetTransform(){
        return cardTransform;
    }

    public Spot GetSpot(){
        return spot;
    }

    public void SetSpot(Spot newSpot){
        spot = newSpot;
    }

    public Card GetCardData(){
        return cardData;
    }

    public void SetCardData(Card newCardData){
        cardData = newCardData;
        UpdateCard();
    }

    public void UpdateCard(){
         //check if flipped
        if(cardData.GetReverse()){
            this.GetComponent<Transform>().localRotation *= Quaternion.Euler(0,0,180);    // Originally Z: 180
        }
        //setting the images
        meshRendererImage.material = cardData.GetImage();
        //border
        meshRendererBorder.material = cardData.GetBorder();
        //name
        textMeshProName.SetText(cardData.GetCardName());
        cardTransform = this.GetComponent<Transform>();
    }

}
