using UnityEngine;
using PrimeTween;

public class CharacterScript : MonoBehaviour
{
    [SerializeField] private Character characterData;
    [SerializeField] private SpriteRenderer portrait;
    [SerializeField] private Transform characterTransform;
    [SerializeField] private float middlePosition;
    [SerializeField] private float endPosition;
    [SerializeField] private float startPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        portrait.sprite = characterData.GetImage();
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToCenter(){
        Tween.PositionX(characterTransform, endValue: middlePosition, duration: 2, ease: Ease.InOutSine);
    }

    public void MoveOffScreen(){
        Tween.PositionX(characterTransform, endValue: endPosition, duration: 2, ease: Ease.InOutSine);
      
    }

    public void ResetPosition(){
        Tween.PositionX(characterTransform, endValue: startPosition, duration: 0, ease: Ease.InOutSine);
    }
}
