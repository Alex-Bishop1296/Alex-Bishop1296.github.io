$(document).ready(function () {

    // Get the current bid price from html
    var currentBid = parseInt($("#price").html());
    // Set url for relocating user
    var url = window.location.href;
    // Get id from end of url
    var id = url.substr(url.lastIndexOf('/') + 1);
    // Set source location
    var source = "/Items/Update/ " + id;

    // send the ajax call to the controller
    var ajax_call = function () {
        //Base ajax call
        $.ajax({
            type: "GET",
            dataType: "json",
            url: source,
            success: displayData,
            error: errorOnAjax
        });
    };

    // Display the data that we've retrieved
    function displayData(recent) {

        //Debug messages
        console.log("database price: " + recent.price);
        console.log("current price: " + currentBid)

        //If new bid is higher, add the new table element to the top of the table
        if (recent.price > currentBid) {
            currentBid = recent.price;
            $("#inner").prepend("<tr><td>" + recent.name + "</td>" + "<td>" + recent.price + "</td></tr>");
        }
    }
    // something went wrong
    function errorOnAjax() {
        console.log("error");
    }


    // Call the function repeatedly
    var interval = 1000 * 5;
    window.setInterval(ajax_call, interval);
});