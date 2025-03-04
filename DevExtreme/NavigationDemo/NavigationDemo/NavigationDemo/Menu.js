$(function () {
    // Customize Item Appearance
    let menuItems = [
        {
            text: 'Dashboard',
            icon: 'home',
            path: "/dashboard"
        },
        {
            text: 'Bugs',
            icon: 'bug',
            items: [
                { text: "All Bugs", path: "/bugs/all" },
                { text: "My Bugs", path: "/bugs/my" },
                { text: "Create Bug", path: "/bugs/create" }
            ]
        },
        {
            text: 'Users',
            icon: 'group',
            items: [
                { text: "All Users", path: "/users/all" },
                { text: "Add User", path: "/users/add" }
            ]
        },
        {
            text: "Reports",
            icon: "chart",
            path: "/reports"
        },
        {
            text: "Settings",
            icon: "preferences",
            path: "/settings",
            disabled: true,
        },
        {
            text: "Enable Adaptivity",
            icon: "check",
            adaptiveToggle: true // Custom property to track adaptivity toggle
        }
    ];

    let menuInstance = $("#menu").dxMenu({
        items: menuItems,
        adaptivityEnabled: false,
        showFirstSubmenuMode: {
            name: "onHover"  // Ensures submenus open on hover
        },
        showSubmenuMode: {
            name: "onHover",
            delay: { show: 100, hide: 300 }
        },
        onItemClick: function (e) {
            if (e.itemData.path) {
                console.log("Navigate to:", e.itemData.path);
                window.location.href = e.itemData.path;
            }
            else if (e.itemData.adaptiveToggle) {
                if (!menuInstance) {
                    console.error("Menu instance not found!");
                    return;
                }

                const currentState = menuInstance.option('adaptivityEnabled');
                menuInstance.option('adaptivityEnabled', !currentState);

                // Update the adaptive toggle icon properly
                menuItems = menuItems.map(item => {
                    if (item.adaptiveToggle) {
                        return { ...item, icon: currentState ? "close" : "check" };
                    }
                    return item;
                });

                // Refresh menu items
                menuInstance.option("items", [...menuItems]);

                console.log("Adaptivity Enabled:", !currentState);
            }
            else {
                console.log("Clicked on menu item:", e.itemData.text);
                alert("You clicked: " + e.itemData.text);
            }
        },

        onSubmenuShowing: function (e) {
            console.log("Submenu is about to be displayed:", e);
        },
        onSubmenuShown: function (e) {
            console.log("Submenu has been displayed:", e);
        }
    }).dxMenu("instance");
});
