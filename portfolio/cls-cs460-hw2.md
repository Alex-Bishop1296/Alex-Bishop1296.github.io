# CS460 Homework 2

# Return to?
### [Code Repo](https://github.com/Alex-Bishop1296/Alex-Bishop1296.github.io) 
### [Home](../index.md) 
### [CS460 Assignments](cls-cs460.md) 

# Link to finished project:

### [Homepage](http://alex-bishop1296.github.io/HW2/html/index.html)

# Notes

# 1.[Setup] 
My first task was to make a new folder in my git repository for my assignment. I got into where my repo is stored locally and place a folder for homework 2 as HW2. From here, I need to branch off and start the assignment. To do this, I run the command:
```
git checkout -b homework-two
```
This is just shorthand for the branch and checkout commands. Thus, I made a new branch from my current one in master and started working on it. From here I make subfolders for html, css, js, and img files. Then in those files I make a index.html, styles.css, and *.js file to work on later.

# 2.[Planning and Design]
After a period of deliberation, I decided to make a quiz as my project. To make it more holloween themed I would quiz on names and triva of bones in the human body.

# 3.[Planning and Design]
Next up, I plannout the layout of my website. I write up some simple instructions that look like so:

![Simple Layout Drawing](example/hw2Draft.jpg)

I decided to have a single container that took up a container with a 8 grid size md setup, spaced out by 2 grid spaces (refering to bootstrap grid). On the title screen, we have a H2 element with some text under it explaining the quiz, then a start button with shadow. I also included a text field for the player to input their name. In the quiz page, I have a header, Image that would be present of invisable depending on the questions, a ul of radio buttons for the user answer, and finally the next question button. I decided to go with a black and orange coloring scheme for all cards in on the halloween theme.

# 4.[Coding]
Quite a few things to go over here so I will try to be brief. First is the requirement for response to user output. I have three types of user input: buttons, radio forms, and an input field. Due various issues working with asyncronous code, I had to think of a good workaround for setting up my buttons as my original plan of one progresion button caused too many issues. Thus, in html, my buttons look like this:
```html
 <button id="progression">Start Quiz</button>
 <button id="submission">Submit</button>
 <button id="restarter">Play Again?</button>
```
I actually have a button for each type of function that needs to be executed, then I hide them based on where in the quiz the user is (ie what button they should have access to). I use a simple toggle to do this, like right when I intialize the page as so:
```js
    // Make submission and restarter button invisable before load
    $("#submission").toggle();
    $("#restarter").toggle();
    // Allow buttons to be shown by fade in (prevents blinking effect)
    $('div.hidden').fadeIn(1000).removeClass('hidden');
```
You might notice that this would normally cause all buttons to flicker when the page is loading in, a visable tick. To prevent this, I include a "hidden" class on my entire container like this:
``` html
<div class="container-fluid text-center hidden">
```
This allowed me to run a .fadeIn() (seen in the toggle block of code) that would cause the entire page to load in, then fade in, hiding any graphical issues. A cool looking work around if I do say so myself. Next up, we can talk about my radio form.

The radio form inputs filled my requirements of having a list with multiple child elements, having multiple form elements (radio elements and input fields), and adding new list type elements to the page. Again, these would act as the answers a user could input for given questions. I started with some html that looks like this:
```html
<form>
    <ul id="question_choices"></ul>
</form>
```
This is simply a form element wrapping a unordered list element with the id "question_choices" assigned to it.