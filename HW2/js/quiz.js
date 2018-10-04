// A variable to store all quiz images, correct answers, and choices
var questions = [{
    prompt: "What bone is this?",
    image: "../img/001.jpg",
    choices: ["Fibula", "Femur", "Tibia", "Tarsus"],
    correctAnswer: 1
},
{
    prompt: "What bone is this?",
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
                nextQuestion();
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

// Code for generating next question
function nextQuestion() {
    //Assign the questions based on current question
    var prompt = questions[currentQuestion].prompt;
    var image = questions[currentQuestion].image;
    var choice;

    $("#question_prompt").text(prompt);
    $("#question_image").html('<img src="' + image + '" width="300" height="300">');
   
    // For each choice in the current questions choices
    for (i = 0; i < questions[currentQuestion].choices.length; i++) {
        choice = questions[currentQuestion].choices[i];
        $('<li><input type="radio" value=' + i + ' name="dynradio" />' + choice + '</li>').appendTo("#question_choices");
    }
}