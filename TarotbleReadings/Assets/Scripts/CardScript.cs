using UnityEngine;
using TMPro;

public class CardScript : MonoBehaviour
{
    [SerializeField] private Card cardData;
    [SerializeField] private MeshRenderer meshRendererImage;
    [SerializeField] private MeshRenderer meshRendererBorder;
    [SerializeField] private TextMeshPro textMeshProName;
  
   
    void Awake()
    {
        //check if flipped
        if(cardData.GetReverse()){
            this.GetComponent<Transform>().localRotation *= Quaternion.Euler(0,0,180);
        }
        //setting the images
        meshRendererImage.material = cardData.GetImage();
        //border
        meshRendererBorder.material = cardData.GetBorder();
        //name
        textMeshProName.SetText(cardData.GetCardName());

        

    }
}
