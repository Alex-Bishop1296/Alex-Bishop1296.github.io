# Return to?
[Home](../index.md)
[CS460 Assignments](cls-cs460.md)
[Code Repo](Alex-Bishop1296.github.io)

# Link to finished project:

### [Homepage](Alex-Bishop1296.github.io/html/home.html)

# 1.[Setup]
 My first task was to install the command line version of the Git version control system. I did this simply enough by following the link from Git’s website and downloading the installer. The process only took a couple clicks through the right checkboxes in the installer and then I was ready to use Git. I then needed to setup a repository for all of my work. For now, I created a CS460 folder which will house my repository.

# 2.[Setup] 
My next step was to setup a Github or Bitbucket account. As I had already made a Github account in the past I decided to go with the one I already made. Next, I needed to set up my repository with Github. Before I did that, I know I needed a little bit of setup, so from the command line I set my username and email for my git commits with the following commands:
```
git config --global user.name “Alex-Bishop1296”

git config --global user.email “alex.bishop1296@gmail.com”
```
From there I made an online Github repository and cloned that into my CS460 folder using the clone command in command line. To finally make sure everything was working as intended, I made a folder for homework 1 and put a test file inside it. From there, I did the following
```
git add HW1

git commit -m “Added Homework 1 folder with test file inside it”

git push origin master
```
These commands successfully committed my changes to the master branch and uploaded them to the online repository. With this done, I was finished with setup and ready to move onto the next step.

# 3.[Content/Coding]
I started by getting my feet wet in HTML and getting the basic template for an html page working with a title,  and simple paragraph, like so:
```html
<!DOCTYPE html>
<html lang=”en”>
<head>
<meta charset="utf-8">
<title>Website</title>
</head>
<body>

<h1>Hello world</h1>
<p>this is some text</p>
</body>
</html>
```
This contained the encoding, language, character set, and some basic text. I had already done a bit of html years ago so it was more review than anything else. From I played with some other things like multiple headers, bold and italic font, and other basic modifiers until I was comfortable with the syntax again. Next, I moved onto learning about Bootstrap and how it worked, knowing I would need it for the basis of my layout.

After some research, I learned two things, 1) Bootstrap could be implemented just like any other CSS file and 2) I would need to download the files for it to avoid using an absolute path as per the assignments requirements. As such, I made a basic CSS file for my needs and downloaded the bootstrap css files and placed them in my project folder to reference. After some minor additions to the CSS file like background color, I went back into my HTML file, now decided to be for my homepage, and added the following code to link it to my stylesheets:
```html
<!-- This meta tag here is for Bootstrap v4, allowing proper rendering and touch zooming -->
<!-- The width sets the view width to device width and the scale allows initial zoom when page is first loaded -->
<meta name="viewport" content="width=device-width, initial-scale=1">
<!-- Bootstrap CSS -->
<link rel="stylesheet" href="../css/bootstrap.min.css">
<!-- Master CSS file-->
<link rel="stylesheet" href="../css/master.css">
```
As explained in the included comments, the first meta line is settings for the scaling of the screen via bootstrap’s features, and the other two links are for linking to the stylesheets for bootstrap and the website’s master CSS file. I made sure to store them in a folder one directory up to avoid using absolute paths.

From here I decided to implement a navigation bar to give the website both linked pages and multiple pages. This took a number of attempts, as I was originally looking at resources for bootstrap that were for version 3 rather than version 4. After some learning, my navigation code for home looked like the following:
```html
<!-- Top Navigation Bar -->
        <nav class="navbar navbar-expand-sm bg-dark navbar-dark">
            <ul class="navbar-nav">
<!-- Nav-Item active sets highlight, nat-item inactive disallowed link use-->
                <li class="nav-item active">
                    <a class="nav-link" href="home.html">Home</a>
                </li>
                <li class="nav-item">
                   <a class="nav-link" href="gallery.html">Gallery</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="blog.html">Blog</a>
                </li>
            </ul>
        </nav>
```
Breaking this code down a little bit, I create a nav class object that is a navbar with a dark background and text as well as a small expanded size. Then, I assign the navbar object to an ul or unordered list. Finally, I create list nav-item class items for each link, setting the current page (home.html in this case) to active. This allows me to create a nav bar like so:



Next, after linking the nav buttons and copying the layout onto new pages for gallery and blog, I began to look at the individual requirements of the webpages. One of them was to use bootstrap to create a single column and multi-column pages. Starting with single column page I created a container to test on the Home page and Blog page. The start of the code for this looked as follows:
```html
<div class="container text-center">
            <div class="col-lg-12">
		 …
		 </div>
</div>
```
Not much to say here, simply the division item contains the class for a container with the center text argument, and within that container I use the bootstrap grid to create a large column with a size of 12. Within this sub division, I can create text and other elements as need and they will align to the center of the page (scaling of course). From inside this division I just placed any text and images I want as the actual content of the page.
The next requirement I went to touch was the multicolumn page requirement. I decided to use this for my gallery. In a similar notion to the single column, I used the following code:
```html
<div class="row">
	<div class="col-sm-6">
…
</div>
    	<div class="col-sm-6">
…
</div>
</div>
```
Here, instead of using a regular container I use a row, then split it into two equal “col-sm-6”, or small 6 unit columns, to form the two columns. From here I started loading the images into the columns and descriptions of them. I noticed the images were not centered in the columns so looked up how to use bootstrap to adjust them. Thus, I used this class code for images:
```html
<img class="img-fluid mx-auto d-block" src= … >
```
mx-auto set the margins to automatic and d-block centered the images inside the col-sm-6 division. Thus, my images looked good, then I just adjusted the padding and other settings in my CSS file.

Now that I had my two types of pages, I still needed another type of list other than the one in navigation and a table. I decided to go with the list first. Some quick research on lists showed that I could either do an ol or order list, or a dl or description list. I decided to go with a description list. I implemented it in my opening page paragraph as follows:
```html
<dl>
                <dt>item</dt>
                <dd>subtext</dd>
                <dt>next item</dt>
                ...
</dl>
```
Simple as it gets, <dl> denotes the start of the list, and each <dt> is the item and <dd> is the item description. I used this in the simplest sense, not much more editing than that. We get this as a result:


Simple, nice and easy. Finally I had to hit the requirement for a table. I decided to use the table as some contacts, as well as use a bootstrap table. With bootstrap, the table took a similar approach to the many containers and rows that I have already made. After some minor research, the code looks like this:
```html
<div class="table-responsive-sm">
                <table class="table table-borderless">
                    <thead>
                        <tr>
                            <th>Firstname</th>
                            <th>Lastname</th>
                            <th>Twitter</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>John</td>
                            <td>Doe</td>
                            <td>@UnavoidableStockGuy</td>
                        </tr>
				…
               </table>
</div>
```
Breaking this down, the first div class makes a small responsive table or “table-responsive-sm”. This allows us to scale the time smartly to screen size to a limit (once it hits the pixel limit it adds a scrollbar). Then I added the table class “table table-borderless”, this allowing the table to have no lines or separations (personal preference). Next we have the thread, or title columns, this is used for setting how many columns each row has and what they are. Followed by this is the tbody which contains each row. With these foundations, making a table was easy as could be, and looked a little like this:


With these requirements hit, I started making lots of edits for the looks and content of the website. As these show on the website and don’t need the additional detail, I can move onto the next step of the project:

# 4.[Test]
This step was pretty simple, making a clone of the repository and checking if all the website works from it. I made a remote test folder, navigated to it in git bash, and did the following:
```
git clone https://github.com/Alex-Bishop1296/CS460projects.git
```
This cloned the repository’s contents into my directory. Next I opened up the html file and everything worked exactly as intended. Now I could jump to the next step.

# 5.[Setup]
