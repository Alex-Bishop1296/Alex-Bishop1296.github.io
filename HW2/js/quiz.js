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

//Items we need to keep track of for this game to work
var playerName;
var currentQuestion = 0;
var correctQuestions = 0;
var quizOver = false;

//This will serve as my game controller 
$(document).ready(function () {
    $(this).find("#progression").on("click", function () {
        //Check if the name field entered by the player is valid
        if (document.getElementById("name").value == "" || document.getElementById("name").value == " " || document.getElementById("name").value == "Enter your name!") {
            alert("That name won't do! Fix it ya BONE head!");
        } else {
            startQuiz();
        }
    });

});

// This code must be executed each time the start button is pressed
function startQuiz() {
    // Log to the console that the quiz is being started
    console.log("Starting Quiz");

    // Store player name
    playerName = document.getElementById("name").value;

    // Turn off the welcome message
    $("#welcome").slideToggle();

    // Change button prompt
    $("#progression").html("Submit");
}