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
    private CardScript viewedCard;

    private Character currentCustomer;
    [SerializeField] private CharacterScript characterScript;

    //UI STUFF_----------------------------------------------------
    [SerializeField] private TextMeshProUGUI dialog;
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private GameObject yesButton;
    [SerializeField] private GameObject noButton;
    [SerializeField] private GameObject deckButton;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject flipButton;
    [SerializeField] private GameObject exitViewButton;
    [SerializeField] private GameObject byeButton;
    [SerializeField] private GameObject nextCustomerButton;

    [SerializeField] private TextFader textFader;           // The script attached to the dialog box that will populate it
    

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
    //present
    private float _spotPresentX;
    //future
    private float _spotFutureX;
    //bonus
    private float _spotBonusX;


    //zoom
     private float _spotScaleZoom;
    private float _spotRotationXZoom;
    private float _spotZoomX;
    private float _spotZoomY;
    private float _spotZoomZ;

    //for deck position
    private Vector3 _deckPosition;
    private Vector3 _deckRotation;

    private int deckCounter;

    void Awake(){
        //singleton stuff
        if(GMInstance == null){
            GMInstance = this;
        }
        else{
            Destroy(gameObject);
        }

        _pulledCards = new List<GameObject>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        ChangeState(GameState.customerArrival);
        _spotScale = 0.31f;
        _spotRotationX = 60f;
        _spotPastX = -1.52f;
        _spotPastY = -2.49f;
       _spotPastZ = -3.85f;
       _spotPresentX = 0.24f;
       _spotFutureX = 1.99f;
       _spotBonusX = 3.77f;

        //zoom
        _spotRotationXZoom = 0f;
        _spotZoomX = -0.27f;
        _spotZoomY = 0f;
       _spotZoomZ = -8.2f;

       //deck position
       _deckPosition = new Vector3(-3.93f,-2.49f,-3.85f);
       _deckRotation = new Vector3(-60f,0f,0f);
       deckCounter = 0;

        if(AudioManager.instance != null)
        {
            AudioManager.PlaySound(AudioManager.SoundType.Prismatic, 1f);
        }
       

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
        //reset deck
        deckCounter = 0;
        deck.ClearPulledCards();
        _pulledCards.Clear();

        Debug.Log("CustomerArrival");
        deckButton.GetComponent<Button>().interactable = false;
        yesButton.SetActive(false);
        yesButton.GetComponent<Button>().interactable = false;
        noButton.GetComponent<Button>().interactable = false;
        flipButton.SetActive(false);
        exitViewButton.SetActive(false);
        nextCustomerButton.SetActive(false);
        byeButton.SetActive(false);
        nextButton.SetActive(true);
        // slide in a character
        currentCustomer = PickCustomer();
        characterScript.UpdateCharacter(currentCustomer);
        characterScript.MoveToCenter();

        // display the dialog (MAKE SOMETHING FANCIER SO DIALOG APPEARS PROGRESSIVELY)
        dialogBox.SetActive(true);
        // dialog.text = currentCustomer.GetCurrentQuestion().GetQuestion();
        textFader.ShowText(currentCustomer.GetCurrentQuestion().GetQuestion());
        flipButton.GetComponent<Button>().interactable = true;



    }

     private void PastState(){

        Debug.Log("Past");
        //activate the deck button
        deckButton.GetComponent<Button>().interactable = true;
        yesButton.SetActive(true);
        noButton.SetActive(true);
        nextButton.SetActive(false);
        flipButton.SetActive(false);
        exitViewButton.SetActive(false);
        flipButton.GetComponent<Button>().interactable = true;
        // when clicked, go to view mode
        //pick a card
        //assign the past spot to the card
       
    }

     private void PresentState(){
        deckButton.GetComponent<Button>().interactable = true;
        Debug.Log("Present");
        flipButton.SetActive(false);
        exitViewButton.SetActive(false);
        flipButton.GetComponent<Button>().interactable = true;
    }

     private void FutureState(){
        deckButton.GetComponent<Button>().interactable = true;
        Debug.Log("Future");
        flipButton.SetActive(false);
        exitViewButton.SetActive(false);
        yesButton.GetComponent<Button>().interactable = true;
        noButton.GetComponent<Button>().interactable = true;
        flipButton.GetComponent<Button>().interactable = true;
        
    }

     private void BonusState(){
        Debug.Log("Bonus");
        flipButton.SetActive(false);
        exitViewButton.SetActive(false);
        yesButton.GetComponent<Button>().interactable = true;
        noButton.GetComponent<Button>().interactable = true;
        deckButton.GetComponent<Button>().interactable = false;
        flipButton.GetComponent<Button>().interactable = true;
    }

     private void ResponseState(){
        Debug.Log("Response");
        yesButton.SetActive(false);
        noButton.SetActive(false);
        displayAnswer();
        byeButton.SetActive(true);
    }

     private void LeaveState(){
        Debug.Log("Leave");
        //close dialog
        deckButton.GetComponent<Button>().interactable = false;
        yesButton.SetActive(false);
        noButton.SetActive(false);
        flipButton.SetActive(false);
        exitViewButton.SetActive(false);
        dialogBox.SetActive(false);
        //move character out
        characterScript.MoveOffScreen();

        //next customer button
        nextCustomerButton.SetActive(true);
        byeButton.SetActive(false);
        DestroyCards();

    }

    private void ViewState(){
        Debug.Log("View");
        //disable all buttons
        yesButton.GetComponent<Button>().interactable = false;
        noButton.GetComponent<Button>().interactable = false;
        deckButton.GetComponent<Button>().interactable = false;
    }

    //------------------------------EHLPERS---------------------------//

    public void PlaceCard(Spot spot, CardScript card){
        float viewedRotationX = viewedCard.GetCardData().GetReverse() ? 240 : -60;
        float viewedRotationY = viewedCard.GetCardData().GetReverse() ? 0: -180f;


        //place in the correctSpot
        switch (spot){
            case Spot.past:
               Sequence.Create(cycles: 1, CycleMode.Yoyo)
                 //shrink card
                 .Group(Tween.Scale(card.GetTransform(), endValue: _spotScale, duration: 2f, startDelay: 1))
                 .Group(Tween.Rotation(card.GetTransform(), endValue: new Vector3(viewedRotationX, viewedRotationY, 0f), duration: 2f))
                 //place at past
                 .Group(Tween.PositionX(card.GetTransform(), endValue: _spotPastX, duration: 2f))
                 .Group(Tween.PositionY(card.GetTransform(), endValue: _spotPastY, duration: 2f))
                 .Group(Tween.PositionZ(card.GetTransform(), endValue: _spotPastZ, duration: 2f));
                break;
            case Spot.present:
                Sequence.Create(cycles: 1, CycleMode.Yoyo)
                 //shrink card
                 .Group(Tween.Scale(card.GetTransform(), endValue: _spotScale, duration: 2f, startDelay: 1))
                 .Group(Tween.Rotation(card.GetTransform(), endValue: new Vector3(viewedRotationX, viewedRotationY, 0f), duration: 2f))
                 //place at past
                 .Group(Tween.PositionX(card.GetTransform(), endValue: _spotPresentX, duration: 2f))
                 .Group(Tween.PositionY(card.GetTransform(), endValue: _spotPastY, duration: 2f))
                 .Group(Tween.PositionZ(card.GetTransform(), endValue: _spotPastZ, duration: 2f));
            break;
            case Spot.future:
                Sequence.Create(cycles: 1, CycleMode.Yoyo)
                 //shrink card
                 .Group(Tween.Scale(card.GetTransform(), endValue: _spotScale, duration: 2f, startDelay: 1))
                 .Group(Tween.Rotation(card.GetTransform(), endValue: new Vector3(viewedRotationX, viewedRotationY, 0f), duration: 2f))
                 //place at past
                 .Group(Tween.PositionX(card.GetTransform(), endValue: _spotFutureX, duration: 2f))
                 .Group(Tween.PositionY(card.GetTransform(), endValue: _spotPastY, duration: 2f))
                 .Group(Tween.PositionZ(card.GetTransform(), endValue: _spotPastZ, duration: 2f));
            break;
            case Spot.bonus:
                Sequence.Create(cycles: 1, CycleMode.Yoyo)
                 //shrink card
                 .Group(Tween.Scale(card.GetTransform(), endValue: _spotScale, duration: 2f, startDelay: 1))
                 .Group(Tween.Rotation(card.GetTransform(), endValue: new Vector3(viewedRotationX, viewedRotationY, 0f), duration: 2f))
                 //place at past
                 .Group(Tween.PositionX(card.GetTransform(), endValue: _spotBonusX, duration: 2f))
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
        viewedCard = card;
        previousState = gameState;

        float viewedRotationX = viewedCard.GetCardData().GetReverse() ? 180f : 0f;
        float viewedRotationY = viewedCard.GetCardData().GetReverse() ? 180f: 0f;

        // viewedCard.GetTransform().rotation = Quaternion.Euler(0f, 180f, 0f);
        ChangeState(GameState.view);
        Debug.Log(card.GetTransform());


        //move card to the center of the screen to view it
        Sequence.Create(cycles: 1, CycleMode.Yoyo)
                 .Group(Tween.Rotation(card.GetTransform(), endValue: new Vector3(viewedRotationX, viewedRotationY, 0f), duration: 2f))
                 //place in the middle of the screen
                 .Group(Tween.PositionX(card.GetTransform(), endValue: _spotZoomX, duration: 2f))
                 .Group(Tween.PositionY(card.GetTransform(), endValue: _spotZoomY, duration: 2f))
                 .Group(Tween.PositionZ(card.GetTransform(), endValue: _spotZoomZ, duration: 2f));



        flipButton.SetActive(true);
        exitViewButton.SetActive(true);
    }

    public void deckClicked(){
        switch(deckCounter){
        case 0:
            ChangeState(GameState.past);
            break;
        case 1:
            //go to present
            ChangeState(GameState.present);
            break;
        case 2:
            //go to future
            ChangeState(GameState.future);
            break;
        case 3:
            //go to bonus
            ChangeState(GameState.bonus);
            break;
       }
       CardScript pulledCard = CreateNewCard();
       ViewCard(pulledCard);
       deckCounter += 1;
    }

    public void nextClicked(){
        //changes state!
        ChangeState(GameState.past);
    }

    public void flipClicked(){
        
        float viewedRotationX = viewedCard.GetCardData().GetReverse() ? 180 : 0f;
        float viewedRotationY = viewedCard.GetCardData().GetReverse() ? 0f: 180f;
        Debug.Log(viewedCard.GetCardData().GetReverse());
      
        //rotate card to show other side
        Tween.Rotation(viewedCard.GetTransform(), endValue: new Vector3(viewedRotationX, viewedRotationY, 0f), duration: 2f);
        //disable the button
        //you get ONE flip
        flipButton.GetComponent<Button>().interactable = false;
    }

    public void exitClicked(){
        Spot spot = Spot.past;
        //moves card to its spot
        PlaceCard(viewedCard.GetSpot(), viewedCard);

        ChangeState(previousState);
    }

    public void yesClicked(){
        // let it know which answer was selected
        currentCustomer.GetCurrentQuestion().SetAnswer(Answer.yes);
        //set up the next question
        currentCustomer.SetCurrentQuestion(currentCustomer.GetCurrentQuestion().GetYesNextQ());
        //go to response state
        ChangeState(GameState.response);
    }

    public void noClicked(){
        // let it know which answer was selected
        currentCustomer.GetCurrentQuestion().SetAnswer(Answer.no);
        //set up the next question
        currentCustomer.SetCurrentQuestion(currentCustomer.GetCurrentQuestion().GetNoNextQ());
        //go to response state
        ChangeState(GameState.response);
    }

    public void byeClicked(){
        //switch to leave state
        ChangeState(GameState.leave);
    }

    public void nextCustomerClicked(){
        characterScript.ResetPosition();
        ChangeState(GameState.customerArrival);

    }

    public CardScript CreateNewCard(){
         //instantiate a new card prefab
        GameObject newCard = Instantiate(cardPrefab, _deckPosition, Quaternion.Euler(_deckRotation));
        newCard.GetComponent<Transform>().localScale = new Vector3(_spotScale,_spotScale,_spotScale);
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

    public void displayAnswer(){
        if(currentCustomer.GetCurrentQuestion().GetAnswer() == Answer.yes){
            textFader.ShowText(currentCustomer.GetCurrentQuestion().GetYesAnswer());
        }
        else if(currentCustomer.GetCurrentQuestion().GetAnswer() == Answer.no){
            textFader.ShowText(currentCustomer.GetCurrentQuestion().GetNoAnswer());
        }
        
    }

    public void DestroyCards(){
        for(int i = 0; i < _pulledCards.Count ; i++){
            Destroy(_pulledCards[i]);
        }
    }
}
