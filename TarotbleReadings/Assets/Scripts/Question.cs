using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "Scriptable Objects/Question")]
public class Question : ScriptableObject
{
    [SerializeField] private string question;
    [SerializeField] private string yesAnswer;
    [SerializeField] private string noAnswer;
    [SerializeField] private int yesNextQ;
    [SerializeField] private int noNextQ;


    public string GetQuestion(){
        return question;
    }
}
