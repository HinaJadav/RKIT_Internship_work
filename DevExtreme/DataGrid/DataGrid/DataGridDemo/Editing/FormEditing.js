$(function () {
    const apiUrl = "https://jsonplaceholder.typicode.com/users";

    let dataStore = new DevExpress.data.CustomStore({
        load: () =>
            $.ajax({ url: apiUrl, method: "GET", dataType: "json" })
                .fail(error => alert("Error loading data: " + error.statusText)),

        update: (key, values) => {
            console.log("Updating record:", key, values);
            return $.ajax({
                url: `${apiUrl}/${key}`,
                method: "PUT",
                data: JSON.stringify(values),
                contentType: "application/json",
                dataType: "json"
            })
                .then(() => alert("Record updated successfully!"))
                .fail(error => alert("Error updating data: " + error.statusText));
        },

        insert: (values) => {
            console.log("Inserting record:", values);
            return $.ajax({
                url: apiUrl,
                method: "POST",
                data: JSON.stringify(values),
                contentType: "application/json",
                dataType: "json"
            })
                .then(() => alert("Record added successfully!"))
                .fail(error => alert("Error inserting data: " + error.statusText));
        },

        remove: (key) => {
            console.log("Deleting record:", key);
            return $.ajax({
                url: `${apiUrl}/${key}`,
                method: "DELETE",
                dataType: "json"
            })
                .then(() => alert("Record deleted successfully!"))
                .fail(error => alert("Error deleting data: " + error.statusText));
        }
    });

    $("#dataGrid").dxDataGrid({
        dataSource: dataStore,
        columnFixing: { enabled: true },
        allowColumnReordering: true,
        columnAutoWidth: true,
        rowAlternationEnabled: true,
        showBorders: true,

        editing: {
            mode: "form",
            allowUpdating: true,
            allowAdding: true,
            allowDeleting: true,
            form: {
                colCount: 2, // Organize input fields into 2 columns
                items: [
                    { dataField: "name", label: { text: "Full Name" }, isRequired: true },
                    { dataField: "username", label: { text: "Username" } },
                    { dataField: "email", label: { text: "Email" }, editorType: "dxTextBox" },
                    { dataField: "phone", label: { text: "Phone" } },
                    { dataField: "website", label: { text: "Website" } },
                ]
            }
        },

        onRowUpdated: (e) => console.log("Row updated:", e),
        onRowInserted: (e) => console.log("Row inserted:", e),
        onRowRemoved: (e) => console.log("Row removed:", e)
    });
});
