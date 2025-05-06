using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TrueFalseQuizManager : MonoBehaviour
{
    public Text questionText; // Or TMP_Text if using TextMeshPro
    public Button trueButton;
    public Button falseButton;
    public Text feedbackText;
    public Text scoreText;
    public Button finishButton;

    private List<Question> questions = new List<Question>();
    private int currentQuestionIndex = 0;

    void Start()
    {
        feedbackText.text = "";
        scoreText.text = "Score: " + GameData.finalScore;
        finishButton.gameObject.SetActive(false);
        LoadQuestions();
        ShowQuestion();
    }

    void LoadQuestions()
{
    questions = new List<Question>()
    {
        new Question {
            questionText = "In 'Titanic', Jack dies at the end of the movie.",
            options = new string[] { "True", "False" },
            correctAnswerIndex = 0,
            questionType = QuestionType.TrueFalse
        },
        new Question {
            questionText = "The Marvel Cinematic Universe began with 'Iron Man' in 2008.",
            options = new string[] { "True", "False" },
            correctAnswerIndex = 0,
            questionType = QuestionType.TrueFalse
        },
        new Question {
            questionText = "The movie 'Inception' was directed by Steven Spielberg.",
            options = new string[] { "True", "False" },
            correctAnswerIndex = 1,
            questionType = QuestionType.TrueFalse
        },
        new Question {
            questionText = "In 'The Lion King', Simba's father is named Mufasa.",
            options = new string[] { "True", "False" },
            correctAnswerIndex = 0,
            questionType = QuestionType.TrueFalse
        },
        new Question {
            questionText = "Keanu Reeves plays Neo in 'The Matrix' series.",
            options = new string[] { "True", "False" },
            correctAnswerIndex = 0,
            questionType = QuestionType.TrueFalse
        },
        new Question {
            questionText = "'The Godfather' was released in the 1990s.",
            options = new string[] { "True", "False" },
            correctAnswerIndex = 1,
            questionType = QuestionType.TrueFalse
        },
        new Question {
            questionText = "The character 'Forrest Gump' is portrayed by Tom Hanks.",
            options = new string[] { "True", "False" },
            correctAnswerIndex = 0,
            questionType = QuestionType.TrueFalse
        },
        new Question {
            questionText = "In 'Avengers: Endgame', Tony Stark sacrifices himself.",
            options = new string[] { "True", "False" },
            correctAnswerIndex = 0,
            questionType = QuestionType.TrueFalse
        },
        new Question {
            questionText = "The movie 'Frozen' is produced by DreamWorks Animation.",
            options = new string[] { "True", "False" },
            correctAnswerIndex = 1,
            questionType = QuestionType.TrueFalse
        },
        new Question {
            questionText = "'Joker' (2019) is set in the Marvel Universe.",
            options = new string[] { "True", "False" },
            correctAnswerIndex = 1,
            questionType = QuestionType.TrueFalse
        }
    };
}


    void ShowQuestion()
    {
        if (currentQuestionIndex >= questions.Count)
        {
            questionText.text = "";
            feedbackText.text = "";
            scoreText.text = "Final Score: " + GameData.finalScore;
            scoreText.alignment = TextAnchor.MiddleCenter;
            scoreText.rectTransform.anchoredPosition = new Vector2(0, 50);

            finishButton.gameObject.SetActive(true);
            finishButton.GetComponentInChildren<Text>().text = "Finish";
            finishButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -50);

            trueButton.gameObject.SetActive(false);
            falseButton.gameObject.SetActive(false);

            return;
        }

        Question q = questions[currentQuestionIndex];
        questionText.text = q.questionText;
        feedbackText.text = "";

        trueButton.onClick.RemoveAllListeners();
        falseButton.onClick.RemoveAllListeners();

        trueButton.onClick.AddListener(() => OnAnswerSelected(0));
        falseButton.onClick.AddListener(() => OnAnswerSelected(1));
    }

    void OnAnswerSelected(int index)
    {
        bool correct = index == questions[currentQuestionIndex].correctAnswerIndex;
        feedbackText.text = correct ? "Correct!" : "Wrong!";

        if (correct)
        {
            GameData.finalScore++;
            scoreText.text = "Score: " + GameData.finalScore;
        }

        currentQuestionIndex++;
        Invoke("ShowQuestion", 1.5f);
    }

    public void OnFinishButtonClicked()
    {
        // You can load another scene or return to main menu
        SceneManager.LoadScene("WordGame"); // Replace with your actual final scene name
    }
}
