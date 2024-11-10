document.addEventListener("DOMContentLoaded", function () {
  const loginForm = document.getElementById("loginForm");

  loginForm.addEventListener("submit", (event) => {
    event.preventDefault();

    let isValid = true; // Track whether the form is valid

    // Validation for email
    const email = document.getElementById("emailInput");
    const emailError = document.getElementById("emailError");

    if (email.value === "" || email.value == null) {
      emailError.innerHTML = "Email is required!";
      isValid = false;
    } else {
      emailError.innerHTML = "";
    }

    // Validation for password
    const password = document.getElementById("password");
    const passwordError = document.getElementById("passwordError");

    if (password.value === "" || password.value == null) {
      passwordError.innerHTML = "Password is required!";
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
