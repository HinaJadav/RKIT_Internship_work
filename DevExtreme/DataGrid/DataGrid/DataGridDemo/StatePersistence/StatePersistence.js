import { users, countries, states } from '../userData.js';

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
            enabled: true,
            type: "custom",
            customLoad: function () {
                return sendStorageRequest("storageKey", "json", "GET");
            },
            customSave: function (state) {
                sendStorageRequest("storageKey", "text", "PUT", state);
            }
        },
       
    });

    
});
