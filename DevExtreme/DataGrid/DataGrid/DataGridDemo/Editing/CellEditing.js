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
            console.log("Updating record with key:", key, "Values:", values);
            return $.ajax({
                url: `${apiUrl}/${key}`,
                method: "PUT",
                data: values,
                dataType: "json"
            })
                .then(response => {
                    alert("Record updated successfully!");
                    return response;
                })
                .fail(error => console.error("Error updating data:", error));
        },
        insert: function (values) {
            console.log("Inserting new record:", values);
            return $.ajax({
                url: apiUrl,
                method: "POST",
                data: values,
                dataType: "json"
            })
                .then(response => {
                    alert("Record added successfully!");
                    return response;
                })
                .fail(error => console.error("Error inserting data:", error));
        },
        remove: function (key) {
            console.log("Deleting record with key:", key);
            return $.ajax({
                url: `${apiUrl}/${key}`,
                method: "DELETE",
                dataType: "json"
            })
                .then(response => {
                    alert("Record deleted successfully!");
                    return response;
                })
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

        editing: {
            mode: "cell",
            allowUpdating: true,
            allowAdding: true,
            allowDeleting: true,
        },

        // events triggers during cell editing mode

        onCellClick: function (e) {
            console.log("Cell clicked: ", e.data);
        },

        onCellDbClick: function (e) {
            console.log("Cell Double Clicked: ", e.data);
        },

        onCellPrepared: function (e) {
            if (e.rowType === "data" && e.column.dataField === "phone" && !e.value) {
                e.cellElement.style.backgroundColor = "lightcoral"; // Highlight empty phone numbers
            }
        },

        onEditingStart: function (e) {
            if (e.column.dataField === "id") {
                e.cancel = true;  // Prevents editing on the "id" column
            }
        },

        

    });
});

/*
In the cell mode, a user edits data cell by cell. Changes are saved once a cell loses the focus, or discarded if a user presses Esc. An added row is saved only when the focus is shifted from it. Choose this mode if any changes should be saved to the data source immediately.
*/