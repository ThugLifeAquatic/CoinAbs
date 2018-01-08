'use strict';

google.charts.load('current', { 'packages': ['corechart'] });

//Call function after Google Chart is loaded, it is required, otherwise you may get error
google.charts.setOnLoadCallback(DrawonLoad);

function DrawonLoad() {
    $(function () {
        console.log('Refresh')
        $.ajax({
            type: 'GET',
            url: '/Home/GetHistoryChartJSONAsync',
            success: function (priceList) {
                // Callback that creates and populates a data table,
                // instantiates the pie chart, passes in the data and
                // draws it.

                //get priceList
                var Data = priceList;
                var data = new google.visualization.DataTable();

                data.addColumn('string', 'Currency');
                data.addColumn('number', 'USD');

                //Loop through each list data
                for (var i = 0; i < Data.length; i++) {
                    data.addRow(["", Data[i].usd]);
                }

                // Instantiate and draw our chart, passing in some options
                var chart = new google.visualization.ColumnChart(document.getElementById('chartdiv'));

                //Draw pie chart command with data and chart options
                chart.draw(data,
                    {
                        title: "Bitcoin price change per hour",
                        position: "top",
                        fontsize: "14px",
                    });

            },
            error: function () {
                alert("Error loading data! Please try again.");
            }
        });
    })
    setTimeout(DrawonLoad, 3800000);
}