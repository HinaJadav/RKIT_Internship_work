$(function () {

   

    // 1. Data Binding
    let apiUrl = "https://67c68a2f351c081993fdadd8.mockapi.io/Bug";

    /*let dataStore = new DevExpress.data.CustomStore({
        load: function () {
            return $.ajax({
                url: apiUrl,
                method: "GET",
                dataType: "json"
            })
                .then(response => response)
                .fail(error => console.error("Error loading data:", error));
        },

       *//* update: function (key, values) {
            console.log("Updating record:", key, values);
            return $.ajax({
                url: `${apiUrl}/${key}`,
                method: "PUT",
                data: JSON.stringify(values),
                contentType: "application/json",
                dataType: "json"
            })
                .then(() => console.log("Record updated successfully!"))
                .fail(error => console.error("Error updating data:", error));
        },*//*

        update: function (key, values) {
            console.log("Updating record:", key, values); // Debugging log

            if (typeof key !== "string" && typeof key !== "number") {
                console.error("❌ Invalid key for update:", key);
                return Promise.reject("Invalid key");
            }

            return $.ajax({
                url: `${apiUrl}/${key}`, // Ensure key is appended correctly
                method: "PUT",
                data: JSON.stringify(values),
                contentType: "application/json",
                dataType: "json"
            })
                .then(response => console.log("✅ Updated successfully:", response))
                .fail(error => console.error("❌ Error updating data:", error));
        },


        insert: function (values) {
            console.log("Inserting record:", values);
           

            return $.ajax({
                url: apiUrl,
                method: "POST",
                data: JSON.stringify(values),
                contentType: "application/json",
                dataType: "json"
            })
            .then((response) => {
                console.log("Record added successfully!", response);
                return response; // Return new object with generated ID
            })
            .fail(error => console.error("Error inserting data:", error));
        },


        remove: function (key) {
            console.log("Deleting record:", key);
            return $.ajax({
                url: `${apiUrl}/${key}`,
                method: "DELETE",
                dataType: "json"
            })
                .then(() => console.log("Record deleted successfully!"))
                .fail(error => console.error("Error deleting data:", error));
        }
    });*/
    var dataStore = new DevExpress.data.CustomStore({
        key: 'id',  // Unique identifier for each record

        // Function to load data from API
        load: async function () {
            try {
                let data = await $.get("https://67c68a2f351c081993fdadd8.mockapi.io/Bug");
                return data;
            } catch (error) {
                DevExpress.ui.notify('Error loading data', 'error', 2000);
                console.error('Load Error:', error);
                return [];
            }
        },

        /*// Function to insert new data into API
        insert: async function (values) {
            try {
                // Fix: Use the correct endpoint (no /add) and handle response properly
                let result = await $.ajax({
                    url: "https://67c68a2f351c081993fdadd8.mockapi.io/Bug",
                    method: 'POST',
                    data: values,
                });
                DevExpress.ui.notify(`Row added: ID = ${result.id}`, 'success', 2000);
                return result;
            } catch (error) {
                DevExpress.ui.notify('Error adding data: ' + error.statusText, 'error', 2000);
                console.error('Insert Error:', error);
                throw error;
            }
        },*/

        insert: async function (values) {
            try {
                let { id, ...dataToInsert } = values; // Remove ID if present

                let result = await $.ajax({
                    url: "https://67c68a2f351c081993fdadd8.mockapi.io/Bug",
                    method: 'POST',
                    data: dataToInsert,
                });

                // Convert flat structure to nested format
                let formattedResult = {
                    ...result,
                    id: parseInt(result.id), // Ensure ID is a number
                    bugCategory: { id: parseInt(result["bugCategory[id]"]) || null }, // Convert back to nested object
                    assignedUser: {
                        email: result["assignedUser[email]"] || "",
                        name: result["assignedUser[name]"] || "",
                        role: result["assignedUser[role]"] || ""
                    }
                };

                // Remove unnecessary flattened keys
                delete formattedResult["bugCategory[id]"];
                delete formattedResult["assignedUser[email]"];
                delete formattedResult["assignedUser[name]"];
                delete formattedResult["assignedUser[role]"];

                DevExpress.ui.notify(`Row added: ID = ${formattedResult.id}`, 'success', 2000);
                return formattedResult; // Return the transformed data
            } catch (error) {
                DevExpress.ui.notify('Error adding data: ' + error.statusText, 'error', 2000);
                console.error('Insert Error:', error);
                throw error;
            }
        },                    


        // Function to update an existing record
        update: async function (key, values) {
            try {
                let result = await $.ajax({
                    url: `https://67c68a2f351c081993fdadd8.mockapi.io/Bug/${key}`,
                    method: 'PUT',
                    data: values,
                });
                DevExpress.ui.notify(`Row updated: ID = ${key}`, 'success', 2000);
                return result;
            } catch (error) {
                DevExpress.ui.notify('Error updating data: ' + error.statusText, 'error', 2000);
                console.error('Update Error:', error);
                throw error;
            }
        },

        // Function to delete a record
        remove: async function (key) {
            try {
                let result = await $.ajax({
                    url: `https://67c68a2f351c081993fdadd8.mockapi.io/Bug/${key}`,
                    method: 'DELETE',
                });
                DevExpress.ui.notify(`Row removed: ID = ${key}`, 'success', 2000);
                return result;
            } catch (error) {
                DevExpress.ui.notify('Error removing data: ' + error.statusText, 'error', 2000);
                console.error('Remove Error:', error);
                throw error;
            }
        }
    });



    let priorityDataSource = [
        { id: "High", name: "High" },
        { id: "Medium", name: "Medium" },
        { id: "Low", name: "Low" }
    ];

    let severityDataSource = [
        { id: "Critical", name: "Critical" },
        { id: "Major", name: "Major" },
        { id: "Minor", name: "Minor" }
    ];

    let bugCategoryDataSource = [
        { id: 1, name: "Authentication Issue" },
        { id: 2, name: "Database Issue" },
        { id: 3, name: "CORS/Network Issue" },
        { id: 4, name: "Session Management" },
        { id: 5, name: "Frontend/UI" },
        { id: 6, name: "Backend/API" },
        { id: 7, name: "Real-Time Communication" },
        { id: 8, name: "Documentation" }
    ];

    let customizeColumns = [
        {
            dataField: "id",
            caption: "ID",
            // column resizing
            width: 50,
            allowResizing: true,
        },
        {
            dataField: "title",
            caption: "Title",
            validationRules: [
                {
                    type: "required",
                    message: "Title is requireed!",
                },
                {
                    type: "stringLength",
                    min: 10,
                    message: "Name must be at least 10 characters!"
                }
            ],
            editorOptions: {
                valueChangeEvent: "input",
                trim: true // Removes leading and trailing spaces automatically
            }
        },
        {
            dataField: "description",
            caption: "Description"
        },
       /* {
            dataField: "status",
            caption: "Status",
            // Cell Customization
            cellTemplate: function (container, options) {
                let statusText = options.value || "Unknown"; // Handle null/undefined values
                let iconClass = "dx-icon-help"; // Default icon

                switch (statusText) {
                    case "Open":
                        iconClass = "dx-icon-plus"; // DevExtreme Plus icon
                        break;
                    case "In Progress":
                        iconClass = "dx-icon-clock"; // DevExtreme Clock icon
                        break;
                    case "Resolved":
                        iconClass = "dx-icon-check"; // DevExtreme Check icon
                        break;
                }

                // Append icon and text inside the cell
                $("<div>")
                    .append($("<i>").addClass("dx-icon " + iconClass)) // DevExtreme icon
                    .append(" " + statusText) // Text next to the icon
                    .appendTo(container);
            }
        },*/
        {
            dataField: "status",
            caption: "Status",

            // Dropdown list for predefined values
            lookup: {
                dataSource: ["Open", "In Progress", "Resolved"], // Only these values allowed
                valueExpr: "this", // Use direct values
                displayExpr: "this"
            },

            // Cell Customization with Icons
            cellTemplate: function (container, options) {
                let statusText = options.value || "Unknown"; // Handle null/undefined values
                let iconClass = "dx-icon-help"; // Default icon

                switch (statusText) {
                    case "Open":
                        iconClass = "dx-icon-plus"; // Plus icon
                        break;
                    case "In Progress":
                        iconClass = "dx-icon-clock"; // Clock icon
                        break;
                    case "Resolved":
                        iconClass = "dx-icon-check"; // Check icon
                        break;
                }

                // Append icon and text inside the cell
                $("<div>")
                    .append($("<i>").addClass("dx-icon " + iconClass)) // DevExtreme icon
                    .append(" " + statusText) // Text next to the icon
                    .appendTo(container);
            },

            // Validation rule to restrict input
            validationRules: [
                {
                    type: "custom",
                    message: "Status must be Open, In Progress, or Resolved.",
                    validationCallback: function (e) {
                        return ["Open", "In Progress", "Resolved"].includes(e.value);
                    }
                }
            ]
        },
        { dataField: "createdAt", caption: "Created At", dataType: "date" },
        {
            dataField: "priority",
            caption: "Priority",
            lookup: {
                dataSource: priorityDataSource, // column based on data source
                valueExpr: "id",
                displayExpr: "name"
            }
        },
        {
            dataField: "severity",
            caption: "Severity",
            lookup: {
                dataSource: severityDataSource, // column based on data source
                valueExpr: "id",
                displayExpr: "name"
            }
        },
        {
            dataField: "bugCategory.id",
            caption: "Bug Category",
            lookup: {
                dataSource: bugCategoryDataSource, // column based on data source
                valueExpr: "id",
                displayExpr: "name"
            }
        },
        {
            // multi - level headers
            caption: "Assigned User",
            columns: [
                {
                    dataField: "assignedUser.name",
                    caption: "Name",
                    width: 180,
                    allowResizing: true
                },
                {
                    dataField: "assignedUser.email",
                    caption: "Email",
                    // data validation
                    validationRules: [
                        { type: "required", message: "Email is required" },
                        { type: "email", message: "Invalid email format" }
                    ],
                    width: 220,
                    allowResizing: true
                },
                {
                    dataField: "assignedUser.role",
                    caption: "Role",
                    width: 150,
                    allowResizing: true
                }
            ]
        },
        {
            type: 'buttons',
            caption: 'Actions',
            buttons: [
                {
                    name: 'edit',
                    icon: 'edit'
                },
                {
                    name: 'delete',
                    icon: 'trash'
                }
            ]
        }
    ];

    let customPaging = {
        pageSize: 8 // Default number of rows per page
    };

    let customPager = {
        visible: true,
        allowedPageSizes: [5, 10, 20, 'all'], // Better predefined options for usability
        displayMode: "full", // Shows full pager UI with buttons and page size selector

        infoText: "Page {0} of {1} ({2} items)", // Displays: "Page X of Y (Total Z items)"

        showInfo: true, // Shows total items and the current page range
        showNavigationButtons: true, // Enables next/prev navigation buttons
        showPageSizeSelector: true // Allows user to change page size
    };

    let customScrolling = {
        mode: "infinite", // Loads data in chunks as the user scrolls. Good for large datasets.
        showScrollbar: "onScroll", // Scrollbar appears only when scrolling.
        scrollByThumb: true, // Allows scrolling using the scrollbar thumb.
        scrollByContent: true, // Enables scrolling when dragging the content itself.
        rowRenderingMode: "virtual", // Renders only the visible rows for better performance.
        columnRenderingMode: "virtual" // Renders only the visible columns to improve rendering speed.
    };

    let customPopEditing = {
        mode: "popup",
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
            colCount: 2,
            
        }
    };

    $("#dataGrid").dxDataGrid({
        // data binding
        dataSource: dataStore,
        keyExpr: "id",
        columnFixing: { enabled: true },
        allowColumnReordering: true,
        columnAutoWidth: true,
        autoGenerateColumns: false, // prevent auto-generated columns order
        allowColumnResizing: true,
        columnResizingMode: "widget",

        // appearance
        showColumnLines: true,
        showRowLines: true,
        rowAlternationEnabled: true,
        showBorders: true,

        columns: customizeColumns,

        // paging
        paging: customPaging,
        pager: customPager,

        // scrolling
        scrolling: customScrolling,

        // editing
        // popup editing
        editing: customPopEditing,

        

        // data validation
        // cascadng lookup


        // grouping

        // filter - panel


        // ----------------------
        // events

        // events related to editing
        onRowUpdated: function (e) {
            console.log("✅ Row Updated:", e);
            DevExpress.ui.notify("Record updated successfully!", "success", 2000);
        },

        onRowInserted: function (e) {
            console.log("➕ Row Inserted:", e);
            DevExpress.ui.notify("Record added successfully!", "success", 2000);
        },

        onRowRemoved: function (e) {
            console.log("🗑️ Row Removed:", e);
            DevExpress.ui.notify("Record deleted successfully!", "warning", 2000);
        },

        onDataErrorOccurred: function (e) {
            console.error("❌ Data Error:", e.error);
            DevExpress.ui.notify("Error processing data: " + e.error.message, "error", 3000);
        }



    });
});
