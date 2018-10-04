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
},
{
    prompt: "What bone is this?",
    image: "../img/003.jpg",
    choices: ["Stapes", "Tibia", "Renais", "Olmves"],
    correctAnswer: 0
}
]

//Items we need to keep track of for this game to work
var playerName;
var currentQuestion = 0;
var correctQuestions = 0;
var quizOver = false;

// This will serve as my button controller 
$(document).ready(function () {
    // Make submission button invisable before load
    $("#submission").toggle();

    //Button functions
    $(this).find("#progression").on("click", checkStart);
    $(this).find("#submission").on("click", checkSubmit);
});

// This will check if the quiz can be started and will start it if it can
function checkStart() {
    //Check if the name field entered by the player is valid
    if (document.getElementById("name").value == "" || document.getElementById("name").value == " " || document.getElementById("name").value == "Enter your name!") {
        console.log("Invalid name error detected");
        alert("That name won't do! Fix it ya BONE head!");
    } else {
        // Start the Quiz and put up first question
        console.log("Sequencing quiz startup");
        startQuiz();
        nextQuestion();
    }
}

// This function will check each value submission, incrementing score if possible
function checkSubmit() {
    // Assign current list value     
    var value = $("input[type='radio']:checked").val();

    //Check if value was valid, skip if not
    if (value == undefined) {
        console.log("Checking Value");
        alert("You numb SKULL! You need to select a value!");
    }
    else {
        //If question was correct, increment
        if (value = questions[currentQuestion].correctAnswer) {
            console.log("Correct answer");
            correctQuestions++;
        }
        //If quiz questions remain, continue to next questions
        if (currentQuestion < questions.length - 1) {
            console.log("Starting next question");
            currentQuestion++;
            nextQuestion();
        }
        //End Quiz
        else {
            console.log("ending quiz now");
            $("#submission").toggle();
            endQuiz();
        }

    }
}

// This code must be executed each time the start button is pressed
function startQuiz() {
    // Log to the console that the quiz is being started
    console.log("Starting Quiz");

    // Store player name
    playerName = document.getElementById("name").value;

    // Turn off the welcome message
    $("#welcome").toggle();

    // Change button prompt
    $("#progression").toggle();
    $("#submission").toggle();
}

//End quiz an give named player their score
function endQuiz() {
    console.log("Reached end function");
    //Empty the staging area
    $("#question_prompt").empty();
    $("#question_image").empty();
    $("#question_choices").find("li").remove();

    //Display end message
    $("#question_prompt").text(playerName + " Scored: " + correctQuestions + " points out of " + questions.length);
}

// Code for generating next question
function nextQuestion() {
    //Assign the questions based on current question
    var prompt = questions[currentQuestion].prompt;
    var image = questions[currentQuestion].image;
    var choice;

    // Insert current question into HTML staging area
    $("#question_prompt").text(prompt);
    $("#question_image").html('<img src="' + image + '" width="300" height="300">');

    // Remove all current <li> elements (if any)
    $("#question_choices").find("li").remove();

    // For each choice in the current questions choices, insert into list
    for (i = 0; i < questions[currentQuestion].choices.length; i++) {
        choice = questions[currentQuestion].choices[i];
        $('<li><input type="radio" value=' + i + ' name="dynradio" />' + choice + '</li>').appendTo("#question_choices");
    }
}