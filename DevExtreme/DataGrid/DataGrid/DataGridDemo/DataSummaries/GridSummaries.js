/*import { users, countries, states } from '../userData.js';
*/
$(function () {
    const users = [
        { ID: 1, Name: 'Amit Sharma', Email: 'amit.sharma@example.com', PhoneNumber: '+91-98765-43210', CountryID: 1, StateID: 1, HireDate: '2022-05-15', Gender: 1, Salary: 120000 },
        { ID: 2, Name: 'Neha Verma', Email: 'neha.verma@example.com', PhoneNumber: '+91-87654-32109', CountryID: 1, StateID: 2, HireDate: '2021-08-20', Gender: 2, Salary: 95000 },
        { ID: 3, Name: 'Rohan Gupta', Email: 'rohan.gupta@example.com', PhoneNumber: '+91-76543-21098', CountryID: 1, StateID: 3, HireDate: '2023-01-10', Gender: 1, Salary: 78000 },
        { ID: 4, Name: 'Emily Brown', Email: 'emily.brown@example.com', PhoneNumber: '+1-234-567-8901', CountryID: 2, StateID: 6, HireDate: '2020-07-05', Gender: 2, Salary: 135000 },
        { ID: 5, Name: 'Michael Johnson', Email: 'michael.johnson@example.com', PhoneNumber: '+1-345-678-9012', CountryID: 2, StateID: 7, HireDate: '2019-11-30', Gender: 1, Salary: 200000 },
        { ID: 6, Name: 'Sophia Wilson', Email: 'sophia.wilson@example.com', PhoneNumber: '+44-7890-123456', CountryID: 3, StateID: 10, HireDate: '2022-03-18', Gender: 2, Salary: 89000 },
        { ID: 7, Name: 'Lucas Schmidt', Email: 'lucas.schmidt@example.com', PhoneNumber: '+49-1520-9876543', CountryID: 4, StateID: 12, HireDate: '2018-09-25', Gender: 1, Salary: 155000 },
        { ID: 8, Name: 'William Taylor', Email: 'william.taylor@example.com', PhoneNumber: '+61-423-678-901', CountryID: 5, StateID: 14, HireDate: '2021-12-10', Gender: 1, Salary: 72000 }
    ];

    const countries = [
        { ID: 1, Name: 'India' },
        { ID: 2, Name: 'United States' },
        { ID: 3, Name: 'United Kingdom' },
        { ID: 4, Name: 'Germany' },
        { ID: 5, Name: 'Australia' }
    ];

    const states = [
        { ID: 1, Name: 'Maharashtra', CountryID: 1 },
        { ID: 2, Name: 'Karnataka', CountryID: 1 },
        { ID: 3, Name: 'Delhi', CountryID: 1 },
        { ID: 4, Name: 'Tamil Nadu', CountryID: 1 },
        { ID: 5, Name: 'Gujarat', CountryID: 1 },
        { ID: 6, Name: 'California', CountryID: 2 },
        { ID: 7, Name: 'Texas', CountryID: 2 },
        { ID: 8, Name: 'New York', CountryID: 2 },
        { ID: 9, Name: 'England', CountryID: 3 },
        { ID: 10, Name: 'Scotland', CountryID: 3 },
        { ID: 11, Name: 'Wales', CountryID: 3 },
        { ID: 12, Name: 'Bavaria', CountryID: 4 },
        { ID: 13, Name: 'Berlin', CountryID: 4 },
        { ID: 14, Name: 'New South Wales', CountryID: 5 },
        { ID: 15, Name: 'Victoria', CountryID: 5 }
    ];

    console.log("Users Data:", users);

    $("#dataGrid").dxDataGrid({
        dataSource: users,
        columnFixing: { enabled: true },
        allowColumnReordering: true,
        columnAutoWidth: true,
        rowAlternationEnabled: true,
        showBorders: true,

        editing: {
            mode: "row",
            allowUpdating: true,
            allowAdding: true,
            allowDeleting: true,
        },

        grouping: {
            autoExpandAll: true
        },
        groupPanel: {
            visible: true
        },

        columns: [
            { dataField: "ID", caption: "ID", width: 50 },
            { dataField: "Name", caption: "Full Name" },
            { dataField: "Email", caption: "Email Address" },
            { dataField: "PhoneNumber", caption: "Phone Number" },
            { dataField: "HireDate", caption: "Hire Date", dataType: "date" },
            {
                dataField: "Gender",
                caption: "Gender",
                groupIndex: 0,
                lookup: {
                    dataSource: [
                        { id: 1, text: "Male" },
                        { id: 2, text: "Female" }
                    ],
                    valueExpr: "id",
                    displayExpr: "text"
                }
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
                dataField: "Salary",
                caption: "Salary",
                dataType: "number",
                format: { type: "currency", precision: 0 },
                calculateCellValue: (data) => data.Salary ?? 0,

            }
        ],

        summary: {
            groupItems: [
                {
                    column: "ID",
                    summaryType: "count",
                    displayFormat: "Total: {0} Employees"
                },
                {
                    column: "Salary",
                    summaryType: "sum",
                    displayFormat: "Total Salary: {0}",
                    valueFormat: { type: "currency" }
                }
            ],

            totalItems: [
                {
                    name: "MaleCount",
                    summaryType: "custom",
                    displayFormat: "Total Males: {0}"
                },
                {
                    name: "FemaleCount",
                    summaryType: "custom",
                    displayFormat: "Total Females: {0}"
                },
                {
                    column: "Salary",
                    summaryType: "sum",
                    displayFormat: "Total Salary: {0}",
                    valueFormat: { type: "currency" }
                }
            ],

            calculateCustomSummary: function (options) {
                if (options.name === "MaleCount") {
                    if (options.summaryProcess === "start") {
                        options.totalValue = 0; // Initialize count
                    }
                    if (options.summaryProcess === "calculate") {
                        if (options.value.Gender === 1) {
                            options.totalValue++; // Count Male employees
                        }
                    }
                }
                if (options.name === "FemaleCount") {
                    if (options.summaryProcess === "start") {
                        options.totalValue = 0; // Initialize count
                    }
                    if (options.summaryProcess === "calculate") {
                        if (options.value.Gender === 2) {
                            options.totalValue++; // Count Female employees
                        }
                    }
                }
            }
        },

        masterDetail: {
            enabled: true,
            template: function (container, options) {
                let user = options.data;

                $("<div>").addClass("detail-grid-container").appendTo(container).dxDataGrid({
                    dataSource: [
                        { Field: "Full Name", Value: user.Name },
                        { Field: "Email", Value: user.Email },
                        { Field: "Phone", Value: user.PhoneNumber },
                        { Field: "Hire Date", Value: user.HireDate },
                        { Field: "Salary", Value: user.Salary }
                    ],
                    showBorders: true,
                    columns: [
                        { dataField: "Field", caption: "Detail Field", width: 150 },
                        { dataField: "Value", caption: "Value" }
                    ]
                });
            }
        }
    });
});