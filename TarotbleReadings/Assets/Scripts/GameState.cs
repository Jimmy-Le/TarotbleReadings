using UnityEngine;

public enum GameState{
    mainMenu,
    tutorial,
    customerArrival,
    past,
    present,
    future,
    bonus,
    response,
    leave
}
    //0 --> customer walks in
    //1 --> picking past
    //2 --> picking present
    //3 --> picking future (YES/NO ARE ENABLED)
    //4 --> Bonus card?
    //5 --> (yes/no chosen, get the answer)
    //6 --> Goodbye, restart