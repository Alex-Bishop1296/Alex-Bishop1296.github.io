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

// This will serve as my button controller 
$(document).ready(function () {
    // Make submission and restarter button invisable before load
    $("#submission").toggle();
    $("#restarter").toggle();
    // Allow buttons to be shown by fade in (prevents blinking effect)
    $('div.hidden').fadeIn(1000).removeClass('hidden');

    //Button functions
    $(this).find("#progression").on("click", checkStart);
    $(this).find("#submission").on("click", checkSubmit);
    //Testing other syntax for buttons
    //Brings user back to start page
    $("#restarter").click(function() {
        window.location = "../html/index.html";
    });
});

// This will check if the quiz can be started and will start it if it can
function checkStart() {
    //Check if the name field entered by the player is valid
    if (document.getElementById("name").value == "" || document.getElementById("name").value == " " || document.getElementById("name").value == "Enter your name!") {
        // Debug message
        console.log("Invalid name error detected");
        // Alert user
        alert("That name won't do! Fix it ya BONE-head!");
    } else {
        // Start the Quiz and put up first question
        console.log("Sequencing quiz startup");
        startQuiz();
        nextQuestion();
    }
}

// This function will check each value submission, incrementing score if possible
// Most of the post-start Quiz logic, including the quit and scoring, happens here
function checkSubmit() {
    // Assign current user selected value     
    var value = $("input[type='radio']:checked").val();

    //Check if value was valid, skip if not
    if (value == undefined) {
        // Debug message
        console.log("Checking Value - Invalid");
        // Alert user
        alert("You numb-SKULL! You need to select a value!");
    }
    else {
        // Debug message
        console.log("Player answered " + value + ", The answer was " + questions[currentQuestion].correctAnswer);

        //If question was correct, increment
        if (value == questions[currentQuestion].correctAnswer) {
            // Debug message
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
            // Debug message
            console.log("ending quiz now");
            // Change button prompts
            // Enable The return to page home as a restart 
            $("#restarter").toggle();
            $("#submission").toggle();
            // End Quiz
            endQuiz();  
        }

    }
}

// Start the quiz
function startQuiz() {
    // Debug message
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
    //Debug message
    console.log("Reached end function");

    //Empty the staging area
    $("#question_prompt").empty();
    $("#question_image").empty();
    $("#question_choices").find("li").remove();

    // Display end message
    $("#question_prompt").text(playerName + " Scored: " + correctQuestions + " points out of " + questions.length);

    // Clear Values
    playerName.empty();
    currentQuestion = 0;
    correctQuestions = 0;
}

// Code for generating next question
function nextQuestion() {
    //Assign the questions based on current question
    var prompt = questions[currentQuestion].prompt;
    var image = questions[currentQuestion].image;
    var choice;

    // Insert current question into HTML staging area
    $("#question_prompt").text(prompt);
    
    // Insert image (if any) into HTML staging area
    if (image != "") {
    $("#question_image").html('<img src="' + image + '" width="300" height="300">');
    }

    // Remove all current <li> elements (if any)
    $("#question_choices").find("li").remove();

    // For each choice in the current questions choices, insert into list
    for (i = 0; i < questions[currentQuestion].choices.length; i++) {
        choice = questions[currentQuestion].choices[i];
        // Append item to the radio list
        $('<label class="radio-inline"><li><input type="radio" value=' + i + ' name="dynradio" />' + choice + '</li></label>').appendTo("#question_choices");
    }
}