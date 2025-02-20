$(document).ready(function () {
    var studentsData = [
        { id: 1, name: "ABC", branch: "CE" },
        { id: 2, name: "ZXC", branch: "EC" },
        { id: 3, name: "ERT", branch: "IT" },
        { id: 4, name: "DFG", branch: "IT" },
        { id: 5, name: "IOP", branch: "CE" },
    ];

    var studentArrayStores = new DevExpress.data.ArrayStore({
        key: "id",
        data: studentsData,

        // events

        errorHandler: function (error) {
            console.log(error.message);
        },




        onInserting: function (e) {
            // Get existing data from the store
            var existingData = studentArrayStores._array;

            // Ensure Required Fields are Present
            if (!e.data.name || !e.data.branch) {
                e.cancel = true;
                throw new Error("Name and Branch are required fields!");
            }

            // Auto-generate ID if not provided
            if (!e.data.id) {
                e.data.id = existingData.length ? Math.max(...existingData.map(s => s.id)) + 1 : 1;
            }

            // Normalize Name Format
            e.data.name = e.data.name.trim().toUpperCase();
        },


        onInserted: function (e) {
            console.log("New student inserted:", e.data);
        },

        onLoading: function (e) {
            console.log("Before loading, Load Options:", e.loadOptions);

            // Apply a default filter 
            if (!e.loadOptions.filter) {
                e.loadOptions.filter = ["branch", "=", "IT"];
            }

            // Sort data 
            if (!e.loadOptions.sort) {
                e.loadOptions.sort = [{ selector: "name", desc: true }];
            }

            // Limit the number of records 
            if (!e.loadOptions.take) {
                e.loadOptions.take = 2;
            }

            // Skip first record 
            if (!e.loadOptions.skip) {
                e.loadOptions.skip = 1;
            }

            console.log("Modified Load Options:", e.loadOptions);
        },

        onLoaded: function (e) {
            console.log("Data loaded successfully: ", e.data);
        },


        onModifying: function (e) {
            console.log("Modifying Event Triggered:", e);

            if (e.type === "update") {
                console.log("Updating Record:", e.key, "With Data:", e.data);
            }
            else if (e.type === "insert") {
                console.log("Inserting New Record:", e.data);
            }
            else if (e.type === "remove") {
                console.log("Deleting Record:", e.key);
            }
        },

        onModified: function (e) {
            console.log("Modification Completed:", e);
        },

        onPush: function (e) {
            console.log("Student Data Pushed:", e.data);
        },

        onRemoving: function (e) {
            console.log("Before Removing Record:", e);

            
            if (e.key === 1) {
                e.cancel = true;
                throw new Error("Deletion prevented for ID:", e.key);
            }
        },

        onRemoved: function (e) {
            console.log("Record Successfully Removed:", e);
        },

        onUpdating: function (e) {
            console.log("Before Updating Record:", e);

            if (e.data.id === 2) {
                e.cancel = true;
                throw new Error("Update prevented on 2nd id!");
            }
        },

        onUpdated: function (e) {
            console.log("Record Successfully Updated:", e);
        },



    });


    // load data
    studentStore.load().done(function (data) {
        console.log("Final Loaded Data:", data);
    }).fail(function (error) {
        throw new Error("Error:", error.message);
    });

    // insert
    studentStore.insert({ id: 3, name: "ERT", branch: "IT" }).done(() => {
        console.log("Record Inserted");
    });

    // update
    studentStore.update(5, { name: "AAA" }).done(() => {
        console.log("Record Updated");
    });

    // delete
    studentStore.remove(2).done(() => {
        console.log("Record Deleted");
    });

    // Simulating real-time data update
    setTimeout(() => {
        studentStore.push([
            { type: "insert", data: { id: 3, name: "QWE", branch: "IT" } },
            { type: "remove", key: 2 }
        ]);
    }, 2000);

    // Removing students
    studentStore.remove(1).done(() => {
        console.log("Student 1 Removed");
    }).

    studentStore.remove(2).done(() => {
        console.log("Student 2 Removed");
    });

    // Updating students
    studentStore.update(1, { name: "LLL" }).done(() => {
        console.log("Student 1 Updated");
    });

    studentStore.update(2, { name: "ZXC" }).done(() => {
        console.log("Student 2 Updated");
    });


    // methods

    // byKey(key) : gets data with a specific key
    studentArrayStores.byKey(1).done(function (data) {
        console.log("Data fetch byKey(1): ", data);
    }).fail(function (error) {
        throw new Error("Error: ", error.message);
    });


    // createQuery(): in "QueryDemo"

    // key() : get key property
    var studentArrayStoreKey = studentArrayStores.key();
    console.log("Key property of student data store: ", studentArrayStoreKey);

    // keyOf(obj): find key based on data value
    var dataKey = studentArrayStoreKey.keyOf({ name: "ABC", branch: "CE" })
    console.log("Key of given data store: ", dataKey);

    // load() : load entire arraystore data without apply any filter etc.
    studentArrayStores.load().done(function (data) {
        console.log("Loaded data: ", data);
    });

    // totalCount(): get the total count of items returns from load() function
    var totalLoadedItems = studentArrayStores.totalCount().done(() => {
        console.log("Number of data loaded: ", totalLoadedItems);
    }).faile(() => {
        console.log("Error : totalCount() method has some error.");
    })



    // load(options) : loads data with custom conditions
    studentArrayStores.load({
        select: ["name", "branch"],
        take: 3
    }).done(function (data) {
        console.log("Load data with custom conditions: ", data);
    });

    // on() and off() : use to trigger events dynamically

    studentArrayStores.on("updating", onUpdating);

    // it will work because event is on
    studentStore.update(2, { name: "CCC" }).done(() => {
        console.log("Student 2 Updated");
    });

    studentArrayStores.off("updating", onUpdating);

    // not work because event is off now
    studentStore.update(2, { name: "EEE" }).done(() => {
        console.log("Student 2 Updated");
    });


    
    // clear(): Clears all the ArrayStore's associated data.
    // studentArrayStores.clear();

});