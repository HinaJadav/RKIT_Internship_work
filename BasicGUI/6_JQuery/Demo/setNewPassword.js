$(document).ready(function () {
  // jquery event: mouse hover
  $(":button").hover(
    function () {
      $(this).css("background-color", "#0056b3");
    },
    function () {
      $(this).css("background-color", "#007bff");
    }
  );

  function togglePasswordVisibility(passwordInputId, iconId) {
    const passwordInput = $(`#${passwordInputId}`);
    const toggleIcon = $(`#${iconId}`);

    if (passwordInput.attr("type") === "password") {
      passwordInput.attr("type", "text");
      toggleIcon.attr("src", "show.png"); // Show icon when password is visible
    } else {
      passwordInput.attr("type", "password");
      toggleIcon.attr("src", "hide.png"); // Hide icon when password is hidden
    }
  }

  // Event listeners for each toggle button
  $("#toggleNewPassword").on("click", function () {
    togglePasswordVisibility("newPassword", "newPasswordIcon");
  });

  $("#toggleConfirmPassword").on("click", function () {
    togglePasswordVisibility("confirmPassword", "confirmPasswordIcon");
  });

  //   Retrieve the current user's email from localStorage
  const currentUserEmail = localStorage.getItem("currentUserEmail");
  // Retrieve user info based on email
  const userInfo = JSON.parse(localStorage.getItem(currentUserEmail));

  // * JS Validators:
  // Validation for new password

  // * ReGex:
  // Password requirements:
  const hasLowercase = /[a-z]/;
  const hasUppercase = /[A-Z]/;
  const hasDigit = /[0-9]/;
  const hasSpecialChar = /[!@#$%^&*(),.?":{}|<>]/;

  function validatePassword(password) {
    const errors = [];

    if (!hasLowercase.test(password)) {
      errors.push("Password must contain at least one lowercase letter.");
    }

    if (!hasUppercase.test(password)) {
      errors.push("Password must contain at least one uppercase letter.");
    }

    if (!hasDigit.test(password)) {
      errors.push("Password must contain at least one digit.");
    }

    if (!hasSpecialChar.test(password)) {
      errors.push("Password must contain at least one special character.");
    }

    // length validator
    if (password.length < 8) {
      errors.push("Password must be at least 8 characters long.");
    }

    return errors;
  }

  $("#saveBtn").click(function (e) {
    e.preventDefault();

    let isValid = true;
    const newPassword = document.getElementById("newPassword").value;
    const newPasswordError = document.getElementById("newPasswordError");
    const confirmPassword = document.getElementById("confirmPassword").value;
    const confirmPasswordError = document.getElementById(
      "confirmPasswordError"
    );

    // Clear previous error messages
    newPasswordError.innerHTML = "";
    confirmPasswordError.innerHTML = "";

    // New password validation
    const validationErrors = validatePassword(newPassword);
    if (newPassword === "") {
      newPasswordError.innerHTML = "New password is required!";
      isValid = false;
    } else if (validationErrors.length > 0) {
      validationErrors.forEach((error) => {
        newPasswordError.innerHTML += `<p>${error}</p>`;
      });
      isValid = false;
    } else if (userInfo && userInfo.password === newPassword) {
      newPasswordError.innerHTML = "This password has already been used!";
      isValid = false;
    }

    // Confirm password validation
    if (confirmPassword !== newPassword) {
      confirmPasswordError.innerHTML = "Passwords do not match!";
      isValid = false;
    }

    // If form is valid, show popup and redirect
    if (isValid) {
      showPopupMessage("#popupMessage2");
      // Update password in the userInfo object
      if (userInfo) {
        userInfo.password = newPassword; // Set the new password

        // Store the updated userInfo back to localStorage
        localStorage.setItem(currentUserEmail, JSON.stringify(userInfo)); // Use currentUserEmail to get the key
      }
      setTimeout(() => {
        window.location.href = "login.html";
      }, 2000);
    }
  });

  function showPopupMessage(popupMessage) {
    $(popupMessage).fadeIn(500).delay(2000).fadeOut(500);
  }
});
