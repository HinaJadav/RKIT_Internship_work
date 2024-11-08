$(document).ready(function () {
  // Double array
  $("#doubleBtn").click(function (event) {
    event.preventDefault();
    const input = $("#arrayInput").val();
    const numbersArray = input.split(",").map(Number); // Convert each item to a number

    const DoubleArray = $.map(numbersArray, function (num) {
      return num * 2;
    });

    $("#result").text("Doubled Array: " + DoubleArray.join(", "));
  });

  // Find even numbers
  $("#findEvenBtn").click(function () {
    const input = $("#arrayInput").val();
    const numbersArray = input.split(",").map(Number);

    const evenNumbers = $.grep(numbersArray, function (num) {
      return num % 2 === 0;
    });

    $("#result").text("Even Numbers: " + evenNumbers.join(", "));
  });

  // Find odd numbers
  $("#findOddBtn").click(function () {
    const input = $("#arrayInput").val();
    const numbersArray = input.split(",").map(Number);

    const oddNumbers = $.grep(numbersArray, function (num) {
      return num % 2 !== 0;
    });

    $("#result").text("Odd Numbers: " + oddNumbers.join(", "));
  });

  // Concatenate arrays
  $("#concateBtn").click(function () {
    const input1 = $("#arrayInput1").val();
    const input2 = $("#arrayInput2").val();

    // Check if both inputs have values
    if (input1 && input2) {
      const numbersArray1 = input1.split(",").map(Number);
      const numbersArray2 = input2.split(",").map(Number);

      // Merge the two arrays
      const mergedArray = $.merge(numbersArray1, numbersArray2);

      $("#resultConcate").text("Merged Array: " + mergedArray.join(", "));
    } else {
      $("#resultConcate").text("Please enter values in both input fields.");
    }
  });

  
});
