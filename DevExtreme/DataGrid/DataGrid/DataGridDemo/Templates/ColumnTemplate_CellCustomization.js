import { users } from '../userData.js';

$(function () {
    $("#dataGrid").dxDataGrid({
        dataSource: users,
        keyExpr: "ID",
        columnFixing: { enabled: true },
        allowColumnReordering: true,
        columnAutoWidth: true,
        showColumnLines: true,
        showRowLines: true,
        rowAlternationEnabled: true,
        showBorders: true,

        searchPanel: { visible: true }, 
        editing: {
            mode: "row",
            allowAdding: true,
            allowUpdating: true,
            allowDeleting: true
        },
        toolbar: {
            items: [
                {
                    location: "before",
                    widget: "dxButton",
                    options: {
                        text: "Custom Button",
                        onClick: function () {
                            alert("Button Clicked!");
                        }
                    }
                }
            ]
        },

        columns: [
            { dataField: "ID", caption: "ID", width: 50 },
            { dataField: "Name", caption: "Full Name" },
            { dataField: "Email", caption: "Email Address" },
            {
                dataField: "PhoneNumber",
                caption: "Phone Number",
                 cellTemplate: function (container, options) {
                     $("<div>")
                         .append($("<span>").text("📞 " + options.value))
                         .appendTo(container);
                 }
            },
            {
                dataField: "HireDate",
                caption: "Hire Date",
                cellTemplate: function (container, options) {
                    $("<div>")
                        .append($("<span>").text("📅 " + options.value))
                        .appendTo(container);
                }
            },
            {
                dataField: "Gender",
                caption: "Gender",
                cellTemplate: function (container, options) {
                    $("<div>")
                        .text(options.value === 1 ? "👨 Male" : "👩 Female")
                        .appendTo(container);
                }
            },
        ],


        



    });

    console.log($("#dataGrid").dxDataGrid("instance").option("toolbar"));

});
