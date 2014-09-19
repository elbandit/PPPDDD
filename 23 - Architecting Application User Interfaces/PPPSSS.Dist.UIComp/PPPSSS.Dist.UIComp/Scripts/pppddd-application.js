// when the page loads
$(function() {

    // query for holidays that simulates a user search
    $.getJSON("/holidays", function (json) {
        $.each(json, function (index, holiday) {
            $('#holidays').append(createHolidayView(holiday));
        });
    });

    // query for promoted holidays
    $.getJSON("/promotions", function (json) {
        $.each(json, function (index, holiday) {
            $('#promotions').append(createHolidayView(holiday));
        });
    });

    // query for user-specific recommendations
    $.getJSON("/recommendations", function (json) {
        $.each(json, function (index, holiday) {
            $('#recommendations').append(createHolidayView(holiday));
        });
    });
});

function createHolidayView(holiday) {
    return '<div> ' +
           '<img src="' + holiday.ImgUrl + '" height="75" style="float: left;" /> ' +
           '<h4>' + holiday.Title + '</h4>' +
           '$' + holiday.Price + 'pp' +
           '</div>' +
           '<br />';
}
/*
* Jquery used for demonstration purposes. Other frameworks
* may be a better choice including Angular.js, Knockout.js etc
*/