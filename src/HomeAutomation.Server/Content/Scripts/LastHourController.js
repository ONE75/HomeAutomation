// Load the Visualization API and the piechart package.
google.load('visualization', '1', { 'packages': ['corechart'] });

// Set a callback to run when the Google Visualization API is loaded.
google.setOnLoadCallback(drawChart);

function drawChart() {
    var jsonData = $.ajax({
        url: "/ChartDataForLastHour",
        dataType: "json",
        async: false
    }).responseText;

    // Create our data table out of JSON data loaded from server.
    var data = new google.visualization.DataTable(jsonData);

    //var jason = data.toJSON();
    //$("#jasonData").text(jason);
    // Create and draw the visualization.
    new google.visualization.LineChart(document.getElementById('visualization')).
    draw(data, { curveType: "function", width: 1600, height: 400, vAxis: { maxValue: 30, minValue: 20 } });
}
