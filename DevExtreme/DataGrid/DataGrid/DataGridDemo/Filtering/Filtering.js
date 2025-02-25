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

        // Filtering

        // Filter Row: 
        // allows users to filters data by individual column's value
        // commman filter for : text boxes (mostly text data)
        // for date and boolean values : they contains other filtering controls

        filterRow: {
            visible: true, // to make the filter row visible
            applyFilter: "onClick", // apply filter after click on filter row's that field name

            // Header Filter
            // allows user to filter values in an individual column by including or excluding them from the applied filter

            headerFilter: {
                visible: true,
            }
        },


        // filter panel : Display all applied filter expressions
        filterPanel: {
            visible: true
        },

        filterSyncEnabled: true, // disables automatuc synchronization between row and filter panel

        filterBuilder: {
            allowHierarchicalFields: true, // allows complex conditions using hierarchical fields
            groupOperations: ["AND", "OR"], // defines acailable logical operators
            maxGroupLevel: 2, // Limits nesting depth for conditions
            fields: [ 
                { dataField: "ID", dataType: "number", caption: "ID" },
                { dataField: "Name", dataType: "string", caption: "Name" },
                { dataField: "CountryID", dataType: "number", caption: "Country" },
                { dataField: "StateID", dataType: "number", caption: "State" }
            ]
        },

        filterBuilderPopup: {
            width: 400,
            title: "Synchronized Filter"
        },

        // Enable Search panel and search functionality 
        searchPanel: {
            visible: true,
            text: "Search about user's data..."
        },

        columns: [
            {
                dataField: "ID",
                caption: "ID",
                dataType: "number", // Specify data type
                allowFiltering: false,
                allowHeaderFiltering: false
            },
            {
                dataField: "Name",
                caption: "Name",
                dataType: "string", // Specify data type
                filterOperations: ["contains", "="],
                selectedFilterOperation: "contains", 
            },
            {
                dataField: "CountryID",
                caption: "Country",
                dataType: "number", // IDs are usually numbers
                lookup: { dataSource: countries, valueExpr: "ID", displayExpr: "Name" },
                filterType: "exclude",
                headerFilter: {
                    allowSelectAll: false,
                    search: { enabled: true }
                }
            },
            {
                dataField: "StateID",
                caption: "State",
                dataType: "number", // IDs are usually numbers
                lookup: { dataSource: states, valueExpr: "ID", displayExpr: "Name" }
            }
        ],

    });

    // Clear any pre-applied filters when the grid loads
    $("#dataGrid").dxDataGrid("instance").clearFilter();
});
