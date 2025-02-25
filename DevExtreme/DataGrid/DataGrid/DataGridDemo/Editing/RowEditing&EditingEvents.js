/// <reference path="../../scripts/bootstrap.esm.js" />
$(function () {
    const apiUrl = "https://jsonplaceholder.typicode.com/users";

    let dataStore = new DevExpress.data.CustomStore({
        load: function () {
            return $.ajax({
                url: apiUrl,
                method: "GET",
                dataType: "json"
            })
                .then(response => response)
                .fail(error => console.error("Error loading data:", error));
        },
        update: function (key, values) {
            return $.ajax({
                url: `${apiUrl}/${key}`,
                method: "PUT",
                data: values,
                dataType: "json"
            })
                .then(response => response)
                .fail(error => console.error("Error updating data:", error));
        },
        insert: function (values) {
            return $.ajax({
                url: apiUrl,
                method: "POST",
                data: values,
                dataType: "json"
            })
                .then(response => response)
                .fail(error => console.error("Error inserting data:", error));
        },
        remove: function (key) {
            return $.ajax({
                url: `${apiUrl}/${key}`,
                method: "DELETE",
                dataType: "json"
            })
                .then(response => response)
                .fail(error => console.error("Error deleting data:", error));
        }
    });

    $("#dataGrid").dxDataGrid({
        dataSource: dataStore,
        columnFixing: { enabled: true },
        allowColumnReordering: true,
        columnAutoWidth: true,
        rowAlternationEnabled: true,
        showBorders: true,

        paging: {
            pageSize: 10,
        },

        pager: {
            visible: true,
            allowedPageSizes: [5, 10, 'all'],
            displayMode: "full",
            label: "Page Navigation",
            showInfo: true,
            showNavigationButtons: true,
            showPageSizeSelector: true,
        },

        scrolling: {
            mode: "infinite",
        },

        // Editing events
        onEditingStart: function (e) {
            console.log("Editing mode ON.", e);
        },

        onRowInserting: function (e) {
            console.log("New data is ready for inserting into row.", e);
        },

        onRowInserted: function (e) {
            console.log("New row inserted successfully!", e);
        },

        onRowUpdating: function (e) {
            console.log("Row is being updated.", e);
        },

        onRowUpdated: function (e) {
            console.log("Row updated successfully!", e);
        },

        onRowRemoving: function (e) {
            console.log("Row is being removed.", e);
        },

        onRowRemoved: function (e) {
            console.log("Row removed successfully!", e);
        },

        onSaving: function (e) {
            console.log("Changes are being saved.", e);
        },

        onSaved: function (e) {
            console.log("Changes saved successfully!", e);
        },

        onEditCanceling: function (e) {
            console.log("Edit is being canceled.", e);
        },

        onEditCanceled: function (e) {
            console.log("Edit has been canceled.", e);
        },

        onInitNewRow: function (e) {
            console.log("New row is added!", e);
        },

        editing: {
            mode: "row",
            allowUpdating: true,
            allowAdding: true,
            allowDeleting: true,
        },
    });
});
