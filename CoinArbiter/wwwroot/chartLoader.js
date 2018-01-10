'use strict';

//Load Google Charts
google.charts.load('current', { 'packages': ['corechart'] });

//Array that stores the session's 10 latest live prices (Size is limited by the "CullLivePrices" function)
var coinList = [];

//Kickoff charting logic after Google Charts finishes loading
google.charts.setOnLoadCallback(DrawHourly);
google.charts.setOnLoadCallback(DrawLive);


//Limits the size of "coinlist[]" to 10 indexes by culling the oldest data from the array
function CullLivePrices(priceArray) {
    if (priceArray.length > 10) {
        priceArray.splice(0, 1);
    }
}

//Get Hourly price history for Bitcoin and draw with Google Charts
function DrawHourly() {
    $(function () {
        console.log('Refresh')
        $.ajax({
            type: 'GET',
            //Queries the below action on the "HomeController" which in turn queries CryptoCompare's API for data
            url: '/Home/GetHistoryChartJSONAsync',

            //If Query is successful, pass in json array for charting.
            success: function (priceList) {
                var Data = priceList;
                var data = new google.visualization.DataTable();

                //Add appropriate data columns to chart
                data.addColumn('string', 'Currency');
                data.addColumn('number', 'USD');

                //Loop over list data, adding a row with data for each index
                for (var i = 0; i < Data.length; i++) {
                    data.addRow(["", Data[i].usd]);
                }

                // Instantiate chart at specified div
                var chart = new google.visualization.ColumnChart(document.getElementById('chartdiv'));

                //Render the chart
                chart.draw(data,
                    {
                        title: "Bitcoin price change per hour",
                        position: "top",
                        fontsize: "14px",
                    });

            },
            error: function () {
                alert("Unable to load data!");
            }
        });
    })

    //Recursively call this function every hourish to update the chart
    setTimeout(DrawHourly, 3800000);
}

//Nearly Identical to the above, same comments apply except where specified.
function DrawLive() {
    $(function () {
        $.ajax({
            type: 'GET',
            url: '/Home/GetLivePriceAsync',
            success: function (newCoin) {

                //Push the "newCoin" into "coinList" so it can be rendered with the last 10 prices
                coinList.push(newCoin);

                //Limits the size of "coinList" to 10 indexes by getting rid of the oldest price
                CullLivePrices(coinList);
                var data = new google.visualization.DataTable();

                data.addColumn('string', 'Currency');
                data.addColumn('number', 'USD');

                for (var i = 0; i < coinList.length; i++) {
                    data.addRow(["", coinList[i].usd]);
                }

                var chart = new google.visualization.ColumnChart(document.getElementById('live-chartdiv'));

                chart.draw(data,
                    {
                        title: "Bitcoin live price",
                        position: "top",
                        fontsize: "14px",
                    });

            },
            error: function () {
                alert("Unable to load data!");
            }
        });
    })

    //Recursively call this function every 11 seconds to update chart with new live data.
    setTimeout(DrawLive, 11000);
}