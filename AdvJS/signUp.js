document.addEventListener("DOMContentLoaded", function () {
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
    } else {
      emailError.innerHTML = "";
    }

    // Validation for password
    const newPassword = document.getElementById("newPassword");
    const newPasswordError = document.getElementById("newPasswordError");
    const confirmPassword = document.getElementById("confirmPassword");
    const confirmPasswordError = document.getElementById(
      "confirmPasswordError"
    );

    const passwordRegex =
      /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*(),.?":{}|<>])[A-Za-z\d!@#$%^&*(),.?":{}|<>]{8,}$/;

    if (newPassword.value === "" || newPassword.value == null) {
      newPasswordError.innerHTML = "New password is required!";
      isValid = false;
    } else if (!passwordRegex.test(newPassword.value)) {
      newPasswordError.innerHTML =
        "Password must be at least 8 characters long, and include an uppercase letter, a lowercase letter, a number, and a special character!";
      isValid = false;
    } else {
      newPasswordError.innerHTML = "";
    }

    // Validate confirm password matches new password
    if (confirmPassword.value !== newPassword.value) {
      confirmPasswordError.innerHTML = "Passwords do not match!";
      isValid = false;
    } else {
      confirmPasswordError.innerHTML = "";
    }

    // If all validations pass, submit the form
    if (isValid) {
      signUpForm.submit();
      alert("SignUp successfully done.");
      window.location.href = "login.html";
    }
  });
});
