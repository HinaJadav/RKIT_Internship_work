$(document).ready(function () {
  // Validation logic
  const signUpForm = document.getElementById("signUpForm");

  signUpForm.addEventListener("submit", (event) => {
    event.preventDefault(); // Prevent form submission + browser reload

    let isValid = true; // Track whether the form is valid

    // Validation for name
    const userName = document.getElementById("userNameInput");
    const userNameError = document.getElementById("userNameError");

    if (userName.value === "" || userName.value == null) {
      userNameError.innerHTML = "User name is required!";
      isValid = false;
    } else {
      userNameError.innerHTML = "";
    }

    // Validation for email
    const email = document.getElementById("emailInput");
    const emailError = document.getElementById("emailError");

    if (email.value === "" || email.value == null) {
      emailError.innerHTML = "Email is required!";
      isValid = false;
    } else if (!/^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$/.test(email.value)) {
      emailError.innerHTML = "Please enter a valid email address!";
      isValid = false;
    } else if (localStorage.getItem(email.value) !== null) {
      alert("User is already registered!");
      window.location.href = "login.html";
      // emailError.innerHTML = "User is already exists!";
      // isValid = false;
    } else {
      emailError.innerHTML = "";
    }

    // * JS Validators:
    // Validation for password
    const newPassword = document.getElementById("newPassword");
    const newPasswordError = document.getElementById("newPasswordError");
    const confirmPassword = document.getElementById("confirmPassword");
    const confirmPasswordError = document.getElementById(
      "confirmPasswordError"
    );

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

    const validationErrors = validatePassword(newPassword.value);

    // Clear any previous errors
    newPasswordError.innerHTML = "";

    if (newPassword.value === "" || newPassword.value == null) {
      newPasswordError.innerHTML = "New password is required!";
      isValid = false;
    } else if (validationErrors.length !== 0) {
      // * foreach loop
      // Display each error in a new line
      validationErrors.forEach((error) => {
        newPasswordError.innerHTML += `<p>${error}</p>`;
      });
      isValid = false;
    } else {
      newPasswordError.innerHTML = ""; // Clear if no errors
    }

    // Validate confirm password matches new password
    if (confirmPassword.value !== newPassword.value) {
      confirmPasswordError.innerHTML = "Passwords do not match!";
      isValid = false;
    } else {
      confirmPasswordError.innerHTML = "";
    }

    // * LocalStorage:
    if (isValid) {
      alert("SignUp successfully done.");

      const userInfo = {
        userName: userName.value,
        email: email.value,
        password: newPassword.value,
      };

      localStorage.setItem(email.value, JSON.stringify(userInfo));
      console.log(userInfo);

      // Form submission and redirect
      signUpForm.submit();
      window.location.href = "login.html";
    }
  });
  //});

  // Generic function to toggle password visibility
  function togglePasswordVisibility(passwordInputId, showIconId, hideIconId) {
    const passwordInput = $(`#${passwordInputId}`);
    const showIcon = $(`#${showIconId}`);
    const hideIcon = $(`#${hideIconId}`);

    if (passwordInput.attr("type") === "password") {
      passwordInput.attr("type", "text");
      showIcon.show();
      hideIcon.hide();
    } else {
      passwordInput.attr("type", "password");
      hideIcon.show();
      showIcon.hide();
    }
  }

  // Event listeners for each toggle button
  $("#newPasswordShowIcon, #newPasswordHideIcon").on("click", function () {
    togglePasswordVisibility(
      "newPassword",
      "newPasswordShowIcon",
      "newPasswordHideIcon"
    );
  });

  $("#confirmPasswordShowIcon, #confirmPasswordHideIcon").on(
    "click",
    function () {
      togglePasswordVisibility(
        "confirmPassword",
        "confirmPasswordShowIcon",
        "confirmPasswordHideIcon"
      );
    }
  );
});
