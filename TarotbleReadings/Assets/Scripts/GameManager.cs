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
    private GameState previousState;

    private Character currentCustomer;
    [SerializeField] private CharacterScript characterScript;

    //UI STUFF_----------------------------------------------------
    [SerializeField] private TextMeshProUGUI dialog;
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private GameObject yesButton;
    [SerializeField] private GameObject noButton;

    public static GameManager GMInstance;

    [SerializeField] private GameObject cardPrefab;

    private List<GameObject> _pulledCards;
    //for placing cards
    private float _spotScale;
    private float _spotRotationX;
    //past
    private float _spotPastX;
    private float _spotPastY;
    private float _spotPastZ;

    //zoom
     private float _spotScaleZoom;
    private float _spotRotationXZoom;
    private float _spotZoomX;
    private float _spotZoomY;
    private float _spotZoomZ;

    //for deck position
    private Vector3 _deckPosition;
    private Vector3 _deckRotation;

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

        //zoom
        _spotRotationXZoom = 0f;
        _spotZoomX = 0f;
        _spotZoomY = 0f;
       _spotZoomZ = -8.2f;

       //deck position
       _deckPosition = new Vector3(-3.93f,-2.49f,-3.85f);
       _deckRotation = new Vector3(-60f,180f,0f);

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
            case GameState.view:
                ViewState();
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
        // when clicked, go to view mode
        //pick a card
        //assign the past spot to the card
       
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

    private void ViewState(){
        Debug.Log("View");
        //disable all buttons
        yesButton.GetComponent<Button>().interactable = false;
        noButton.GetComponent<Button>().interactable = false;
        //return to previous state once view is removed
    }

    //------------------------------EHLPERS---------------------------//

    public void PlaceCard(Spot spot, CardScript card){
        //place in the correctSpot
        switch (spot){
            case Spot.past:
               Sequence.Create(cycles: 1, CycleMode.Yoyo)
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

    public void ViewCard(CardScript card){
        previousState = gameState;
        ChangeState(GameState.view);
        //move card to the center of the screen to view it
        Sequence.Create(cycles: 1, CycleMode.Yoyo)
                 .Group(Tween.Rotation(card.GetTransform(), endValue: new Vector3(_spotRotationXZoom, -180f, 0f), duration: 2f))
                 //place in the middle of the screen
                 .Group(Tween.PositionX(card.GetTransform(), endValue: _spotZoomX, duration: 2f))
                 .Group(Tween.PositionY(card.GetTransform(), endValue: _spotZoomY, duration: 2f))
                 .Group(Tween.PositionZ(card.GetTransform(), endValue: _spotZoomZ, duration: 2f));
    }

    public void deckClicked(){
       CardScript pulledCard = CreateNewCard();
       ViewCard(pulledCard);
    }

    public CardScript CreateNewCard(){
         //instantiate a new card prefab
        GameObject newCard = Instantiate(cardPrefab, _deckPosition, Quaternion.Euler(_deckRotation));
        //give it the card data
        newCard.GetComponent<CardScript>().SetCardData(deck.pickCard());
        //add the card to the pulled cards list
        _pulledCards.Add(newCard);
        //assign a spot to the card
        switch(gameState){
            case GameState.past:
                newCard.GetComponent<CardScript>().SetSpot(Spot.past);
                break;
             case GameState.present:
                newCard.GetComponent<CardScript>().SetSpot(Spot.present);
                break;
            case GameState.future:
                newCard.GetComponent<CardScript>().SetSpot(Spot.future);
                break;
            case GameState.bonus:
                newCard.GetComponent<CardScript>().SetSpot(Spot.bonus);
                break;
        }
        return  newCard.GetComponent<CardScript>();
    }
}
