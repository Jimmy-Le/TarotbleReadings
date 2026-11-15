using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using PrimeTween;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Character> characters;

    [SerializeField] private Deck deck;

    //0 --> customer walks in
    //1 --> picking past
    //2 --> picking present
    //3 --> picking future (YES/NO ARE ENABLED)
    //4 --> Bonus card?
    //5 --> (yes/no chosen, get the answer)
    //6 --> Goodbye, restart
    private GameState gameState;

    private Character currentCustomer;
    [SerializeField] private CharacterScript characterScript;

    public static GameManager GMInstance;

    private float _spotScale;
    private float _spotRotationX;
    private float _spotPastX;
    private float _spotPastY;
    private float _spotPastZ;

    void Awake(){
        //singleton stuff
        if(GMInstance == null){
            GMInstance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameState = GameState.customerArrival;
        _spotScale = 0.31f;
        _spotRotationX = -60f;
        _spotPastX = -1.52f;
        _spotPastY = -2.49f;
       _spotPastZ = -3.85f;
    }

    void Update(){
        
    }

    public Character PickCustomer(){
        int randomIndex = Random.Range(0, characters.Count);
        return characters[randomIndex];
    }

    public void ChangeState(GameState newState){
        gameState = newState;
        switch(gameState){
            case GameState.mainMenu:
                MainMenuState();
                break;
            case GameState.tutorial:
                TutorialState();
                break;
            case GameState.customerArrival:
                CustomerArrivalState();
                break;
            case GameState.past:
                PastState();
                break;
            case GameState.present:
                PresentState();
                break;
            case GameState.future:
                FutureState();
                break;
            case GameState.bonus:
                BonusState();
                break;
            case GameState.response:
                ResponseState();
                break;
            case GameState.leave:
                LeaveState();
                break;
        }
    }

    private void MainMenuState(){
        Debug.Log("main menu");
    }

    private void TutorialState(){
        Debug.Log("tutorial");
    }

     private void CustomerArrivalState(){
        Debug.Log("CustomerArrival");
        // slide in a character
        currentCustomer = PickCustomer();
        characterScript.UpdateCharacter(currentCustomer);
        characterScript.MoveToCenter();

        // display the dialog
        // click next on dialog to switch to the pastState
    }

     private void PastState(){
        Debug.Log("Past");
        //activate the deck button
        // when clicked, placed card on past spot, then move to present
    }

     private void PresentState(){
        Debug.Log("Present");
    }

     private void FutureState(){
        Debug.Log("Future");
    }

     private void BonusState(){
        Debug.Log("Bonus");
    }

     private void ResponseState(){
        Debug.Log("Response");
    }

     private void LeaveState(){
        Debug.Log("Leave");
    }

    public void placeCard(Spot spot, Card card){
        //place in the correctSpot
        switch (spot){
            case Spot.past:
               Sequence.Create(cycles: 10, CycleMode.Yoyo)
                 //shrink card
                 .Group(Tween.Scale(card.GetTransform(), endValue: _spotScale, duration: 2f, startDelay: 1))
                 .Group(Tween.Rotation(card.GetTransform(), endValue: new Vector3(_spotRotationX, -180f, 0f), duration: 2f))
                 //place at past
                 .Group(Tween.PositionX(card.GetTransform(), endValue: _spotPastX, duration: 2f))
                 .Group(Tween.PositionY(card.GetTransform(), endValue: _spotPastY, duration: 2f))
                 .Group(Tween.PositionZ(card.GetTransform(), endValue: _spotPastZ, duration: 2f));
                break;
        }
    }
}
