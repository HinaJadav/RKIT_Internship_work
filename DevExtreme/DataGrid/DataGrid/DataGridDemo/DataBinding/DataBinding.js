$(function () {

    // 1) data binding using : Array

    const studentData = [
        { id: 1, name: "Aarav Sharma", email: "aarav.sharma@gmail.com", gender: "male", fees: 15000, package: 200000 },
        { id: 2, name: "Priya Iyer", email: "priya.iyer@gmail.com", gender: "female", fees: 18000, package: 900000 },
        { id: 3, name: "Rohan Verma", email: "rohan.verma@gmail.com", gender: "male", fees: 13500, package: 400000 },
        { id: 4, name: "Sneha Nair", email: "sneha.nair@gmail.com", gender: "female", fees: 16000, package: 200000 },
        { id: 5, name: "Vikram Singh", email: "vikram.singh@gmail.com", gender: "male", fees: 14000, package: 300000 }
    ];

    
    $("#dataGrid1").dxDataGrid({
        dataSource: studentData,
        keyExpr: "id"
    });

    // 2) data binding using : Ajex

    // data api url 
    const apiUrl = "https://reqres.in/api/users";

    
    let dataStore = new DevExpress.data.CustomStore({
        key: "id",
        load: function () {
            return $.ajax({ 
                url: apiUrl,
                method: "GET",
                dataType: "json"
            }).then(response => response.data); // extract "data" from API response
        }
    });

    $("#dataGrid2").dxDataGrid({
        dataSource: dataStore,
        keyExpr: "id",
        columnFixing: {
            enabled: true,
        },// when column contains large data then user can see entire data by horizontal scrolling 

        columns: [{
            dataField: "Id",
            dataType: "int",
            width: 50
        }, {
            dataField: "userId",
            width: 50,
            }, {
            dataField: "title",
            width: 100,

            }, {
            dataField: "body",
            width: 200,
            fixed: true,
            // visible : false // if we want to hide data 
            }],

        allowColumnReordering: true,
        columnAutoWidth: true, // allow manage column width because init it's default = equal



    });

});
