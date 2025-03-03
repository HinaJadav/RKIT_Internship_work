import { users, countries, states } from '../userData.js';

$(function () {
    $("#dataGrid").dxDataGrid({
        dataSource: users, // Set the data source
        columnFixing: { enabled: true }, // Enable column fixing
        allowColumnReordering: true, // Allow column reordering
        columnAutoWidth: true, // Auto adjust column width
        rowAlternationEnabled: true, // Apply row alternation style
        showBorders: true, // Show grid borders

        columnHidingEnabled: true, // Enable column hiding when screen is small
        wordWrapEnabled: true, // Enable text wrapping for better adaptability
        columnResizingMode: "widget", // Resize columns within the widget

        editing: {
            mode: "row", // Enable row-based editing
            allowUpdating: true,
            allowAdding: true,
            allowDeleting: true,
        },

        columns: [
            { dataField: "ID", caption: "ID", width: 50, hidingPriority: 8 }, // Highest priority to stay visible
            { dataField: "Name", caption: "Full Name", hidingPriority: 7 },
            { dataField: "Email", caption: "Email Address", hidingPriority: 6 },
            { dataField: "PhoneNumber", caption: "Phone Number", hidingPriority: 5 },
            { dataField: "HireDate", caption: "Hire Date", hidingPriority: 4 },
            {
                dataField: "Gender",
                caption: "Gender",
                lookup: {
                    dataSource: [
                        { id: 1, text: "Male" },
                        { id: 2, text: "Female" }
                    ],
                    valueExpr: "id",
                    displayExpr: "text"
                },
                hidingPriority: 3
            },
            {
                dataField: "CountryID",
                caption: "Country",
                lookup: { dataSource: countries, valueExpr: "ID", displayExpr: "Name" },
                hidingPriority: 2
            },
            {
                dataField: "StateID",
                caption: "State",
                lookup: { dataSource: states, valueExpr: "ID", displayExpr: "Name" },
                hidingPriority: 1
            }
        ],

        // Enable adaptive column rendering
        adaptivityEnabled: true
    });
});
