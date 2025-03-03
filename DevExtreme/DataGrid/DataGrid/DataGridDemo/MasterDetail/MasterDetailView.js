import { users, countries, states } from '../userData.js';

$(function () {
    $("#dataGrid").dxDataGrid({
        dataSource: users,
        columnFixing: { enabled: true },
        allowColumnReordering: true,
        columnAutoWidth: true,
        rowAlternationEnabled: true,
        showBorders: true,

        columns: [
            { dataField: "ID", caption: "ID", width: 100 },
            { dataField: "Name", caption: "Full Name", width: 300 },
           
        ],

        masterDetail: {
            enabled: true,
            template: function (container, options) {
                let user = options.data;

                // Get Country & State Names
                let country = countries.find(c => c.ID === user.CountryID)?.Name || "Unknown";
                let state = states.find(s => s.ID === user.StateID)?.Name || "Unknown";

                $("<div>").addClass("detailGridContainer").appendTo(container).dxDataGrid({
                    dataSource: [
                        { Field: "Full Name", Value: user.Name },
                        { Field: "Email", Value: user.Email },
                        { Field: "Phone", Value: user.PhoneNumber },
                        { Field: "Hire Date", Value: user.HireDate },
                        { Field: "Gender", Value: user.Gender === 1 ? "Male" : "Female" },
                        { Field: "Country", Value: country },   
                        { Field: "State", Value: state },       
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
