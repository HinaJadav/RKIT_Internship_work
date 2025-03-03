import { users } from '../userData.js';

$(function () {
    $("#dataGrid").dxDataGrid({
        dataSource: users,
        columnFixing: { enabled: true },
        allowColumnReordering: true,
        columnAutoWidth: true,
        rowAlternationEnabled: true,
        showBorders: true,
        selection: {
            mode: "multiple" // Allow row selection for export
        },
        // Export settings
        export: {
            enabled: true, // Enable export feature
            formats: ['pdf', 'xlsx'], // Allow both PDF and Excel export
            allowExportSelectedData: true, // Allow exporting only selected rows
        },

        // Handle export functionality
        onExporting(e) {
            if (e.format === "xlsx") { // Check if export format is Excel
                const workbook = new ExcelJS.Workbook(); // Create a new Excel workbook
                const worksheet = workbook.addWorksheet('Employees'); // Add a worksheet

                // Export DataGrid data to Excel
                DevExpress.excelExporter.exportDataGrid({
                    component: e.component,
                    worksheet,
                    autoFilterEnabled: true, // Enable auto-filters in Excel
                }).then(() => {
                    // Convert workbook to binary and trigger download
                    workbook.xlsx.writeBuffer().then((buffer) => {
                        saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Employees.xlsx');
                    });
                });

                e.cancel = true; // Prevent default export behavior
            }
        },

        toolbar: {
            items: [
                "exportButton" // Add Export Button to Toolbar
            ]
        },

        columns: [
            { dataField: "ID", caption: "ID", width: 100 },
            { dataField: "Name", caption: "Full Name", width: 300 },
            { dataField: "Email", caption: "Email", width: 250 }
        ]
    });
});
