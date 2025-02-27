import { users } from '../userData.js';

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

        stateStoring: {
            enabled: true, // enables state storing
            type: "sessionStorage", // type of storage (localStorage, sessionStorage, custom)
          

            // type: "custom", // where data will be stored

            storageKey: "userDatagridState", // uniquely identify the grid's state in storage
            savingTimeout: 2000,// Delay before saving state

            // use to load state from storage
            /* customLoad: function () {
                return sendStorageRequest("storageKey", "json", "GET");
            },*/

            // save the state of storage
            /*customSave: function (state) {
                sendStorageRequest("storageKey", "text", "PUT", state);
            }*/
        },
       
    });

   /* // function which interact with custom Storage
    function sendStorageRequest(storageKey, type, method, data) {
        const path = ""// give server storage path 

        const options = {
            method: method,

            headers: {
                "Content-Type": type === "json" ? "application/json" : "text/plain",
            },
        };

        if (method === "PUT") {
            options.body = JSON.stringify(data);
        }

        return fetch(path, options).then(response => {
            if (!response.ok) {
                throw new Error(`Failed to ${method} data`);
            }

            return response.json();
        }).then(responseData => {
            return responseData;
        }).catch(error => {
            console.error("Error: ", error);
            return null;
        });
    }*/
    
});
