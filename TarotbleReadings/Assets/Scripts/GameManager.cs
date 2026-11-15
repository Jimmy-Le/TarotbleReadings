using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using PrimeTween;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Character> characters;

    [SerializeField] private Deck deck;

    private GameState gameState;

    private Character currentCustomer;
    [SerializeField] private CharacterScript characterScript;

    [SerializeField] private TextMeshProUGUI dialog;
    [SerializeField] private GameObject dialogBox;

    public static GameManager GMInstance;

    //for placing cards
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
        ChangeState(GameState.customerArrival);
        _spotScale = 0.31f;
        _spotRotationX = -60f;
        _spotPastX = -1.52f;
        _spotPastY = -2.49f;
       _spotPastZ = -3.85f;
    }

    void Update(){
        
    }

  //-----------------CHANGE STATE------------------------//

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

    //-----------------------STATE FUNCTIONS----------------------------//

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

        // display the dialog (MAKE SOMETHING FANCIER SO DIALOG APPEARS PROGRESSIVELY)
        dialogBox.SetActive(true);
        dialog.text = currentCustomer.GetCurrentQuestion().GetQuestion();

        // once dialog is displayed, we can move to the past state
        ChangeState(GameState.past);
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

    //------------------------------EHLPERS---------------------------//

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

      public Character PickCustomer(){
        int randomIndex = Random.Range(0, characters.Count);
        return characters[randomIndex];
    }
}
