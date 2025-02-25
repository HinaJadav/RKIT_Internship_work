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

        grouping: {
            contextMenuEnabled: true, // Enable grouping commands in the context menu
        },

        groupPanel: {
            visible: true, // Show group panel
        },

        columns: [
            { dataField: "ID", caption: "ID", allowGrouping: false },
            { dataField: "Name", caption: "Name", groupIndex: 0 }, // Example: Group by Name initially
            { dataField: "CountryID", caption: "Country", lookup: { dataSource: countries, valueExpr: "ID", displayExpr: "Name" } },
            { dataField: "StateID", caption: "State", lookup: { dataSource: states, valueExpr: "ID", displayExpr: "Name" } }
        ],

        // Event Handling for Grouping
        onRowExpanding: function (e) {
            console.log("Row Expanding: ", e.key);
        },

        onRowExpanded: function (e) {
            console.log("Row Expanded: ", e.key);
        },

        onRowCollapsing: function (e) {
            console.log("Row Collapsing: ", e.key);
        },

        onRowCollapsed: function (e) {
            console.log("Row Collapsed: ", e.key);
        }
    });

    // Clear Grouping Button
    $("#clearGroupingBtn").dxButton({
        text: "Clear Grouping",
        type: "default",
        onClick: function () {
            $("#dataGrid").dxDataGrid("instance").clearGrouping();
        }
    });


});
