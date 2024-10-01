document.addEventListener("DOMContentLoaded", () => {
  const signUpForm = document.getElementById("signUpForm");
  const nameInput = document.getElementById("nameInput");
  const emailInput = document.getElementById("emailInput");
  const newPasswordInput = document.getElementById("newPassword");
  const confirmPasswordInput = document.getElementById("confirmPassword");

  const userNameError = document.getElementById("nameError");
  const emailError = document.getElementById("emailError");
  const newPasswordError = document.getElementById("newPasswordError");
  const confirmPasswordError = document.getElementById("confirmPasswordError");
  const correctConfirmPasswordError = document.getElementById(
    "correctConfirmPasswordError"
  );

  const submitButton = document.getElementById("submit");

  submitButton.addEventListener("click", (e) => {
    // Get text from contenteditable elements and trim whitespaces
    const userName = nameInput.textContent.trim();
    const email = emailInput.textContent.trim();
    const newPassword = newPasswordInput.textContent.trim();
    const confirmPassword = confirmPasswordInput.textContent.trim();

    // Clear all error messages
    userNameError.textContent = "";
    emailError.textContent = "";
    newPasswordError.textContent = "";
    confirmPasswordError.textContent = "";
    correctConfirmPasswordError.textContent = "";

    // Flag to check if any validation failed
    let isValid = true;

    // Validation checks
    if (userName === "") {
      userNameError.textContent = "Name is required.";
      isValid = false;
    }

    if (email === "") {
      emailError.textContent = "Email is required.";
      isValid = false;
    }

    if (newPassword === "") {
      newPasswordError.textContent = "New password is required.";
      isValid = false;
    }

    if (confirmPassword === "") {
      confirmPasswordError.textContent = "Confirmation password is required.";
      isValid = false;
    }

    if (
      newPassword !== "" &&
      confirmPassword !== "" &&
      confirmPassword !== newPassword
    ) {
      correctConfirmPasswordError.textContent = "Passwords do not match.";
      isValid = false;
    }

    // If any validation fails, prevent form submission
    if (!isValid) {
      e.preventDefault();
    } else {
      // Form is valid, show success message
      alert("SignUp complete successfully.");
    }
  });
});
