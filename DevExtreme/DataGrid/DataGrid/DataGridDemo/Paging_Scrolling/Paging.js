$(function () {
    const apiUrl = "https://jsonplaceholder.typicode.com/posts";


    let dataStore = new DevExpress.data.CustomStore({
        load: function () {
            return $.ajax({
                url: apiUrl,
                method: "GET",
                dataType: "json"
            })
                .then(response => response) // Extract "data" from API response
                .fail(error => console.error("Error loading data:", error));
        }
    });

    $("#dataGrid").dxDataGrid({
        dataSource: dataStore,
        columnFixing: { enabled: true },
        allowColumnReordering: true,
        columnAutoWidth: true,
        autoGenerateColumns: false, // prevent auto-generated columns order 
        rowAlternationEnabled: true,
        showBorders: true,
        columns: [
            { dataField: "id", dataType: "int", width: 50 },
            { dataField: "userId", width: 50, sortOrder: "asc" },
            { dataField: "title", width: 200 },
            { dataField: "body", width: 500, fixed: true }
        ],


        // Paging

        
        paging: {
            pageSize: 8,
            // pageIndex: 1 // make easy and effective searching
        },

        pager: {
            visible: true, 
            allowedPageSizes: [5, 10, 'all'], // explain
            displayMode: "full",
            // infoText: // how to use it and which type input need to insert
            label: "Page Navigation", // aria label attribute for pager
            showInfo: true, // it display total no. of items and page no. start to end range

            showNavigationButtons: true, // show those back and forword arrows which use for page navigation

            showPageSizeSelector: true, 
            // it shows different page sizes which we write into "allowedPageSizes" and based on selected page size DataGrid UI is affected
        },


       
    });
})