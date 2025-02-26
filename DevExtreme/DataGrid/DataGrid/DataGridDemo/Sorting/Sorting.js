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

        // Sorting
        // Sorting applies to data by single and multiple columns

        sorting: {
            mode: "multiple", // Options: "single", "multiple", "none"
            ascendingText: "Sort Ascending", // Text for ascending sort option in column header
            descendingText: "Sort Descending", // Text for descending sort option in column header
            clearText: "Clear Sorting", // Text for clearing sorting option in column header
            showSortIndexes: true // Displays sort index when multiple sorting is enabled
        },

        columns: [
            {
                dataField: "ID",
                caption: "ID",
                sortIndex: 0,
                sortOrder: "asc", // Sorts ID column in ascending order
                allowSorting: true // Allows sorting on this column
            },
            {
                dataField: "Name",
                caption: "Name",
                sortIndex: 1,
                sortOrder: "desc", // Sorts Name column in descending order
                allowSorting: true // Allows sorting on this column
            },
            {
                dataField: "CountryID",
                caption: "Country",
                lookup: { dataSource: countries, valueExpr: "ID", displayExpr: "Name" },
                allowSorting: true // Allows sorting on this column
            },
            {
                dataField: "StateID",
                caption: "State",
                lookup: { dataSource: states, valueExpr: "ID", displayExpr: "Name" },
                allowSorting: false, // Disables sorting for the State column
            }
        ],

        // Allows users to reset sorting by clicking a button
        onOptionChanged: function (e) {
            if (e.fullName === "sorting.mode" && e.value === "none") {
                $("#dataGrid").dxDataGrid("instance").clearSorting();
            }
        }
    });

    $("#clearSortingBtn").dxButton({
        text: "Clear Sorting",
        type: "default",
        onClick: function () {
            $("#dataGrid").dxDataGrid("instance").clearSorting();
        }
    });
});
