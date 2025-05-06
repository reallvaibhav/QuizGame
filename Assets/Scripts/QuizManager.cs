using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public Text questionText;
    public Button[] answerButtons;
    public Text feedbackText;
    public Text scoreText;
    public Button finishButton;

    private List<Question> questions = new List<Question>();
    private int currentQuestionIndex = 0;
    private int score = 0;

    void Start()
    {
        feedbackText.text = "";
        scoreText.text = "Score: 0";
        finishButton.gameObject.SetActive(false);
        LoadQuestions();
        ShowQuestion();
    }
    
void LoadQuestions()
{
    questions = new List<Question>() {
        new Question {
            questionText = "Who directed the movie 'Inception'?",
            options = new string[] { "Christopher Nolan", "Steven Spielberg", "James Cameron", "Quentin Tarantino" },
            correctAnswerIndex = 0,
            questionType = QuestionType.MultipleChoice
        },
        new Question {
            questionText = "Which actor plays Iron Man in the Marvel Cinematic Universe?",
            options = new string[] { "Chris Evans", "Robert Downey Jr.", "Chris Hemsworth", "Mark Ruffalo" },
            correctAnswerIndex = 1,
            questionType = QuestionType.MultipleChoice
        },
        new Question {
            questionText = "'The Godfather' was released in which year?",
            options = new string[] { "1965", "1972", "1980", "1990" },
            correctAnswerIndex = 1,
            questionType = QuestionType.MultipleChoice
        },
        new Question {
            questionText = "In which movie does the quote 'I'll be back' appear?",
            options = new string[] { "Die Hard", "Predator", "The Terminator", "Robocop" },
            correctAnswerIndex = 2,
            questionType = QuestionType.MultipleChoice
        },
        new Question {
            questionText = "Which movie features a ship called the Black Pearl?",
            options = new string[] { "Titanic", "Pirates of the Caribbean", "Jaws", "Cast Away" },
            correctAnswerIndex = 1,
            questionType = QuestionType.MultipleChoice
        },
        new Question {
            questionText = "Who played the Joker in 'The Dark Knight' (2008)?",
            options = new string[] { "Joaquin Phoenix", "Heath Ledger", "Jared Leto", "Jack Nicholson" },
            correctAnswerIndex = 1,
            questionType = QuestionType.MultipleChoice
        },
        new Question {
            questionText = "Which film won the Best Picture Oscar in 2020?",
            options = new string[] { "1917", "Joker", "Parasite", "Ford v Ferrari" },
            correctAnswerIndex = 2,
            questionType = QuestionType.MultipleChoice
        },
        new Question {
            questionText = "Which Disney movie features the song 'Let It Go'?",
            options = new string[] { "Moana", "Tangled", "Frozen", "Encanto" },
            correctAnswerIndex = 2,
            questionType = QuestionType.MultipleChoice
        },
        new Question {
            questionText = "What is the name of Harry Potter’s pet owl?",
            options = new string[] { "Scabbers", "Fawkes", "Hedwig", "Errol" },
            correctAnswerIndex = 2,
            questionType = QuestionType.MultipleChoice
        },
        new Question {
            questionText = "Which Marvel film introduced the character Black Panther?",
            options = new string[] { "Black Panther", "Avengers: Infinity War", "Captain America: Civil War", "Iron Man 3" },
            correctAnswerIndex = 2,
            questionType = QuestionType.MultipleChoice
        }
    };
}

    void ShowQuestion()
    {
        if (currentQuestionIndex >= questions.Count)
        {
            questionText.text = "";
            feedbackText.text = "";
            scoreText.text = "Your Score: " + score;
            scoreText.alignment = TextAnchor.MiddleCenter;
            scoreText.rectTransform.anchoredPosition = new Vector2(0, 50);

            finishButton.gameObject.SetActive(true);
            finishButton.GetComponentInChildren<Text>().text = "Proceed";
            finishButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -50);

            foreach (var btn in answerButtons)
            {
                btn.gameObject.SetActive(false);
            }

            return;
        }

        Question q = questions[currentQuestionIndex];
        questionText.text = q.questionText;
        feedbackText.text = "";

        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < q.options.Length)
            {
                answerButtons[i].gameObject.SetActive(true);
                Text buttonText = answerButtons[i].GetComponentInChildren<Text>();
                TMP_Text buttonTMPText = answerButtons[i].GetComponentInChildren<TMP_Text>();

                if (buttonTMPText != null)
                {
                    buttonTMPText.text = q.options[i];
                }
                else if (buttonText != null)
                {
                    buttonText.text = q.options[i];
                }

                int index = i;
                answerButtons[i].onClick.RemoveAllListeners();
                answerButtons[i].onClick.AddListener(() => OnAnswerSelected(index));
            }
            else
            {
                answerButtons[i].gameObject.SetActive(false);
            }
        }
    }

    void OnAnswerSelected(int index)
    {
        bool correct = index == questions[currentQuestionIndex].correctAnswerIndex;
        feedbackText.text = correct ? "Correct!" : "Try Again!";

        if (correct)
        {
            score++;
            scoreText.text = "Score: " + score;
        }

        currentQuestionIndex++;
        Invoke("ShowQuestion", 1.5f);
    }

    public void OnFinishButtonClicked()
    {
        GameData.finalScore = score; // Store score globally
        SceneManager.LoadScene("True_False"); // Replace with your next scene name
    }
}

[System.Serializable]
public class Question
{
    public string questionText;
    public string[] options;
    public int correctAnswerIndex;
    public QuestionType questionType;
}

public enum QuestionType
{
    MultipleChoice,
    TrueFalse
}
