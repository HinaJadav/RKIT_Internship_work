$(document).ready(function () {
  const signUpForm = document.getElementById("signUpForm");

  signUpForm.addEventListener("submit", async (event) => {
    event.preventDefault(); // Prevent form submission + browser reload

    // Validation logic
    let isValid = validateForm();

    // Store user information securely if validation passes
    if (isValid) {
      alert("Sign up successfully done.");

      const email = document.getElementById("emailInput").value;
      const userInfo = {
        userName: document.getElementById("userNameInput").value,
        email: email,
        password: await hashPassword(
          document.getElementById("newPassword").value
        ),
        schoolCollege: document.getElementById("schoolCollegeInput").value,
        course: document.getElementById("courseInput").value,
        codingSkills: document.getElementById("codingSkillsInput").value,
        experienceLevel: document.getElementById("experienceLevel").value,
      };

      localStorage.setItem(email, JSON.stringify(userInfo));
      console.log(userInfo);

      // Form submission and redirect
      signUpForm.submit();
      window.location.href = "login.html";
    }
  });

  // Function to hash the password using SHA-256
  async function hashPassword(password) {
    const msgBuffer = new TextEncoder().encode(password);
    const hashBuffer = await crypto.subtle.digest("SHA-256", msgBuffer);
    const hashArray = Array.from(new Uint8Array(hashBuffer));
    return hashArray.map((b) => b.toString(16).padStart(2, "0")).join("");
  }

  // Form validation
  function validateForm() {
    let isValid = true;

    if (!validateUserName() || !validateEmail() || !validatePasswordFields()) {
      isValid = false;
    }

    if (
      !validateSchoolCollege() ||
      !validateCourse() ||
      !validateCodingSkills() ||
      !validateExperienceLevel()
    ) {
      isValid = false;
    }

    return isValid;
  }

  // Function to validate username
  function validateUserName() {
    const userName = document.getElementById("userNameInput");
    const userNameError = document.getElementById("userNameError");

    if (userName.value === "" || userName.value == null) {
      userNameError.innerHTML = "User name is required!";
      return false;
    } else {
      userNameError.innerHTML = "";
      return true;
    }
  }

  // Function to validate email
  function validateEmail() {
    const email = document.getElementById("emailInput");
    const emailError = document.getElementById("emailError");

    if (email.value === "" || email.value == null) {
      emailError.innerHTML = "Email is required!";
      return false;
    } else if (!/^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$/.test(email.value)) {
      emailError.innerHTML = "Please enter a valid email address!";
      return false;
    } else if (localStorage.getItem(email.value) !== null) {
      alert("User is already registered!");
      window.location.href = "login.html";
      return false;
    } else {
      emailError.innerHTML = "";
      return true;
    }
  }

  // Function to validate password fields
  function validatePasswordFields() {
    const newPassword = document.getElementById("newPassword");
    const newPasswordError = document.getElementById("newPasswordError");
    const confirmPassword = document.getElementById("confirmPassword");
    const confirmPasswordError = document.getElementById(
      "confirmPasswordError"
    );

    const validationErrors = validatePassword(newPassword.value);
    newPasswordError.innerHTML = ""; // Clear any previous errors

    if (newPassword.value === "" || newPassword.value == null) {
      newPasswordError.innerHTML = "New password is required!";
      return false;
    } else if (validationErrors.length !== 0) {
      validationErrors.forEach((error) => {
        newPasswordError.innerHTML += `<p>${error}</p>`;
      });
      return false;
    } else if (confirmPassword.value !== newPassword.value) {
      confirmPasswordError.innerHTML = "Passwords do not match!";
      return false;
    } else {
      confirmPasswordError.innerHTML = "";
      return true;
    }
  }

  // Password validation helper
  function validatePassword(password) {
    const errors = [];
    const hasLowercase = /[a-z]/;
    const hasUppercase = /[A-Z]/;
    const hasDigit = /[0-9]/;
    const hasSpecialChar = /[!@#$%^&*(),.?":{}|<>]/;

    if (!hasLowercase.test(password))
      errors.push("Password must contain at least one lowercase letter.");
    if (!hasUppercase.test(password))
      errors.push("Password must contain at least one uppercase letter.");
    if (!hasDigit.test(password))
      errors.push("Password must contain at least one digit.");
    if (!hasSpecialChar.test(password))
      errors.push("Password must contain at least one special character.");
    if (password.length < 8)
      errors.push("Password must be at least 8 characters long.");

    return errors;
  }

  // Toggle password visibility
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

  // Validation functions for the new fields
  function validateSchoolCollege() {
    const schoolCollege = document.getElementById("schoolCollegeInput");
    const schoolCollegeError = document.getElementById("schoolCollegeError");

    if (schoolCollege.value === "" || schoolCollege.value == null) {
      schoolCollegeError.innerHTML = "School/College name is required!";
      return false;
    } else {
      schoolCollegeError.innerHTML = "";
      return true;
    }
  }

  function validateCourse() {
    const course = document.getElementById("courseInput");
    const courseError = document.getElementById("courseError");

    if (course.value === "" || course.value == null) {
      courseError.innerHTML = "Course/Major is required!";
      return false;
    } else {
      courseError.innerHTML = "";
      return true;
    }
  }

  function validateCodingSkills() {
    const codingSkills = document.getElementById("codingSkillsInput");
    const codingSkillsError = document.getElementById("codingSkillsError");

    if (codingSkills.value === "" || codingSkills.value == null) {
      codingSkillsError.innerHTML = "Coding skills are required!";
      return false;
    } else {
      codingSkillsError.innerHTML = "";
      return true;
    }
  }

  function validateExperienceLevel() {
    const experienceLevel = document.getElementById("experienceLevel");
    const experienceError = document.getElementById("experienceError");

    if (experienceLevel.value === "") {
      experienceError.innerHTML = "Experience level is required!";
      return false;
    } else {
      experienceError.innerHTML = "";
      return true;
    }
  }
});
