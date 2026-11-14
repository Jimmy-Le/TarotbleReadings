using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Character", menuName = "Scriptable Objects/Character")]
public class Character : ScriptableObject
{
    [SerializeField] private List<Question> questions;
    [SerializeField] private Sprite image;
    private int currentQuestion;

    void Awake(){
        currentQuestion = 0;
    }

    public void ChangeQuestion(int index){
        currentQuestion = index;
    }

    public Question GetCurrentQuestion(){
        return questions[currentQuestion];
    }

}
