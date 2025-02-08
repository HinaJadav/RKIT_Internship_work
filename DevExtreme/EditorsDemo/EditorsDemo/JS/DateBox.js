$(function () {
    $("#BirthDate").dxDateBox({
        type: "date", // "datetime
        max: now,
        min: new Date(2025, 1, 1,),
        value: now,
        // list of all weekends into 2025
        onvalueChanged: function (e) {
            console.log(e.value);
            console.log(e.previousValue);
        }

        // disableDates: function()
    })
})