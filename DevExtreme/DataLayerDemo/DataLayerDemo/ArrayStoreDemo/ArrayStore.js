$(function () {
    var studentsData = [
        { id: 1, name: "ABC", branch: "CE" },
        { id: 2, name: "ZXC", branch: "EC" },
        { id: 3, name: "ERT", branch: "IT" },
        { id: 4, name: "DFG", branch: "IT" },
        { id: 5, name: "IOP", branch: "CE" },
    ];

    var studentArrayStores = new DevExpress.data.ArrayStore({

        // specify the data store accociated with array
        data: studentsData, 

        key: "id",

        // events
        // the function that is executed when the datastore throws an error
        errorHandler: function (error) {
            console.log(error.message);
        },




        

        onInserted: function (e) {
            console.log("New student inserted:", e.data);
        },

        onLoading: function (e) {
            console.log("Before loading, Load Options:", e.loadOptions);

            // Ensure `loadOptions` exists before modifying it
            e.loadOptions = e.loadOptions || {};

            // Apply a default filter 
            e.loadOptions.filter = e.loadOptions.filter || ["branch", "=", "IT"];

            // Sort data 
            e.loadOptions.sort = e.loadOptions.sort || [{ selector: "name", desc: true }];

            // Limit the number of records 
            e.loadOptions.take = e.loadOptions.take || 2;

            // Skip the first record 
            e.loadOptions.skip = e.loadOptions.skip || 1;

            console.log("Modified Load Options:", e.loadOptions);
        },

        onLoaded: function (e) {
            console.log("Data loaded successfully: ", e.data);
        },


        onModifying: function (e) {
            console.log("Modifying Event Triggered:", e);   
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

            /*if (e.data.id === 2) {
                e.cancel = true;
                throw new Error("Update prevented on 2nd id!");
            }*/
        },

        onUpdated: function (e) {
            console.log("Record Successfully Updated:", e);
        },



    });


    // load data
    studentArrayStores.load().done(function (data) {
        console.log("Final Loaded Data:", data);
    }).fail(function (error) {
        throw new Error("Error:", error.message);
    });

    // insert
    // Insert a new record with a unique ID
    studentArrayStores.insert({ id: 22, name: "SSS", branch: "IT" }).done(() => {
        console.log("New record inserted successfully");
    }).fail((error) => {
        console.error("Insert failed:", error.message);
    });


    // update
    studentArrayStores.update(5, { name: "AAA" }).done(() => {
        console.log("Record Updated");
    });

    // delete
    studentArrayStores.remove(2).done(() => {
        console.log("Record Deleted");
    });

    // Simulating real-time data update
    setTimeout(() => {
        if (!studentsData.some(s => s.id === 10)) {
            studentArrayStores.push([{ type: "insert", data: { id: 10, name: "QWE", branch: "IT" } }]);
        }
        studentArrayStores.push([
            { type: "insert", data: { id: 10, name: "QWE", branch: "IT" } }, // Unique ID
            { type: "remove", key: 2 }
        ]);
    }, 2000);


    // Removing students
    studentArrayStores.remove(1).done(() => {
        console.log("Student 1 Removed");
    });

    studentArrayStores.remove(2).done(() => {
        console.log("Student 2 Removed");
    });

    // Updating students
    studentArrayStores.update(1, { name: "LLL" }).done(() => {
        console.log("Student 1 Updated");
    });

    studentArrayStores.update(2, { name: "ZXC" }).done(() => {
        console.log("Student 2 Updated");
    });


    // methods

    // byKey(key) : gets data with a specific key
    studentArrayStores.byKey(1).done(function (data) {
        console.log("Data fetch byKey(1): ", data);
    }).fail(function (error) {
        console.log("Error fetching data by key:", error.message);
    });



    // createQuery(): in "QueryDemo"

    // key() : get key property
    
    var studentArrayStoreKey = studentArrayStores.key();
    console.log("Key property of student data store: ", studentArrayStoreKey);

    // Find key based on data object
    var dataKey = studentArrayStores.keyOf({ id: 2, name: "ZXC", branch: "EC" });
    console.log("Key of given data store: ", dataKey);


    // load() : load entire arraystore data without apply any filter etc.
    studentArrayStores.load().done(function (data) {
        console.log("Loaded data: ", data);
    });

    // totalCount(): get the total count of items returns from load() function
    var totalLoadedItems = studentArrayStores.totalCount().done(() => {
        console.log("Number of data loaded: ", totalLoadedItems);
    }).fail(() => {
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

    function onUpdating(e) {
        console.log("Updating event triggered:", e);
    }

    studentArrayStores.on("updating", onUpdating);

    // it will work because event is on
    studentArrayStores.update(2, { name: "CCC" }).done(() => {
        console.log("Student 2 Updated");
    });

    studentArrayStores.off("updating", onUpdating);

    // not work because event is off now
    studentArrayStores.update(2, { name: "EEE" }).done(() => {
        console.log("Student 2 Updated");
    });


    
    // clear(): Clears all the ArrayStore's associated data.
    // studentArrayStores.clear();

});