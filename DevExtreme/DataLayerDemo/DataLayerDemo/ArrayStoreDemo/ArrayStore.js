// arrayStore: all data is loaded at once into memory

$(function () {

    var studentData = [

        { id: 1, name: "ABC", branch: "CE" },

        { id: 2, name: "ZXC", branch: "EC" },

        { id: 3, name: "ERT", branch: "IT" },

        { id: 4, name: "DFG", branch: "IT" },

        { id: 5, name: "IOP", branch: "CE" },

    ];

    // method 1: Using dataSource with arrayStore

    // adv:

    // Supports sorting, filtering, searching, paging.

    // works well with UI components (ex: for dataGrid we need filtering, sorting, Paging, this functionality provided by it so it is more effective)

    // declare arrayStore inside "DataStore"

    // Use:  when using DevExtreme UI components (like dxDataGrid, dxList, dxSelectBox) and need filtering, sorting, or paging.

    var dataStore = new DevExpress.data.DataSource({

        store: {

            type: "array", // arrayStore data binding type we mention here 

            key: "id",

            data: studentData,

        }

    });

    // fetch data from dataStore

    dataStore.load().done(function (result) {

        console.log("Data from DataSource:", result);

    }).fail(function (error) {

        console.error("Error loading DataSource:", error);

    });




    // method 2: Using ArrayStore directly

    // adv:

    // 1) simple and fast (no need to use .load())

    // 2) entire data available immediatly

    // use:  need to store & fetch data manually.

    var studentArrayStores = new DevExpress.data.ArrayStore({

        key: "id",

        data: studentData,

        errorHandler: function (error) {

            console.log(error.message);

        },

        onInserted: function (e) {

            console.log("New student inserted:", e.data);

        },

        onLoading: function (e) {

            console.log("Before loading, Load Options:", e.loadOptions);

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

        },

        onUpdated: function (e) {

            console.log("Record Successfully Updated:", e);

        },

    });


    // ----------------------------------------------------------------------

    // methods:

    // byKey(key) : gets data with a specific key

    // fetch data with id = 1

    studentArrayStores.byKey(1).done(function (data) {

        console.log("Data fetch byKey(1): ", data);

    }).fail(function (error) {

        console.log("Error fetching data by key:", error.message);

    });


    // clear(): Clears all the ArrayStore's associated data.

    //studentArrayStores.clear();

    //console.log("Data load after clear:", studentArrayStores);


    // createQuery():

    // create a Query from ArrayStore

    // use: on ArrayStore direclty we can't apply filter sorting like operation so using it we can apply those operations on arrayStore also

    var query = studentArrayStores.createQuery();

    // apply filtering (Get students from IT branch)

    query = query.filter(["branch", "=", "IT"]);

    // execute the query to get the data

    query.enumerate().done(function (result) {

        console.log("Query data:", result);

    }).fail(function (error) {

        console.error("Query Error:", error);

    });

    // insert()

    // insert a new record

    // note : "The data item's key value should be unique, otherwise, the insertion will fail." 

    studentArrayStores.insert({ id: 22, name: "SSS", branch: "IT" }).done(() => {

        console.log("New record inserted successfully");

    }).fail((error) => {

        console.error("Insert failed:", error.message);

    });

    // key() : get key property

    var studentArrayStoreKey = studentArrayStores.key();

    console.log("Key property of student data store: ", studentArrayStoreKey);

    // keyOf(): Find key based on data object

    var dataKey = studentArrayStores.keyOf({ id: 2, name: "ZXC", branch: "EC" });

    console.log("Key of given data store: ", dataKey);

    // load() : load entire arraystore data 

    studentArrayStores.load().done(function (data) {

        console.log("Loaded data: ", data);

    });

    // totalCount(): get the total count of items returns from load() function

    // Note: it is not useful to use with load() method into arrayStore

    // because: all data stores locally



    // load(options) : loads data with custom conditions

    studentArrayStores.load({

        take: 2, // Fetch 2 records

        skip: 1  // Skip the first record

    }).done(function (data) {

        console.log("Load data with custom conditions:", data);

    }).fail(function (error) {

        console.error("Load Error:", error);

    });

    // arrayStore not support "select" like options during load()

    // update() : update data with specific single key (also with composite key if mention in ArrayStore "Key: []")

    studentArrayStores.update(5, { name: "AAA" }).done(() => {

        console.log("Record Updated");

    });

    // remove() : remove data based on key value declare in ArrayStore

    /* studentArrayStores.remove(2).done(() => {
 
         console.log("Record Deleted");
 
     });*/

    // Simulating real-time data update

    studentArrayStores.push([

        { type: "insert", data: { id: 10, name: "QWE", branch: "IT" } },

        { type: "remove", key: 2 }

        // other type: "update"

    ]);

    studentArrayStores.load().done(function (data) {

        console.log("Loaded data: ", data);

    });

    // ----------------------------------------------------------------------------------------------

    function onUpdating(e) {

        console.log("Updating event triggered:", e);

    }

    studentArrayStores.on("updating", onUpdating);

    studentArrayStores.update(3, { name: "CCC" }).done(() => {

        console.log("Student 3 Updated");

    });

    studentArrayStores.off("updating", onUpdating);



});
