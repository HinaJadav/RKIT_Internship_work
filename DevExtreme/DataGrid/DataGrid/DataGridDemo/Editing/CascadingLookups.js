import { users, countries, states } from '../userData.js';

$(function () {
  

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


        // Cascading LookUp

        columns: [
            { dataField: "ID", caption: "ID", allowEditing: false },
            { dataField: "Name", caption: "Name" },
            { dataField: "Email", caption: "Email" },
            { dataField: "PhoneNumber", caption: "Phone Number" },

            // Country Lookup
            {
                dataField: "CountryID",
                caption: "Country",
                lookup: {
                    dataSource: countries,
                    valueExpr: "ID",
                    displayExpr: "Name"
                },
                validationRules: [{
                    type: "required"
                }]
            },

            // State lookUp with filtering based on selected Country
            {
                dataField: "StateID",
                caption: "State",
                lookup: {
                    dataSource: function (options) {
                        return {
                            store: states,
                            filter: options.data ? ["CountryID", "=", options.data.CountryID] : null
                        };
                    },
                    valueExpr: "ID",
                    displayExpr: "Name"
                },
                validationRules: [{
                    type: "required"
                }]
            }
        ],

        // Event Handling
        onRowUpdating: function (e) {
            if ("CountryID" in e.newData) {
                e.newData.StateID = null; // Reset state if country changes
            }
        }
    });
});
