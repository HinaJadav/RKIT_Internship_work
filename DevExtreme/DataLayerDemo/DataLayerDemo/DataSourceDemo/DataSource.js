$(function () {
    var apiUrl = "https://jsonplaceholder.typicode.com/users";

    var customStore = new DevExpress.data.CustomStore({
        loadMode: "raw", // Specifies how data is loaded. Possible values: "raw" (directly from source), "processed" (processed by DevExtreme).
        load: function () {
            return fetch(apiUrl)
                .then(response => response.json())
                .catch(error => {
                    console.log("Error loading data into customStore: ", error);
                });
        }
    });

    var dataSource = new DevExpress.data.DataSource({
        store: customStore, // Specifies the store that provides data. Can be CustomStore, ArrayStore, or LocalStore.

        // Filtering data to exclude users with the name "Leanne Graham".
        // Filters can have multiple formats:
        // Single condition: ["field", "operator", "value"] -> Example: ["name", "=", "John"]
        // Logical NOT: ["!", ["name", "=", "John"]] -> Excludes users with the name "John"
        // Multiple conditions: [["name", "=", "John"], "or", ["city", "=", "New York"]]
        filter: ["!", ["name", "=", "Leanne Graham"]],

        // Groups the data by the city field inside the address object.
        // The group option can take:
        // 1. A string specifying the field to group by. Example: "address.city"
        // 2. An array with multiple fields. Example: [{ selector: "city", desc: true }]
        group: "address.city",

        // The map function is used to transform or modify each data item before passing it to UI components.
        // This is useful for renaming fields, formatting values, or extracting nested properties.
        map: function (item) {
            return {
                id: item.id, // Keeps the original ID
                name: item.username + " : " + item.name // Combines username and name into a single field
            };
        },

        // Event triggered when the data source changes, such as when new data is loaded.
        onChanged: function (e) {
            console.log("Selected user value is changed: ", e);
        },

        // Event triggered when there is an error loading the data.
        onLoadError: function (error) {
            console.log("Error: ", error.message);
        },

        // Event triggered when the loading state changes (for example, when loading starts or finishes).
        onLoadingChanged: function (changes) {
            console.log("Alert changes done during loading: ", changes);
        },

        // Enables pagination, meaning data will be loaded in smaller chunks rather than all at once.
        paginate: true,

        // Specifies the number of items per page when pagination is enabled.
        pageSize: 4,

        // Defines which fields should be used for searching when a search query is entered.
        // This can be a string (single field) or an array (multiple fields).
        searchExpr: ["username"],

        // Ensures that the total count of records is available when using pagination.
        requiredTotalCount: true,

        // Determines whether data should be reshaped when push changes are made.
        // If set to true, the UI updates automatically when data changes.
        reshapeOnPush: true,

        // Defines the search operation. Possible values:
        // "contains", "startswith", "endswith", "=" (exact match)
        searchOperation: "startswith",

        // Defines sorting behavior.
        // Can be a single string field name or an array for multiple sorting options.
        // Example:
        // sort: ["name"] -> Sorts by name in ascending order
        // sort: [{ selector: "username", desc: true }] -> Sorts username in descending order
        sort: [
            "position",
            {
                selector: "username",
                desc: true
            }
        ],
    });

    // Load data initially and log it to the console.
    dataSource.load().then(data => console.log(data));

    // Create a SelectBox UI component with data from the DataSource.
    $("#selectBox").dxSelectBox({
        width: "20rem",
        height: "3rem",
        dataSource: dataSource, // Uses the DataSource for dynamic data
        displayExpr: "name", // Specifies which field to display in the dropdown
        valueExpr: "id", // Specifies the unique field used for selection
        searchEnabled: true, // Enables search functionality in the dropdown
        grouped: true // Enables grouping based on the group option in DataSource
    });

    // Create a button to insert new data into the DataSource.
    $("#insertButton").dxButton({
        text: "Add data",
        onClick: function () {
            // Push method is used to insert new data into the store.
            // The object format should include a unique key and data.
            dataSource.store().push([{
                type: "insert",
                key: 12, // Unique identifier for the new data
                data: {
                    "id": 12,
                    "name": "Antonette : Ervin Howell", // Matches the mapped format
                    "username": "Antonette",
                    "email": "Shanna@melissa.tv",
                    "address": {
                        "street": "Victor Plains",
                        "suite": "Suite 879",
                        "city": "Wisokyburgh",
                        "zipcode": "90566-7771",
                        "geo": {
                            "lat": "-43.9509",
                            "lng": "-34.4618"
                        }
                    },
                    "phone": "010-692-6593 x09125",
                    "website": "anastasia.net",
                    "company": {
                        "name": "Deckow-Crist",
                        "catchPhrase": "Proactive didactic contingency",
                        "bs": "synergize scalable supply-chains"
                    }
                }
            }]);

            // Reloads the data source to reflect changes in the UI.
            dataSource.load().then(() => {
                console.log("New data added and reloaded.");
            });
        }
    });

    // Delay execution for 2 seconds before checking DataSource properties and changing pages.
    setTimeout(function () {
        // Logging different DataSource methods to check current state.
        console.log("Filters: ", dataSource.filter()); // Returns the current applied filters
        console.log("Is last page? ", dataSource.isLastPage()); // Returns true if the last page is loaded
        console.log("Is loaded? ", dataSource.isLoaded()); // Returns true if data is fully loaded
        console.log("Is loading? ", dataSource.isLoading()); // Returns true if data is still loading
        console.log("Items: ", dataSource.items()); // Returns the items currently loaded in the DataSource
        console.log("Keys: ", dataSource.key()); // Returns the key field used in the DataSource

        // Change to page index 3 (page indexes are 0-based).
        // This will only work if pagination is enabled.
        if (dataSource.paginate()) {
            dataSource.pageIndex(3);
            dataSource.load().then(() => {
                console.log("Page changed to: ", dataSource.pageIndex()); // Logs the new page index
                console.log("Current Items: ", dataSource.items()); // Logs the items on the current page
            });
        } else {
            console.log("Pagination is disabled.");
        }

        // Logs whether pagination is enabled.
        console.log("Pagination enabled? ", dataSource.paginate());

        // Logs the total number of items in the dataset.
        console.log("Total Count: ", dataSource.totalCount());
    }, 2000);
});
