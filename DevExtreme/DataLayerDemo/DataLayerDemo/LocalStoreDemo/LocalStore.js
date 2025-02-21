$(function () {
    // Define a key for storing data in localStorage
    const localStorageKey = "userLocalStorageKey";

    // Retrieve data from localStorage, if available, otherwise use default user data
    var userData = localStorage.getItem(localStorageKey);
    var users = userData ? JSON.parse(userData) : [
        { id: 1, name: "Amit Sharma", email: "amit.sharma@example.com", city: "Ahmedabad" },
        { id: 2, name: "Priya Verma", email: "priya.verma@example.com", city: "Bengaluru" },
        { id: 3, name: "Rahul Mehta", email: "rahul.mehta@example.com", city: "Ahmedabad" },
        { id: 4, name: "Sneha Iyer", email: "sneha.iyer@example.com", city: "Bengaluru" },
        { id: 5, name: "Vikram Rao", email: "vikram.rao@example.com", city: "Hyderabad" },
    ];

    // Create a LocalStore with options for handling data persistence
    var localStore = new DevExpress.data.LocalStore({
        key: "id",                 // Unique key to identify records
        name: localStorageKey,      // Storage key in localStorage
        data: users,                // Initial dataset
        immediate: true,            // Ensures changes are applied immediately

        errorHandler: function (error) {
            console.error("Error in LocalStore:", error);
        },

        flushInterval: 5000  // Flush data to localStorage every 5 seconds if needed
    });

    // Create a DataSource using the LocalStore
    var dataStore = new DevExpress.data.DataSource({
        store: localStore,
        group: "city"  // Corrected grouping by city
    });

    // Initialize dxSelectBox with the data from dataStore
    $("#selectBox").dxSelectBox({
        width: "20rem",
        height: "3rem",
        dataSource: dataStore,
        displayExpr: "name",  // Display the name in the dropdown
        valueExpr: "id",      // Use ID as the value
        searchEnabled: true,  // Allow searching
        grouped: true         // Group data by city
    });

    // Button to insert a new user
    $("#addButton").dxButton({
        text: "Add User",
        onClick: function () {
            var newUser = { id: 6, name: "Jay Sharma", email: "jay.sharma@example.com", city: "Ahmedabad" };

            localStore.insert(newUser).done(() => {
                dataStore.load(); // Refresh the UI

                // Save updated data to localStorage
                localStore.load().done((data) => {
                    localStorage.setItem(localStorageKey, JSON.stringify(data));
                });
            });
        }
    });

    // Button to delete a user by ID (Example: Remove user with ID = 3)
    $("#deleteButton").dxButton({
        text: "Delete User",
        onClick: function () {
            var userIdToDelete = 3;

            localStore.remove(userIdToDelete).done(() => {
                dataStore.load(); // Refresh the UI

                // Save updated data to localStorage
                localStore.load().done((data) => {
                    localStorage.setItem(localStorageKey, JSON.stringify(data));
                });
            });
        }
    });
});
