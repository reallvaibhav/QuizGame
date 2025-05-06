using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[System.Serializable]
public class WordQuestion
{
    public string answer;
    public string hint;
    public Sprite image;  // The image for each question
}

public class WordShuffleGame : MonoBehaviour
{
    public TextMeshProUGUI shuffledWordText;
    public TMP_InputField inputField;
    public TextMeshProUGUI feedbackText;
    public TextMeshProUGUI scoreText;
    public GameObject finishButton;

    public Image questionImage;  // Image component to display the question image
    public TextMeshProUGUI hintText;  // To show the hint
    public List<WordQuestion> questions = new List<WordQuestion>();  // Dynamically populated list of questions

    private string currentWord;
    private int questionCount = 0;
    private const int maxQuestions = 10;

    void Start()
    {
        LoadQuestionsFromSprites();  // Dynamically load questions
        finishButton.SetActive(false);
        feedbackText.text = "";  // Hide feedback at the start
        scoreText.text = "Score: " + GameData.finalScore;  // Ensure GameData is properly defined
        ShowQuestion(0);  // Show the first question
    }

    void LoadQuestionsFromSprites()
    {
        // Load all sprites from the "Resources/Questions" folder
        Sprite[] sprites = Resources.LoadAll<Sprite>("Questions");

        foreach (Sprite sprite in sprites)
        {
            // Create a new WordQuestion object for each sprite
            WordQuestion question = new WordQuestion
            {
                answer = sprite.name.ToUpper(),  // Use the sprite name as the answer
                hint = "Guess the word!",  // Default hint (you can customize this)
                image = sprite
            };

            questions.Add(question);
        }

        // Shuffle the questions list to randomize the order
        ShuffleQuestions();
    }

    void ShuffleQuestions()
    {
        for (int i = 0; i < questions.Count; i++)
        {
            int randomIndex = Random.Range(i, questions.Count);
            WordQuestion temp = questions[i];
            questions[i] = questions[randomIndex];
            questions[randomIndex] = temp;
        }
    }

    public void CheckAnswer()
    {
        if (inputField.text.ToUpper() == currentWord)
        {
            GameData.finalScore++;
            feedbackText.text = "Correct! Well done!";
            feedbackText.color = Color.green;
            scoreText.text = "Score: " + GameData.finalScore;
        }
        else
        {
            feedbackText.text = "Incorrect! Try again.";
            feedbackText.color = Color.red;
        }

        StartCoroutine(HideFeedbackAfterDelay(2f));  // Hide feedback after 2 seconds
    }

    private IEnumerator HideFeedbackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        feedbackText.text = "";  // Clear feedback text

        // Show the next question if there are more to ask
        if (questionCount + 1 < Mathf.Min(maxQuestions, questions.Count))
        {
            ShowQuestion(++questionCount);
        }
        else
        {
            shuffledWordText.text = "Quiz Complete!";
            questionImage.enabled = false;
            inputField.gameObject.SetActive(false);
            hintText.text = "";
            finishButton.SetActive(true);
        }
    }

    void ShowQuestion(int index)
    {
        inputField.text = "";  // Clear the input field
        WordQuestion q = questions[index];  // Get the current question object
        currentWord = q.answer.ToUpper();  // Set the current word (answer)
        shuffledWordText.text = "Shuffled Word: " + ShuffleWord(currentWord);  // Display the shuffled word

        // Show image if it exists for this question
        if (q.image != null)
        {
            questionImage.sprite = q.image;  // Set the image for the current question
            questionImage.enabled = true;  // Make sure the image is visible
        }
        else
        {
            questionImage.enabled = false;  // Hide the image if no image is assigned
        }

        // Display the hint for the current question
        hintText.text = q.hint;
    }

    // Method to shuffle the word (for the word scramble effect)
    string ShuffleWord(string word)
    {
        char[] charArray = word.ToCharArray();
        for (int i = 0; i < charArray.Length; i++)
        {
            int randomIndex = Random.Range(i, charArray.Length);
            char temp = charArray[i];
            charArray[i] = charArray[randomIndex];
            charArray[randomIndex] = temp;
        }
        return new string(charArray);
    }

    public void FinishGame()
    {
        Debug.Log("Game Finished with score: " + GameData.finalScore);
        SceneManager.LoadScene("FinalScene"); // Replace with your actual final scene name
    }
}

