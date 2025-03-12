$(function () {
    var countryData = [
        { id: 1, name: "India" },
        { id: 2, name: "Japan" },
        { id: 3, name: "Sri Lanka" }
    ];

    var stateData = {
        1: [
            { id: 101, name: "Gujarat" },
            { id: 102, name: "Maharashtra" },
            { id: 103, name: "Karnataka" },
            { id: 104, name: "Tamil Nadu" },
            { id: 105, name: "West Bengal" }
        ],
        2: [
            { id: 201, name: "Tokyo" },
            { id: 202, name: "Osaka" },
            { id: 203, name: "Kyoto" },
            { id: 204, name: "Hokkaido" },
            { id: 205, name: "Fukuoka" }
        ],
        3: [
            { id: 301, name: "Colombo" },
            { id: 302, name: "Kandy" },
            { id: 303, name: "Galle" },
            { id: 304, name: "Jaffna" },
            { id: 305, name: "Matara" }
        ]
    };

    var districtData = {
        101: [{ id: 1001, name: "Ahmedabad" }, { id: 1002, name: "Surat" }],
        102: [{ id: 1003, name: "Mumbai" }, { id: 1004, name: "Pune" }],
        103: [{ id: 1005, name: "Bangalore" }, { id: 1006, name: "Mysore" }],
        104: [{ id: 1007, name: "Chennai" }, { id: 1008, name: "Coimbatore" }],
        105: [{ id: 1009, name: "Kolkata" }, { id: 1010, name: "Darjeeling" }],
        201: [{ id: 2001, name: "Shibuya" }, { id: 2002, name: "Shinjuku" }],
        202: [{ id: 2003, name: "Namba" }, { id: 2004, name: "Umeda" }],
        203: [{ id: 2005, name: "Gion" }, { id: 2006, name: "Arashiyama" }],
        301: [{ id: 3001, name: "Fort" }, { id: 3002, name: "Bambalapitiya" }],
        302: [{ id: 3003, name: "Peradeniya" }, { id: 3004, name: "Katugasthota" }]
    };

    var cityData = {
        1001: [{ id: 4001, name: "Navrangpura" }],
        1002: [{ id: 4002, name: "Adajan" }],
        1003: [{ id: 4003, name: "Bandra" }],
        1004: [{ id: 4004, name: "Shivajinagar" }],
        2001: [{ id: 5001, name: "Harajuku" }],
        2002: [{ id: 5002, name: "Kabukicho" }],
        3001: [{ id: 6001, name: "Galle Face" }],
        3002: [{ id: 6002, name: "Mount Lavinia" }]
    };

    $("#country").dxSelectBox({
        dataSource: countryData,
        displayExpr: "name",
        valueExpr: "id",
        placeholder: "Select Country",
        onValueChanged: function (e) {
            $("#state").dxSelectBox("option", "dataSource", stateData[e.value] || []);
            $("#state").dxSelectBox("option", "value", null);
        }
    });

    $("#state").dxSelectBox({
        dataSource: [],
        displayExpr: "name",
        valueExpr: "id",
        placeholder: "Select State",
        onValueChanged: function (e) {
            $("#district").dxSelectBox("option", "dataSource", districtData[e.value] || []);
            $("#district").dxSelectBox("option", "value", null);
        }
    });

    $("#district").dxSelectBox({
        dataSource: [],
        displayExpr: "name",
        valueExpr: "id",
        placeholder: "Select District",
        onValueChanged: function (e) {
            $("#city").dxSelectBox("option", "dataSource", cityData[e.value] || []);
            $("#city").dxSelectBox("option", "value", null);
        }
    });

    $("#city").dxSelectBox({
        dataSource: [],
        displayExpr: "name",
        valueExpr: "id",
        placeholder: "Select City",
        acceptCustomValue: true,
        onInput: function (e) {
            const searchValue = e.event.target.value.toLowerCase();
            const districtID = $("#district").dxSelectBox("option", "value");

            if (!districtID || !cityData[districtID]) return;

            const filteredCities = cityData[districtID].filter(city =>
                city.name.toLowerCase().includes(searchValue)
            );

            e.component.option("dataSource", filteredCities);

            if (filteredCities.length === 0) {
                e.component.close();
            } else {
                e.component.open();
            }
        }

    });
});
