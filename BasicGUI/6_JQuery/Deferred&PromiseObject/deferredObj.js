// Deferred Objects: A deferred object describes the state of availability of something, and offers a nice interface to help when that state changes.
// It is mostly used to handle latency in fetching responses.

// A deferred object starts in a "pending" state
// While in the pending state if the resolve() function is called then the state is changed to "resolved"
// If the reject() function is called then the state is changed to "rejected".

console.log($.Deferred().state());
console.log($.Deferred().resolve().state());
console.log($.Deferred().reject().state());

// Callbacks can be attached to these state changes using the "done() and fail()" functions, or to all state changes using the "always()" function

// .done() is for the success scenario
// .fail() handles errors
// .always() will execute no matter the outcome

$(document).ready(function () {
  function passwordValidation(password) {
    let deferred = $.Deferred();

    setTimeout(() => {
      let passwordRegex =
        /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*(),.?":{}|<>])[A-Za-z\d!@#$%^&*(),.?":{}|<>]{8,}$/;

      passwordRegex.test(password)
        ? deferred.resolve("Password is correct.")
        : deferred.reject(
            "Password must be at least 8 characters long, and include an uppercase letter, a lowercase letter, a number, and a special character!"
          );
    }, 2000);

    return deferred.promise();
  }

  // Add event listener for the form submission
  $("#passwordForm").on("submit", function (event) {
    event.preventDefault(); // Prevent form from submitting the traditional way

    // Get the password input value
    const inputPassword = $("#passwordInput").val();

    // Call the password validation function
    passwordValidation(inputPassword)
      .done((msg) => {
        $("#message").text(msg).css("color", "green"); // Display success message
      })
      .fail((error) => {
        $("#message").text(error).css("color", "red"); // Display error message
      })
      .always(() => {
        console.log("Process Complete!");
      });
  });
});
