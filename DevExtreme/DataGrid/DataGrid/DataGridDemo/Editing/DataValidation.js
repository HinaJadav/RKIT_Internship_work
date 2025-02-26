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
            mode: "cell",
            allowUpdating: true,
            allowAdding: true,
            allowDeleting: true,
            popup: {
                title: "Edit Record",
                showTitle: true,
                width: 600,
                height: 400
            },
            form: {
                colCount: 2
            }
        },

        // Data Validation duting editing

        columns: [
            {
                dataField: "name",
                caption: "Full Name",
                validationRules: [{
                    type: "required",
                    message: "Name is requireed!",
                },
                    { type: "stringLength", min: 3, message: "Name must be at least 3 characters!" }
                ],
                editorOptions: {
                    valueChangeEvent: "input",
                    trim: true // Removes leading and trailing spaces automatically
                }
            },
            {
                dataField: "email",
                caption: "Email",
                validationRules: [
                    { type: "required", message: "Email is required" },
                    { type: "email", message: "Invalid email format" }
                ] 
            },
            {
                dataField: "phone",
                caption: "Contact Number",
                validationRules: [
                    { type: "required", message: "Phone number is required!" },
                    { type: "pattern", pattern: "^[0-9]{10}$", message: "Phone number must be exactly 10 digits"}
                ]
            },
            {
                dataField: "website",
                caption: "Website",
                validationRules: [
                    // This rule ensures the field is not empty
                    {
                        type: "required",
                        message: "Your Website URL is required!"
                    },

                    // This rule checks if the entered URL is valid and reachable
                    {
                        type: "async",
                        reevaluate: true, // Revalidate if the user changes input
                        ignoreEmptyValue: true, // Skip validation if the field is empty
                        message: "Invalid or unreachable website!",

                        // Function to validate the website
                        validationCallback: function (params) {
                            return new Promise((resolve) => {
                                let url = params.value;

                                // Check if the URL format is correct
                                let urlPattern = /^(https?:\/\/)?([\w-]+(\.[\w-]+)+)(\/[\w-]*)*$/;

                                if (!urlPattern.test(url)) {
                                    resolve(false); // Fail if format is incorrect
                                } else {
                                    // Check if the website is reachable
                                    $.ajax({
                                        url: url,
                                        type: "HEAD", // Sends a lightweight request
                                        timeout: 3000 // Avoids long delays
                                    })
                                        .done(() => resolve(true)) // Pass if site is reachable
                                        .fail(() => resolve(false)); // Fail if site is not found
                                }
                            });
                        }
                    }
                ]
            }
        ],

        // Handling Validation Before Row Update

        onCellValidating: function (e) {
            console.log("Cell validation triggered for cell:", e.dataField, "Value: ", e.value);
        },

        onRowValidating: function (e) {
            console.log("Validation triggered for row: ", e);

            if (e.brokenRules.length > 0) {
                e.isValid = false; // prevents saving if validation fails
            }
        },

        onDataErrorOccurred: function (e) {
            console.error("Server-side validation error:", e.error);
            DevExpress.ui.notify("An error occurred while saving data!", "error", 3000);
        }


    });
});
