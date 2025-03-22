$(function () {
    let apiUrl = "https://67c68a2f351c081993fdadd8.mockapi.io/Bug";

    let dataStore = new DevExpress.data.CustomStore({
        key: "id",

        load: function () {
            let d = $.Deferred(); // use to  handle asynchronous operations in jQuery

            $.ajax({
                url: apiUrl,
                method: "GET",
                dataType: "json",
            }).done(function (data) {
                d.resolve(data); // Load all data
            }).fail(function (error) {
                d.reject("Data loading error: " + error);
            });

            return d.promise();
        },

        insert: function (values) {
            let d = $.Deferred();

            $.ajax({
                url: apiUrl,
                method: "POST",
                contentType: "application/json",
                data: JSON.stringify(values)
            }).done(function (response) {
                d.resolve(response);
            }).fail(function (error) {
                d.reject("Data inserting process error: " + error);
            });

            return d.promise();
        },

        update: function (key, values) {
            let d = $.Deferred();

            $.ajax({
                url: apiUrl + "/" + String(key), 
                method: "PUT",
                contentType: "application/json",
                data: JSON.stringify(values)
            }).done(function (response) {
                d.resolve(response);
            }).fail(function (error) {
                d.reject("Data updating process error: " + error);
            });

            return d.promise();
        },

        remove: function (key) { 
            let d = $.Deferred();

            $.ajax({
                url: apiUrl + "/" + String(key),
                method: "DELETE",
            }).done(function () {
                d.resolve();
            }).fail(function (error) {
                d.reject("Data deleting process error: " + error);
            });

            return d.promise();
        },
    })

    $("#dataGrid").dxDataGrid({
        dataSource: dataStore,
        allowColumnReordering: true,
        columns: [
            {
                dataField: "id",
                caption: "Bug ID",
                fixed: true,
                dataType: "number",
                validationRules: [{ type: "required" }]
                // other types: async(server side validation), compare, custom, email, numeric, pattern range, required, stringLength
            },
            {
                dataField: "title",
                caption: "Title",
                dataType: "string",
                validationRules: [{ type: "required" }]
            },
            {
                dataField: "description",
                caption: "Description",
                dataType: "string",
                width: 300
            },
            {
                dataField: "status",
                caption: "Status",
                dataType: "string",
                lookup: {
                    dataSource: ["Open", "In Progress", "Resolved"],
                    valueExpr: "this",
                    displayExpr: "this"
                },
                validationRules: [{ type: "required" }]
            },
            {
                dataField: "priority",
                caption: "Priority",
                dataType: "string",
                lookup: {
                    dataSource: ["Low", "Medium", "High", "Critical"],
                    valueExpr: "this",
                    displayExpr: "this"
                },
                validationRules: [{ type: "required" }]
            },
            {
                dataField: "createdAt",
                caption: "Created At",
                dataType: "date",
                format: "yyyy-MM-dd HH:mm:ss",
                width: 150
            },
            {
                dataField: "updatedAt",
                caption: "Updated At",
                dataType: "date",
                format: "yyyy-MM-dd HH:mm:ss",
                width: 150
            },
            {
                dataField: "assignedTo",
                caption: "Assigned To",
                dataType: "string"
            },
            {
                dataField: "reportedBy",
                caption: "Reported By",
                dataType: "string"
            }
        ],

        filterRow: {
            visible: true
        },

        searchPanel: {
            visible: true
        },

        //  grouping is enabled by default
        groupPanel: {
            visible: true
        },

        editing: {
            mode: "popup",  // row = default, cell, batch, form
            allowUpdating: true,
            allowDeleting: true,
            allowAdding: true,
            allowEditing: true
        },

        summary: {
            totalItems: [
                {
                    summaryType: "count",
                    column: "id",
                    displayFormat: "Total Bugs: {0}",
                    alignment: "left"
                },
                {
                    summaryType: "min",
                    column: "createdAt",
                    displayFormat: "Oldest Bug: {0}",
                    alignment: "left"
                },
                {
                    summaryType: "max",
                    column: "updatedAt",
                    displayFormat: "Latest Update: {0}",
                    alignment: "left"
                }
            ]
        }, 

        masterDetail: {
            enabled: true,
            template: function (_, options) {
                const bug = options.data;
                const details = $("<div>").addClass("bug-details");

                const desc = $("<p>").html(`<strong>Description:</strong> ${bug.description}`);
                const status = $("<p>").html(`<strong>Status:</strong> ${bug.status}`);
                const priority = $("<p>").html(`<strong>Priority:</strong> ${bug.priority}`);
                const assignedTo = $("<p>").html(`<strong>Assigned To:</strong> ${bug.assignedTo || "Not Assigned"}`);
                const reportedBy = $("<p>").html(`<strong>Reported By:</strong> ${bug.reportedBy}`);
                const createdAt = $("<p>").html(`<strong>Created At:</strong> ${new Date(bug.createdAt).toLocaleString()}`);
                const updatedAt = $("<p>").html(`<strong>Updated At:</strong> ${new Date(bug.updatedAt).toLocaleString()}`);

                return details.append(desc, status, priority, assignedTo, reportedBy, createdAt, updatedAt);
            }
        },

        columnHidingEnabled: true,

        columnChooser: {
            enabled: true,
            mode: "dragAndDrop"
        },

        headerFilter: {
            visible: true,
            allowSearch: true
        },

        filterPanel: {
            visible: true
        },

        filterSyncEnabled: true,

        filterBuilder: {
            customOperations: [{
                name: "isHighPriority",
                caption: "Is High Priority",
                dataTypes: ["string"],
                hasValue: false,
                calculateFilterExpression: function (filterValue, field) {
                    return [field.dataField, "=", "High"];
                }
            },
            {
                name: "isOpenStatus",
                caption: "Is Open Status",
                dataTypes: ["string"],
                hasValue: false,
                calculateFilterExpression: function (filterValue, field) {
                    return [field.dataField, "=", "Open"];
                }
            }],
            filterValue: [["priority", "isHighPriority"]] 
        },

        filterBuilderPopup: {
            width: 400,
            title: "Synchronized Filter"
        },

        paging: {
            enabled: true,
            pageSize: 5  
        },

        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [2, 5, 10],
            showNavigationButtons: true,
            showInfo: true,
            infoText: "Page: {0} out of {1} ({2} items)"
        },

        selection: {
            mode: "multiple", // none = default, single
            selectAllMode: "page",
            allowSelectAll: true,
            showCheckBoxesMode: "always"
        },

        loadPanel: {
            height: 50,
            width: 50,
            indicatorSrc: "https://js.devexpress.com/Content/data/loadingIcons/rolling.svg"
        },

        export: {
            enabled: true,
            texts: {
                exportAll: "Export All",
                exportSelectedRows: "Export Selected",
                exportTo: "Export"
            },
            allowExportSelectedData: true
        },

        onExporting: function (e) {
            var dataGrid = e.component;

            dataGrid.getDataSource().load().done(function (data) {
                if (data.length === 0) {
                    alert("No data available for export!");
                    return;
                }

                var workbook = new ExcelJS.Workbook();
                var worksheet = workbook.addWorksheet('Bug Data');

                DevExpress.excelExporter.exportDataGrid({
                    worksheet: worksheet,
                    component: dataGrid,
                    selectedRowsOnly: false  // Export all data
                }).then(function () {
                    return workbook.xlsx.writeBuffer();
                }).then(function (buffer) {
                    saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Bug-Tracking-Data.xlsx');
                });

            }).fail(function () {
                alert("Failed to load data for export.");
            });

            e.cancel = true;
        }



    });
    


});
