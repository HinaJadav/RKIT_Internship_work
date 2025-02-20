$(function () {
    var apiUrl = "https://jsonplaceholder.typicode.com/users";

    var customStore = new DevExpress.data.CustomStore({
        loadMode: "raw",

        load: function () {
            return fetch(apiUrl).then(response => response.json()).catch(error => {
                console.log("Error into data load into customStore: ", error);
            })
        }
    });

    var dataSource = new DevExpress.data.DataSource({
        type: "custom",
        store: customStore,

        // Unary filter
        filter: ["!", ["name", "=", "Leanne Graham"]],

        // make group by : city
    })
})
// option:

// group ?
// map
// onChanged
// onLoadError
// onLoadingChanged
// pageSize
// paginate
// Post Processing
// pushAggregationTimeout
// reshapeOnPush
// store

// methods:
/*
cancel
IsLastPage
Isloaded
IsLoading
items

// events

pageIndex
pageSize

paginate
reload
loadError


*/
