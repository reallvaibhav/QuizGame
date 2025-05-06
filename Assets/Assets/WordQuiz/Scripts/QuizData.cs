using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuizData", menuName = "QuizData", order = 1)]
public class QuizDataScriptable : ScriptableObject
{
    public List<QuestionData> questions;
}

[System.Serializable]
public class QuestionData
{
    public Sprite questionImage;  // Image of the question (e.g., Ronaldo image)
    public string answer;         // Answer to the question (e.g., "cristiano")
}
