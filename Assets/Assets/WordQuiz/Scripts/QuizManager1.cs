using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager1 : MonoBehaviour
{
    public static QuizManager1 instance; //Instance to make it available in other scripts without reference

    [SerializeField] private GameObject gameComplete;
    // Reference to the QuizData ScriptableObject that holds all question data
    [SerializeField] private QuizDataScriptable quizDataScriptable;
    [SerializeField] private Image questionImage;           // Image element to show the image
    [SerializeField] private WordData[] answerWordList;     // List of answer words in the game
    [SerializeField] private WordData[] optionsWordList;    // List of option words in the game

    private GameStatus gameStatus = GameStatus.Playing;     // To keep track of game status
    private char[] wordsArray = new char[12];               // Array which stores chars for options

    private List<int> selectedWordsIndex;                   // List which keeps track of selected option indices
    private int currentAnswerIndex = 0, currentQuestionIndex = 0;   // Indices to keep track of current question and answer
    private bool correctAnswer = true;                      // Bool to decide if the answer is correct
    private string answerWord;                              // String to store the answer of the current question

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        selectedWordsIndex = new List<int>();           // Create a new list at start
        SetQuestion();                                  // Set the first question
    }

    void SetQuestion()
    {
        gameStatus = GameStatus.Playing;                // Set GameStatus to Playing 

        // Get the answerWord from the current question in the QuizData ScriptableObject
        answerWord = quizDataScriptable.questions[currentQuestionIndex].answer;

        // Set the image of the question
        questionImage.sprite = quizDataScriptable.questions[currentQuestionIndex].questionImage;

        ResetQuestion();                               // Reset answers and options to original state

        selectedWordsIndex.Clear();                     // Clear the list for new question
        Array.Clear(wordsArray, 0, wordsArray.Length);  // Clear the array

        // Add the correct char to the wordsArray
        for (int i = 0; i < answerWord.Length; i++)
        {
            wordsArray[i] = char.ToUpper(answerWord[i]);
        }

        // Add random dummy chars to wordsArray for extra options
        for (int j = answerWord.Length; j < wordsArray.Length; j++)
        {
            wordsArray[j] = (char)UnityEngine.Random.Range(65, 90);
        }

        // Shuffle the words array
        wordsArray = ShuffleList.ShuffleListItems<char>(wordsArray.ToList()).ToArray();

        // Set the option words Text value
        for (int k = 0; k < optionsWordList.Length; k++)
        {
            optionsWordList[k].SetWord(wordsArray[k]);
        }
    }

    // Method called on Reset Button click and on a new question
    public void ResetQuestion()
    {
        // Activate all answerWordList gameobjects and set their word to "_"
        for (int i = 0; i < answerWordList.Length; i++)
        {
            answerWordList[i].gameObject.SetActive(true);
            answerWordList[i].SetWord('_');
        }

        // Deactivate the unwanted answerWordList gameobjects (objects that exceed the answer string length)
        for (int i = answerWord.Length; i < answerWordList.Length; i++)
        {
            answerWordList[i].gameObject.SetActive(false);
        }

        // Activate all optionsWordList objects
        for (int i = 0; i < optionsWordList.Length; i++)
        {
            optionsWordList[i].gameObject.SetActive(true);
        }

        currentAnswerIndex = 0;
    }

    /// <summary>
    /// This method is called when we click on any option button
    /// </summary>
    /// <param name="value"></param>
    public void SelectedOption(WordData value)
    {
        // If gameStatus is Next or currentAnswerIndex is greater than or equal to answerWord length, do nothing
        if (gameStatus == GameStatus.Next || currentAnswerIndex >= answerWord.Length) return;

        selectedWordsIndex.Add(value.transform.GetSiblingIndex()); // Add the selected option's index to the list
        value.gameObject.SetActive(false); // Deactivate the selected option object
        answerWordList[currentAnswerIndex].SetWord(value.wordValue); // Set the answer word list to the selected word

        currentAnswerIndex++;   // Increment the currentAnswerIndex

        // If the currentAnswerIndex is equal to the answerWord length
        if (currentAnswerIndex == answerWord.Length)
        {
            correctAnswer = true;   // Assume the answer is correct initially
            // Loop through answerWordList and check if the selected words match the correct answer
            for (int i = 0; i < answerWord.Length; i++)
            {
                // If the selected word does not match the correct word
                if (char.ToUpper(answerWord[i]) != char.ToUpper(answerWordList[i].wordValue))
                {
                    correctAnswer = false; // Set correctAnswer to false
                    break; // Break out of the loop if incorrect
                }
            }

            // If the answer is correct
            if (correctAnswer)
            {
                Debug.Log("Correct Answer");
                gameStatus = GameStatus.Next; // Change the game status to "Next"
                currentQuestionIndex++; // Increment the currentQuestionIndex

                // If there are more questions left
                if (currentQuestionIndex < quizDataScriptable.questions.Count)
                {
                    Invoke("SetQuestion", 0.5f); // Go to the next question after a brief delay
                }
                else
                {
                    Debug.Log("Game Complete"); // If no more questions, display "Game Complete"
                    gameComplete.SetActive(true);
                }
            }
        }
    }

    // Method to reset the last selected word (if the user wants to undo)
    public void ResetLastWord()
    {
        if (selectedWordsIndex.Count > 0)
        {
            int index = selectedWordsIndex[selectedWordsIndex.Count - 1];
            optionsWordList[index].gameObject.SetActive(true); // Reactivate the last option
            selectedWordsIndex.RemoveAt(selectedWordsIndex.Count - 1); // Remove it from the list

            currentAnswerIndex--; // Decrease the currentAnswerIndex
            answerWordList[currentAnswerIndex].SetWord('_'); // Reset the word to "_"
        }
    }
}

// Enum for the game status
public enum GameStatus
{
   Next,
   Playing
}
