document.addEventListener("DOMContentLoaded", function () {
  const loginForm = document.getElementById("loginForm");

  loginForm.addEventListener("submit", (event) => {
    event.preventDefault();

    let isValid = true; // Track whether the form is valid

    // Validation for email
    const email = document.getElementById("emailInput");
    const emailError = document.getElementById("emailError");
    const userInfo = localStorage.getItem(email.value); // Assuming email is used as the key

    if (email.value === "" || email.value == null) {
      emailError.innerHTML = "Email is required!";
      isValid = false;
    } else if (userInfo === null) {
      alert("User is not registered!");
      window.location.href = "signUp.html";
    } else {
      emailError.innerHTML = "";
    }

    // Validation for password (Based on localStorage store values of user)
    const password = document.getElementById("password");
    const passwordError = document.getElementById("passwordError");

    if (password.value === "" || password.value == null) {
      passwordError.innerHTML = "Password is required!";
      isValid = false;
    } else if (JSON.parse(userInfo).password !== password.value) {
      // Correctly comparing password value
      passwordError.innerHTML = "Password is incorrect!";
      isValid = false;
    } else {
      passwordError.innerHTML = "";
    }

    if (isValid) {
      loginForm.submit();
      alert("Welcome, Learn code with LearnCode.");
      window.location.href = "home.html";
    }
  });
});

$(document).ready(function () {
  // .on():
  // Used to attach event handlers to elements. It provides a flexible way to listen to events like clicks, mouseover, keypress, etc., and execute functions in response.
  // Syntax: $(selector).on(event, [selector], [data], function)

  // Parameters:
  // 1) event: The event type(s) to listen for, e.g., 'click', 'mouseenter', 'keydown', or multiple events like 'click keydown'.
  // 2) selector (optional): A child element selector within the main selector where the event will be triggered. Useful for dynamically added elements.
  // 3) data (optional): Data to be passed to the event handler function when the event occurs.
  // 4) function: The function to execute when the event occurs.

  $("#togglePassword").on("click", function () {
    const passwordInput = $("#password");
    const toggleIcon = $("#toggleIcon");

    // .attr():
    // 1) To retrieve the value of any attribute from an HTML element, use .attr('<attribute name>').
    //    Example: $('#element').attr('src') retrieves the 'src' attribute of the element.
    // 2) To set or update one or more attributes, pass the attribute name and value as arguments:
    //      - To set a single attribute: .attr('<attribute name>', '<new value>')
    //      - To set multiple attributes: .attr({ '<attr1>': '<value1>', '<attr2>': '<value2>', ... })
    //    Example: $('#element').attr({ src: 'new-image.png', alt: 'New Image Description' });

    // * Get Attribute (JQuery) : attr()
    if (passwordInput.attr("type") === "password") {
      // * Set Attribute (JQuery) : attr()
      passwordInput.attr("type", "text");
      toggleIcon.attr("src", "show.png"); // Use "show" icon when password is visible
    } else {
      passwordInput.attr("type", "password");
      toggleIcon.attr("src", "hide.png"); // Use "hide" icon when password is hidden
    }
  });
});
