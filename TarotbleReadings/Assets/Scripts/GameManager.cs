using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private List<Character> characters;
    //0 --> customer walks in
    //1 --> picking past
    //2 --> picking present
    //3 --> picking future (YES/NO ARE ENABLED)
    //4 --> Bonus card?
    //5 --> (yes/no chosen, get the answer)
    //6 --> Goodbye, restart
    private GameState gameState;

    private Character currentCustomer;

    public static GameManager GMInstance;

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
        // display the dialog
    }

     private void PastState(){
        Debug.Log("Past");
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

}
