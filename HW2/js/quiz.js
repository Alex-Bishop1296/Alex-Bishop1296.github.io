// A variable to store all quiz images, correct answers, and choices
var questions = [{
    image: "../img/001.jpg",
    choices: ["Fibula", "Femur", "Tibia", "Tarsus"],
    correctAnswer: 1
},
{
    image: "../img/002.jpg",
    choices: ["Vomer", "Ulna", "Humerus", "Femur"],
    correctAnswer: 2
}
]

var playerName = "";
var currentQuestion = 0;
var correctQuestions = 0;
var quizOver = false;