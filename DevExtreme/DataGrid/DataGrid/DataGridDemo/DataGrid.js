$(function () {

    const studentData = [
        { id: 1, name: "Aarav Sharma", email: "aarav.sharma@gmail.com", gender: "male", fees: 15000, package: 200000 },
        { id: 2, name: "Priya Iyer", email: "priya.iyer@gmail.com", gender: "female", fees: 18000, package: 900000 },
        { id: 3, name: "Rohan Verma", email: "rohan.verma@gmail.com", gender: "male", fees: 13500, package: 400000 },
        { id: 4, name: "Sneha Nair", email: "sneha.nair@gmail.com", gender: "female", fees: 16000, package: 200000 },
        { id: 5, name: "Vikram Singh", email: "vikram.singh@gmail.com", gender: "male", fees: 14000, package: 300000 }
    ];


    const customColums = [

    ]
    $("#dataGrid").dxDataGrid({
        dataSource: studentData,
        keyExpr: "id",

    })
})