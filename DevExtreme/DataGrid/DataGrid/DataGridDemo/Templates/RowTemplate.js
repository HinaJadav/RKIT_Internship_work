import { users } from '../userData.js';

$(function () {
    $("#dataGrid").dxDataGrid({
        dataSource: users,
        keyExpr: "ID",
        columnFixing: { enabled: true },
        allowColumnReordering: true,
        columnAutoWidth: true,
        showColumnLines: true,
        showRowLines: true,
        rowAlternationEnabled: true,
        showBorders: true,

        searchPanel: { visible: true }, // Optional: Adds a search panel in toolbar
        editing: {
            mode: "row",
            allowAdding: true,
            allowUpdating: true,
            allowDeleting: true
        },
        toolbar: {
            items: [
                {
                    location: "before",
                    widget: "dxButton",
                    options: {
                        text: "Custom Button",
                        onClick: function () {
                            alert("Button Clicked!");
                        }
                    }
                }
            ]
        },

       

        rowTemplate: function (container, item) {
            const data = item.data;

            // Create the default row
            const mainRow = $("<tr>").appendTo(container);
            $("<td>").text(data.ID).appendTo(mainRow);
            $("<td>").text(data.Name).appendTo(mainRow);
            $("<td>").text(data.Email).appendTo(mainRow);
            $("<td>").text(data.PhoneNumber).appendTo(mainRow);
            $("<td>").text(data.HireDate).appendTo(mainRow);
            $("<td>").text(data.Gender === 1 ? "Male" : "Female").appendTo(mainRow);

            // Create an additional row for extra details
            const detailRow = $("<tr>").addClass("detail-row");
            $("<td>")
                .attr("colspan", 6) // Span across all columns
                .css({ "padding": "8px", "font-style": "italic", "color": "#666" })
                .text(`User ${data.Name} is an active member with email ${data.Email}.`)
                .appendTo(detailRow);

            // Append extra row below the main row
            container.append(detailRow);
        }



    });

    console.log($("#dataGrid").dxDataGrid("instance").option("toolbar"));

});
