document.addEventListener("DOMContentLoaded", function () {
  const loginForm = document.getElementById("loginForm");

  loginForm.addEventListener("submit", async (event) => {
    event.preventDefault();

    const emailInput = document.getElementById("emailInput");
    const passwordInput = document.getElementById("password");

    // Perform validation
    const isEmailValid = validateEmail(emailInput);
    const isPasswordValid = await validatePassword(
      passwordInput,
      emailInput.value
    );

    if (isEmailValid && isPasswordValid) {
      handleSuccessfulLogin(emailInput.value);
    }
  });
});

// Validation function for email
function validateEmail(emailInput) {
  const emailError = document.getElementById("emailError");
  const userInfo = localStorage.getItem(emailInput.value);

  if (!emailInput.value) {
    emailError.innerHTML = "Email is required!";
    return false;
  } else if (userInfo === null) {
    alert("User is not registered!");
    window.location.href = "signUp.html";
    return false;
  } else {
    emailError.innerHTML = "";
    return true;
  }
}

// Validation function for password
async function validatePassword(passwordInput, email) {
  const passwordError = document.getElementById("passwordError");
  const userInfo = localStorage.getItem(email);

  if (!passwordInput.value) {
    passwordError.innerHTML = "Password is required!";
    return false;
  } else {
    const enteredPasswordHash = await hashPassword(passwordInput.value);
    const storedPasswordHash = JSON.parse(userInfo).password;

    if (enteredPasswordHash !== storedPasswordHash) {
      passwordError.innerHTML = "Password is incorrect!";
      return false;
    } else {
      passwordError.innerHTML = "";
      return true;
    }
  }
}

// Function to hash the password using SHA-256
async function hashPassword(password) {
  const msgBuffer = new TextEncoder().encode(password);
  const hashBuffer = await crypto.subtle.digest("SHA-256", msgBuffer);
  const hashArray = Array.from(new Uint8Array(hashBuffer));
  return hashArray.map((b) => b.toString(16).padStart(2, "0")).join("");
}

// Handle successful login
function handleSuccessfulLogin(email) {
  localStorage.setItem("currentUserEmail", email);
  // getItem check
  alert("Welcome, Learn code with LearnCode.");
  window.location.href = "home.html";
}

$(document).ready(function () {
  // Toggle password visibility
  $("#togglePassword").on("click", function () {
    togglePasswordVisibility();
  });
});

// Function to toggle password visibility
function togglePasswordVisibility() {
  const passwordInput = $("#password");
  const toggleIcon = $("#toggleIcon");

  if (passwordInput.attr("type") === "password") {
    passwordInput.attr("type", "text");
    toggleIcon.attr("src", "/7_BasicGUIFinalDemo/assets/images/show.png");
  } else {
    passwordInput.attr("type", "password");
    toggleIcon.attr("src", "/7_BasicGUIFinalDemo/assets/images/hide.png");
  }
}
