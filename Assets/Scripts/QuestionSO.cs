// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// [CreateAssetMenu(menuName = "Quiz Question SO", fileName = "New Question")]
// public class QuestionSO : ScriptableObject
// {
//     public QuestionSO (string questionTitle, string answer1, string answer2, string answer3, string answer4)
//     {
//         this.QuestionTitle = questionTitle; 
//         this.AnswerOne = answer1;
//         this.AnswerTwo = answer2; 
//         this.AnswerThree = answer3;
//         this.AnswerFour = answer4;
//     }

//     [TextArea(2,6)]
//     [SerializeField] string QuestionTitle;
//     [TextArea(2,3)]
//     [SerializeField] string AnswerOne;
//     [TextArea(2,3)]
//     [SerializeField] string AnswerTwo;
//     [TextArea(2,3)]
//     [SerializeField] string AnswerThree;
//     [TextArea(2,3)]
//     [SerializeField] string AnswerFour;

//     public string GetQuestionTitle()
//     {
//         return QuestionTitle;
//     }
//     public string GetAnswerOne()
//     {
//         return AnswerOne;
//     }
//     public string GetAnswerTwo()
//     {
//         return AnswerTwo;
//     }
//     public string GetAnswerThree()
//     {
//         return AnswerThree;
//     }
//     public string GetAnswerFour()
//     {
//         return AnswerFour;
//     }

// }
