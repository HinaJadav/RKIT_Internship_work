
$(function () {
  

    $("#dataGrid").dxDataGrid({
        dataSource: users,
        columnFixing: { enabled: true },
        allowColumnReordering: true,
        columnAutoWidth: true,
        rowAlternationEnabled: true,
        showBorders: true,
       
        editing: {
            mode: "row",
            allowUpdating: true,
            allowAdding: true,
            allowDeleting: true,
        },


        // Cascading LookUp

        columns: [
            { dataField: "ID", caption: "ID", allowEditing: false },
            { dataField: "Name", caption: "Name" },
            { dataField: "Email", caption: "Email" },
            { dataField: "PhoneNumber", caption: "Phone Number" },

            // Country Lookup
            {
                dataField: "CountryID",
                caption: "Country",
                lookup: {
                    dataSource: countries,
                    valueExpr: "ID",
                    displayExpr: "Name"
                },
                validationRules: [{
                    type: "required"
                }]
            },

            // State lookUp with filtering based on selected Country
            {
                dataField: "StateID",
                caption: "State",
                lookup: {
                    dataSource: function (options) {
                        return {
                            store: states,
                            filter: options.data ? ["CountryID", "=", options.data.CountryID] : null
                        };
                    },
                    valueExpr: "ID",
                    displayExpr: "Name"
                },
                validationRules: [{
                    type: "required"
                }]
            }
        ],

        // Event Handling
        onRowUpdating: function (e) {
            if ("CountryID" in e.newData) {
                e.newData.StateID = null; // Reset state if country changes
            }
        }
    });
});


const users = [
    {
        ID: 1,
        Name: 'Amit Sharma',
        Email: 'amit.sharma@example.com',
        PhoneNumber: '+91-98765-43210',
        CountryID: 1,
        StateID: 1,
    },
    {
        ID: 2,
        Name: 'Neha Verma',
        Email: 'neha.verma@example.com',
        PhoneNumber: '+91-87654-32109',
        CountryID: 1,
        StateID: 2,
    },
    {
        ID: 3,
        Name: 'Rohan Gupta',
        Email: 'rohan.gupta@example.com',
        PhoneNumber: '+91-76543-21098',
        CountryID: 1,
        StateID: 3,
    },
    {
        ID: 4,
        Name: 'Emily Brown',
        Email: 'emily.brown@example.com',
        PhoneNumber: '+1-234-567-8901',
        CountryID: 2,
        StateID: 6,
    },
    {
        ID: 5,
        Name: 'Michael Johnson',
        Email: 'michael.johnson@example.com',
        PhoneNumber: '+1-345-678-9012',
        CountryID: 2,
        StateID: 7,
    },
    {
        ID: 6,
        Name: 'Sophia Wilson',
        Email: 'sophia.wilson@example.com',
        PhoneNumber: '+44-7890-123456',
        CountryID: 3,
        StateID: 10,
    },
    {
        ID: 7,
        Name: 'Lucas Schmidt',
        Email: 'lucas.schmidt@example.com',
        PhoneNumber: '+49-1520-9876543',
        CountryID: 4,
        StateID: 12,
    },
    {
        ID: 8,
        Name: 'William Taylor',
        Email: 'william.taylor@example.com',
        PhoneNumber: '+61-423-678-901',
        CountryID: 5,
        StateID: 14,
    },
];

const countries = [
    { ID: 1, Name: 'India' },
    { ID: 2, Name: 'United States' },
    { ID: 3, Name: 'United Kingdom' },
    { ID: 4, Name: 'Germany' },
    { ID: 5, Name: 'Australia' },
];

const states = [
    // India
    { ID: 1, Name: 'Maharashtra', CountryID: 1 },
    { ID: 2, Name: 'Karnataka', CountryID: 1 },
    { ID: 3, Name: 'Delhi', CountryID: 1 },
    { ID: 4, Name: 'Tamil Nadu', CountryID: 1 },
    { ID: 5, Name: 'Gujarat', CountryID: 1 },

    // United States
    { ID: 6, Name: 'California', CountryID: 2 },
    { ID: 7, Name: 'Texas', CountryID: 2 },
    { ID: 8, Name: 'New York', CountryID: 2 },

    // United Kingdom
    { ID: 9, Name: 'England', CountryID: 3 },
    { ID: 10, Name: 'Scotland', CountryID: 3 },
    { ID: 11, Name: 'Wales', CountryID: 3 },

    // Germany
    { ID: 12, Name: 'Bavaria', CountryID: 4 },
    { ID: 13, Name: 'Berlin', CountryID: 4 },

    // Australia
    { ID: 14, Name: 'New South Wales', CountryID: 5 },
    { ID: 15, Name: 'Victoria', CountryID: 5 },
];

