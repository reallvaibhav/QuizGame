// using UnityEngine;

// public class QuizFlowController : MonoBehaviour
// {
//     [Header("Quiz GameObjects")]
//     public GameObject mcqManager;
//     public GameObject trueFalseManager;

//     private MCQManager mcqScript;
//     private TrueFalseManager tfScript;

//     void Start()
//     {
//         // Get components
//         mcqScript = mcqManager.GetComponent<MCQManager>();
//         tfScript = trueFalseManager.GetComponent<TrueFalseManager>();

//         // Subscribe to MCQ completion event
//         mcqScript.OnQuizCompleted += HandleMCQCompleted;

//         // Start with MCQ quiz
//         mcqManager.SetActive(true);
//         trueFalseManager.SetActive(false);
//     }

//     void HandleMCQCompleted()
//     {
//         Debug.Log("MCQ quiz completed. Starting True/False quiz.");

//         // Switch UI
//         mcqManager.SetActive(false);
//         trueFalseManager.SetActive(true);

//         // Start True/False quiz
//         tfScript.StartQuiz();
//     }
// }
