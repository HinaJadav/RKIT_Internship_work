import { users, countries, states } from '../userData.js';

$(function () {
    $("#dataGrid").dxDataGrid({
        dataSource: users,
        keyExpr: "ID",
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

        selection: {
            mode: "multiple", // Allows multiple row selection
            selectAllMode: "page", // "page" or "allPages"
            allowSelectAll: true, // Enables the "Select All" checkbox
            showCheckBoxesMode: "onClick" // Options: "onClick", "onLongTap", "always", "none"
        }
    });

    // Function to clear selection
    $("#clearSelectionButton").dxButton({
        text: "Clear Selection",
        onClick: function () {
            var dataGrid = $("#dataGrid").dxDataGrid("instance");
            dataGrid.clearSelection();
        }
    });
});
