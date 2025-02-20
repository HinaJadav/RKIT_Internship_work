$(function () {
   
    // The CustomStore enables you to implement custom data access logic for consuming data from any source.
    // it's implemetation depends on whether data is processed on the client or server.

    var apiUrl = "https://jsonplaceholder.typicode.com/users";

    let customStore = new DevExpress.data.CustomStore({
        key: "id",

        loadMode: "raw", // other loadMode = "processed"

        load: function (loadOptions) {
            console.log("Load process is started.");

            return fetch(apiUrl)
                .then(response => response.json())
                .then(data => {
                    console.log("Data loaded:", data);

                    if (!Array.isArray(data)) {
                        throw new Error("Expected an array but received something else");
                    }

                    // Apply filter
                    if (loadOptions.filter) {
                        let [field, operator, value] = loadOptions.filter;

                        data = data.filter(item => item[field]?.toString().toLowerCase().includes(value.toLowerCase()));
                    }

                    // Apply search filter
                    if (loadOptions.searchValue && loadOptions.searchExpr) {
                        let field = loadOptions.searchExpr;
                        let value = loadOptions.searchValue.toLowerCase();

                        data = data.filter(item => {
                            let feildValue = item[field]?.toString().toLowerCase();

                            let value = loadOption.searchValue.toLowerCase();

                            data = data.filter(item => {
                                let fieldValue = item[field]?.toString().toLowerCase();

                                if (!feildValue) {
                                    return false;
                                }

                                switch (loadOptions.searchOperation) {
                                    case "contains":
                                        return fieldValue.includes(value);
                                    case "startswith":
                                        return fieldValue.startsWith(value);;
                                    case "=":
                                        return feildValue === value;
                                    default:
                                        return true;
                                }
                            });
                        })

                    }



                    return data; // Must return an array
                })
                .catch(error => {
                    console.error("Data loading error:", error);
                    throw "Data loading error";
                });
        },

        byKey: function (key) {
            console.log("Fetching data for key:", key);

            return fetch(`${apiUrl}/${key}`)
                .then(response => response.json())
                .then(data => {
                    console.log("Data fetched for key:", key, data);
                    return data;
                })
                .catch(error => {
                    console.error("Error fetching data by key:", error);
                    throw "Error fetching data by key";
                });
        },

        insert: function (values) {
            console.log("Inserting data:", values);

            return fetch(apiUrl, {
                method: "POST",
                header: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(values)
            })
                .then(response => response.json())
                .then(data => {
                    console.log("Inserted data:", data);
                    return data;
                })
                .catch(error => {
                    console.log(error.message);
                });
        },

        // group --> use into final demo (with dataGrid)
        // grouping and group summary to organize data and calculate aggregated values.
        // enable grouping and summaries in your existing setup.

        // The parentIds option in DevExtreme's CustomStore is primarily used in hierarchical data structures, such as when implementing tree-like data (e.g., categories and subcategories).

        // requireGroupCount : Returns the total number of groups when grouping is applied.

        // requireTotalCount : Returns the total number of records (before applying pagination or filters).

        // searchExpr : Defines which field(s) should be searched
        // searchOperation  : ("contains", "=", ">")
        // searchValue : The actual value being searched.

        // options: select, skip, sort, take --> into Query section

        // totalSummary : with dataGrid

        // userData: this option in DevExtreme CustomStore allows you to return custom metadata along with the fetched data. It can be used to store additional information that is not part of the main dataset but is still useful for the UI.
    });

    $("#selectBox").dxSelectBox({
        dataSource: customStore,
        valueExpr: "id",
        displayExpr: "name",
        searchEnabled: true,
        searchExpr: "name",
        searchOperation: "contains",
        placeholder: "Select a user...",
        onValueChanged: function (e) {
            let selectedKey = e.value;

            if (selectedKey) {
                customStore.byKey(selectedKey).then(data => {
                    console.log("Selected User Details:", data);
                });
            }
        }
    });

    // search funatinality --> not working 
    $("#searchOperation").dxSelectBox({
        dataSource: ["contains", "startswith", "="],
        value: "contains",
        placeholder: "Select search operation."
    });

    $("#searchButton").dxButton({
        text: "search",
        onClick: function () {
            let operation = $("#searchOperation").dxSelectBox("instance").option("value");

            $("#selectBox").dxSelectBox("instance").getDataSource().load({
                searchExpr: "name",
                searchOperation: operation,
                searchValue: "Leanne"  // Example search value
            });
        }
    });

    $("#insertButton").dxButton({
        text: "Add User",
        onClick: function () {
            let newUser = {
                "id": 10,
                "name": "Clementina DuBuque",
                "username": "Moriah.Stanton",
                "email": "Rey.Padberg@karina.biz",
                "address": {
                    "street": "Kattie Turnpike",
                    "suite": "Suite 198",
                    "city": "Lebsackbury",
                    "zipcode": "31428-2261",
                    "geo": {
                        "lat": "-38.2386",
                        "lng": "57.2232"
                    }
                },
                "phone": "024-648-3804",
                "website": "ambrose.net",
                "company": {
                    "name": "Hoeger LLC",
                    "catchPhrase": "Centralized empowering task-force",
                    "bs": "target end-to-end models"
                }
            };

            customStore.insert(newUser).then(data => {
                console.log("New User Added:", data);
            })

        }
    });

/*
    Binary filter :
    Supported operators: "=", "<>", ">", ">=", "<", "<=", "startswith", "endswith", "contains", "notcontains".

    Unary filter
    Supported operators: binary operators, "!".

    Complex filter
    Supported operators: binary and unary operators, "and", "or".

*/
    $("#filterButton").dxButton({
        text: "Filter Users (Contains 'a')",
        onClick: function () {          
            customStore.load({ filter: ["name", "contains", "a"] }).then(data => console.log("Filtered Users:", data));
        }
    })
});