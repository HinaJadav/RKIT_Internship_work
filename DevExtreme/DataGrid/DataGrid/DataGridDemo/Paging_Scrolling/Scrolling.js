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
            
        },


        // scrolling

        // note:
        // paging only works with "standard" scrolling
        // virtual and infinite scrolling ignore paging settings
        // "virtual" scrolling option : use for large datasets

        scrolling: {

           
            mode: "infinite",

            // mode: "standard",
            // Renders all rows/columns at once. Better for small datasets.

            // mode: "virtual",
            // Renders only the rows/columns currently in view. Improves performance for large datasets.

            showScrollbar: "onScroll",

            // scrollByThumb: true,

            // scrollByContent: true,

            // rowRenderingMode: "virtual", // Controls how rows are rendered while scrolling.

            // columnRenderingMode: "standard" // Controls how columns are rendered when scrolling horizontally.
        }
    });
})