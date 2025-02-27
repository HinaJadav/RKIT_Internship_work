import { users, countries, states } from '../userData.js';

$(function () {
    $("#dataGrid").dxDataGrid({
        dataSource: users,
        keyExpr: "ID",
        columnFixing: { enabled: true }, // Allow fixing columns
        allowColumnReordering: true, // Enable column drag & drop
        columnAutoWidth: true, // Auto adjust column width
        rowAlternationEnabled: true, // Zebra striping for rows
        showBorders: true, // Show grid borders
        hoverStateEnabled: true, // Highlight row on hover

        editing: {
            mode: "row", // Row editing mode
            allowUpdating: true,
            allowAdding: true,
            allowDeleting: true,
        },

        // Columns
        columns: [
            {
                dataField: "Name",
                caption: "Name",
                validationRules: [{ type: "required" }], 
                
            },
            {
                caption: "Contacts",
                columns: [
                    {
                        dataField: "Email",
                        caption: "Email",
                        validationRules: [{ type: "email" }],
                        // Cell Template
                        cellTemplate: function (element, info) {
                            element.append("<div>" + info.text + "</div>")
                                .css("color", "blue");
                        }
                    },
                    { dataField: "PhoneNumber", caption: "Phone Number" }
                ]
            },
            {
                dataField: "CountryID",
                caption: "Country",
                lookup: {
                    dataSource: countries,
                    valueExpr: "ID",
                    displayExpr: "Name"
                }
            },
            {
                dataField: "StateID",
                caption: "State",
                lookup: {
                    dataSource: states,
                    valueExpr: "ID",
                    displayExpr: "Name"
                }
            },
            {
                dataField: "HireDate",
                caption: "Hire Date",
                dataType: "date" // Simple Data Column (Date Type)
            },
            {
                dataField: "Gender",
                caption: "Gender",
                lookup: { // Lookup Column using static data
                    dataSource: [
                        { ID: 1, Name: "Male" },
                        { ID: 2, Name: "Female" },
                        { ID: 3, Name: "Other" }
                    ],
                    valueExpr: "ID",
                    displayExpr: "Name"
                }
            },
            {
                type: "buttons",
                buttons: [
                    {
                        name: "save",
                        cssClass: "my-class" // Custom class for styling
                    },
                    "edit",
                    "delete"
                ]
            }
        ],

        customizeColumns: function (columns) {
            columns[0].width = 100; // Set width for Name column
            columns[1].width = 210; // Set width for Contact band column
        }
    });
});
