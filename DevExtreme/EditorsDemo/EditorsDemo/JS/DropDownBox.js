﻿$(function () {
    const cities = ["Ahemdabad", "Junagadh", "Nadiad", "Delhi", "Mumbai"];

    $("#permanentAddress").dxDropDownBox({
        acceptCustomValue: false,
        accessKey: 'p',
        showClearButton: true,
        value: cities[0],
        dataSource: cities,
        contentTemplate: function (e) {
            return $("<div>").dxList({
                dataSource: e.component.option("dataSource"),
                selectionMode: "single",
                onSelectionChanged: function (arg) {
                    if (arg.addedItems.length) {
                        e.component.option("value", arg.addedItems[0]);
                        e.component.close();
                    }
                }
            });
        },
        stylingMode: 'Filled',
        tabIndex: 1,
        placeholder: "Permanent address",
        width: "500px",
        onChange: function (e) {
            localStorage.setItem("permanentAddress", e.value);
            console.log("Permanent address updated:", e.value);
        },
        onClose: function (e) {
            console.log("Dropdown closed. Selected value:", e.component.option("value"));
        },
        onCopy: function () {
            DevExpress.ui.notify("Address copied!", "success", 1500);
        },
        onCut: function () {
            DevExpress.ui.notify("Address removed!", "warning", 1500);
        },
        onInitialized: function (e) {
            let savedAddress = localStorage.getItem("permanentAddress");
            if (savedAddress) {
                e.component.option("value", savedAddress);
            }
        },
        onValueChanged: function () {
            alert("Permanent address value changed successfully!");
        },
        validationError: { message: "Invalid address format!" },
        validationMessageMode: "always",
        onEnterKey: function () {
            $("#currentAddress").dxDropDownBox("instance").focus(); 
        },
    });

    $("#currentAddress").dxDropDownBox({
        valueExpr: "id",
        tabIndex: 2,
        stylingMode: 'Filled',
        showClearButton: true,
        placeholder: "Current address",
        width: "500px",
        dataSource: [
            { id: 1, address: "Ahemdabad" },
            { id: 2, address: "Rajkot" },
            { id: 3, address: "Mumbai" },
            { id: 4, address: "Delhi" }
        ],
        valueExpr: "address",
        displayExpr: "address",
        contentTemplate: function (e) {
            return $("<div>").dxList({
                dataSource: e.component.option("dataSource"),
                selectionMode: "multiple",
                onSelectionChanged: function (arg) {
                    if (arg.addedItems.length) {
                        e.component.option("value", arg.addedItems[0].address); 
                        e.component.close();
                    }
                }
            });
        },
        dropDownOptions: {
            showTitle: true,
            title: "Select Current Address",
            width: 200
        },
        onFocusIn: function () {
            console.log("Current Address field focused!");
        },
        onFocusOut: function () {
            console.log("Current Address field lost focus!");
        },
        onPaste: function () {
            alert("Current address pasted successfully!");
        },
        onValueChanged: function () {
            alert("Current address value changed successfully!");
        },
        opened: function () {
            alert("You started selecting your current address");
        },
        onCopy: function () {
            DevExpress.ui.notify("Current address copied!", "info", 1500);
        },
        onCut: function () {
            DevExpress.ui.notify("Current address removed!", "warning", 1500);
        },
        validationError: { message: "Invalid current address!" },
        validationMessageMode: "always",
        onEnterKey: function () {
            const permanentAddress = $("#permanentAddress").dxDropDownBox("instance").option("value");
            const currentAddress = $("#currentAddress").dxDropDownBox("instance").option("value");

            alert(`✅ Success!\nPermanent Address: ${permanentAddress}\nCurrent Address: ${currentAddress}`);
        },
    });
});


// onChange & onValueChange difference
// onChange: fires only when the field loses focus (after selecting and clicking outside).
// onValueChange: fires immediately when the selection changes.



// content() : it is used to get the popup content inside a DropDownBox
    //  Modify the popup content dynamically
    //  Access or manipulate elements inside the popup
    // Add custom logic or UI inside the dropdown



// difference between reset() and repaint()
// reset() clears the value.
// repaint() just refreshes the UI but does not change the value.




// difference between "elementAttr" & "inputAttr":
// elementAttr : applies to outer container like <div> of component
    // use for add extra style class id etc
    // works on all DevExtreme components

// inputAttr: applies on that input part or actual <input> field
    // use to specifies attributes like : maxLength, placeholder, readonly
    // works on only input-based components
