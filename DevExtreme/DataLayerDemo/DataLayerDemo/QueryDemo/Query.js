$(function () {
    const studentData = [
        { id: 1, name: "Aarav Sharma", email: "aarav.sharma@gmail.com", gender: "male", fees: 15000, package: 200000 },
        { id: 2, name: "Priya Iyer", email: "priya.iyer@gmail.com", gender: "female", fees: 18000, package: 900000 },
        { id: 3, name: "Rohan Verma", email: "rohan.verma@gmail.com", gender: "male", fees: 13500, package: 400000 },
        { id: 4, name: "Sneha Nair", email: "sneha.nair@gmail.com", gender: "female", fees: 16000, package: 200000 },
        { id: 5, name: "Vikram Singh", email: "vikram.singh@gmail.com", gender: "male", fees: 14000, package: 300000 }
    ];

    // Using Promise.all() to handle all async queries together
    Promise.all([
        DevExpress.data.query(studentData).avg("package"),   // Average package
        DevExpress.data.query(studentData).count(),          // Total student count
        DevExpress.data.query(studentData).sum("fees"),      // Total fees
        DevExpress.data.query(studentData).max("package")    // Maximum package
    ]).then(([avgPackage, countStudent, totalFees, maxPackage]) => {
        console.log("Average Package: " + avgPackage);
        console.log("Total Students: " + countStudent);
        console.log("Total Fees: " + totalFees);
        console.log("Max Package: " + maxPackage);
    });

    // Getting female students
    const femaleStudents = DevExpress.data.query(studentData)
        .filter(["gender", "=", "female"])
        .toArray();
    console.log("Female Students:", femaleStudents);

    // Grouping students by gender
    const groupedData = DevExpress.data.query(studentData)
        .groupBy("gender")
        .toArray();
    console.log("Grouped Data by Gender:", groupedData);

    // Selecting only name and email
    const studentBasicInfo = DevExpress.data.query(studentData)
        .select("name", "email")
        .toArray();
    console.log("Student Basic Info:", studentBasicInfo);

    // Sorting students by package
    const sortByPackage = DevExpress.data.query(studentData)
        .sortBy("package")
        .toArray();
    console.log("Sorted by Package:", sortByPackage);

    // Sorting first by package, then by fees in descending order
    const sortedData = DevExpress.data.query(studentData)
        .sortBy("package")
        .thenBy("fees", true) // true = descending order
        .toArray();
    console.log("Sorted by Package, then by Fees (Desc):", sortedData);
});
