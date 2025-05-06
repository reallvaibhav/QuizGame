// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;
// using System.Collections.Generic;

// public class MCQManager : MonoBehaviour
// {
//     [Header("UI Elements")]
//     public TMP_Text questionText;
//     public TMP_Text[] optionTexts;
//     public Button[] optionButtons;
//     public TMP_Text feedbackText;
//     public TMP_Text scoreText;

//     [Header("MCQ Questions")]
//     public List<Question> mcqQuestions;

//     private int currentIndex = 0;
//     private int score = 0;

//     public delegate void QuizCompleted();
//     public event QuizCompleted OnQuizCompleted;

//     void Start()
//     {
//         feedbackText.text = "";
//         scoreText.text = "Score: 0";
//         ShowQuestion();
//     }

//     void ShowQuestion()
//     {
//         if (currentIndex >= mcqQuestions.Count)
//         {
//             feedbackText.text = "Quiz Complete!";
//             OnQuizCompleted?.Invoke();  // Notify QuizFlowController
//             return;
//         }

//         Question q = mcqQuestions[currentIndex];
//         questionText.text = q.questionText;
//         feedbackText.text = "";

//         for (int i = 0; i < optionButtons.Length; i++)
//         {
//             if (i < q.options.Length)
//             {
//                 optionButtons[i].gameObject.SetActive(true);
//                 optionTexts[i].text = q.options[i];
//                 int index = i;
//                 optionButtons[i].onClick.RemoveAllListeners();
//                 optionButtons[i].onClick.AddListener(() => HandleAnswer(index));
//             }
//             else
//             {
//                 optionButtons[i].gameObject.SetActive(false);
//             }
//         }
//     }

//     void HandleAnswer(int selected)
//     {
//         Question q = mcqQuestions[currentIndex];
//         bool isCorrect = selected == q.correctAnswerIndex;
//         feedbackText.text = q.feedback;

//         if (isCorrect)
//         {
//             score++;
//             scoreText.text = "Score: " + score;
//         }

//         currentIndex++;
//         Invoke(nameof(ShowQuestion), 1.5f);
//     }
// }
