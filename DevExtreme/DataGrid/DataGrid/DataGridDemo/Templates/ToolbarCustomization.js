import { users } from '../userData.js';

$(function () {
    $("#dataGrid").dxDataGrid({
        dataSource: users,
        keyExpr: "ID",
        columnFixing: { enabled: true },
        allowColumnReordering: true,
        columnAutoWidth: true,
        showColumnLines: true,
        showRowLines: true,
        rowAlternationEnabled: true,
        showBorders: true,

        searchPanel: { visible: true }, // Optional: Adds a search panel in toolbar
        editing: {
            mode: "row",
            allowAdding: true,
            allowUpdating: true,
            allowDeleting: true
        },

        onToolbarPreparing: function (e) {
            console.log("Toolbar is being prepared...");

            let dataGrid = e.component;

            // Add Refresh Button (Clears Filters and Refreshes Data)
            e.toolbarOptions.items.unshift({
                location: "after",
                widget: "dxButton",
                options: {
                    icon: "refresh",
                    onClick: function () {
                        console.log("Refresh button clicked!");

                        // Clear Filters
                        dataGrid.clearFilter();

                        // Refresh DataGrid
                        dataGrid.refresh();
                    }
                }
            });

            // Add Gender Filter Dropdown
            e.toolbarOptions.items.unshift({
                location: "before",
                widget: "dxSelectBox",
                options: {
                    placeholder: "Filter by Gender",
                    items: [
                        { id: null, text: "All" },
                        { id: 1, text: "Male" },
                        { id: 2, text: "Female" }
                    ],
                    displayExpr: "text",
                    valueExpr: "id",
                    onValueChanged: function (e) {
                        console.log("Gender filter changed:", e.value);

                        if (e.value === null) {
                            dataGrid.clearFilter();
                        } else {
                            dataGrid.filter(["Gender", "=", e.value]);
                        }
                    }
                }
            });

            console.log("Toolbar items:", e.toolbarOptions.items);
        },


        columns: [
            { dataField: "ID", caption: "ID", width: 50 },
            { dataField: "Name", caption: "Full Name" },
            { dataField: "Email", caption: "Email Address" },
            { dataField: "PhoneNumber", caption: "Phone Number" },
            { dataField: "HireDate", caption: "Hire Date" },
            {
                dataField: "Gender",
                caption: "Gender",
                lookup: {
                    dataSource: [
                        { id: 1, text: "Male" },
                        { id: 2, text: "Female" }
                    ],
                    valueExpr: "id",
                    displayExpr: "text"
                }
            }
        ]
    });

    console.log($("#dataGrid").dxDataGrid("instance").option("toolbar"));
});
