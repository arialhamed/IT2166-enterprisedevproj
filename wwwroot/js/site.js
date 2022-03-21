// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

// function is extracted and modified from https://www.w3schools.com/howto/howto_js_sort_table.asp
function ISMainSortTable(n, dir="asc") {

    // n is function given from w3schools
    var table, rows, switching, i, x, y, shouldSwitch, dir, switchcount = 0;
    table = document.getElementById("ISMainTable");
    switching = true;

    // Set the sorting direction to ascending:
    //dir = "asc";

    /* Make a loop that will continue until
    no switching has been done: */
    while (switching) {

        // Start by saying: no switching is done:
        switching = false;
        rows = table.rows;

        /* Loop through all table rows (except the
        first, which contains table headers): */
        for (i = 1; i < (rows.length - 1); i++) {

            // Start by saying there should be no switching:
            shouldSwitch = false;

            /* Get the two elements you want to compare,
            one from current row and one from the next: */
            x = rows[i].getElementsByTagName("TD")[n];
            y = rows[i + 1].getElementsByTagName("TD")[n];

            /* Check if the two rows should switch place,
            based on the direction, asc or desc: */
            if (dir == "asc") {
                if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {

                    // If so, mark as a switch and break the loop:
                    shouldSwitch = true;
                    break;
                }
            } else if (dir == "desc") {
                if (x.innerHTML.toLowerCase() < y.innerHTML.toLowerCase()) {

                    // If so, mark as a switch and break the loop:
                    shouldSwitch = true;
                    break;
                }
            }
        }
        if (shouldSwitch) {

            /* If a switch has been marked, make the switch
            and mark that a switch has been done: */
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;

            // Each time a switch is done, increase this count by 1:
            switchcount++;
        } else {

            /* If no switching has been done AND the direction is "asc",
            set the direction to "desc" and run the while loop again. */
            if (switchcount == 0 && dir == "asc") {
                dir = "desc";
                switching = true;
            }
        }
    }
}

/*function history(i = 0) {
    var historyArray = [];
    if (window.sessionStorage.getItem("historyArray") === null) {
        window.sessionStorage.setItem("historyArray", historyArray);
    }
    historyArray = window.sessionStorage.getItem("historyArray");
    if (i == 0) {
        historyArray.push(window.location.href);
        window.sessionStorage.setItem("historyArray", historyArray);
    }
    if (i == -1) {
        *//*var outLink = historyArray[historyArray.length -1];
        historyArray.
        return outLink*//*
        return historyArray.pop();
    }
}
*/

function ISMainSearchTable() {
    // Declare variables
    // get input, this function runs onkeyup
    input = document.getElementById('ISMainSearchBox');
    // filter input to uppercase
    filter = input.value.toUpperCase();
    // get tbody (table itself contains thead and tbody)
    table = document.getElementById('ISMainContent')
    // get array of rows
    rows = table.getElementsByTagName('tr')

    //displayed_count = 0 // if this part is for if there is 0 results in a search

    // go through each row of the rows
    for (y = 0; y < rows.length; y++) {
        // get list details of from each row
        details = rows[y].getElementsByTagName('td')
        // go through each detail of list of details of each row of the rows
        for (x = 0; x < details.length; x++) {
            // the detail
            span = details[x]
            // text value of detail
            txtValue = span.textContent || span.innerText;

            // self-explanatory here
            if ((txtValue.toUpperCase().indexOf(filter) > -1)) {
                rows[y].style.display = "";
                break;
            } else {
                rows[y].style.display = "none";
            }
        }
    }
}