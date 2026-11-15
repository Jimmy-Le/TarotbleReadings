using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "Scriptable Objects/Question")]
public class Question : ScriptableObject
{
    [SerializeField] private string question;
    [SerializeField] private string yesAnswer;
    [SerializeField] private string noAnswer;
    [SerializeField] private int yesNextQ;
    [SerializeField] private int noNextQ;
    private Answer answer;


    public void SetAnswer(Answer newAnswer){
        answer = newAnswer;
    }
    public Answer GetAnswer(){
        return answer;
    }

    public string GetQuestion(){
        return question;
    }

    public string GetYesAnswer(){
        return yesAnswer;
    }

    public string GetNoAnswer(){
        return noAnswer;
    }

    public int GetYesNextQ(){
        return yesNextQ;
    }

    public int GetNoNextQ(){
        return noNextQ;
    }
}
