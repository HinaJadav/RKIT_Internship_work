$(function () {
    // 1) Data Binding Using Array
    const studentData = [
        { id: 1, name: "Aarav Sharma", email: "aarav.sharma@gmail.com", gender: "male", fees: 15000, package: 200000 },
        { id: 2, name: "Priya Iyer", email: "priya.iyer@gmail.com", gender: "female", fees: 18000, package: 900000 },
        { id: 3, name: "Rohan Verma", email: "rohan.verma@gmail.com", gender: "male", fees: 13500, package: 400000 },
        { id: 4, name: "Sneha Nair", email: "sneha.nair@gmail.com", gender: "female", fees: 16000, package: 200000 },
        { id: 5, name: "Vikram Singh", email: "vikram.singh@gmail.com", gender: "male", fees: 14000, package: 300000 }
    ];

    $("#dataGrid1").dxDataGrid({
        dataSource: studentData
    });

    // 2) Data Binding Using AJAX
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

    $("#dataGrid2").dxDataGrid({
        dataSource: dataStore,
        columnFixing: { enabled: true },
        allowColumnReordering: true,
        columnAutoWidth: true,
        autoGenerateColumns: false, // prevent auto-generated columns order 
        columns: [
            { dataField: "id", dataType: "int", width: 50 },
            { dataField: "userId", width: 50, sortOrder: "asc" },
            { dataField: "title", width: 200 },
            { dataField: "body", width: 500, fixed: true }
        ]
    });

});
