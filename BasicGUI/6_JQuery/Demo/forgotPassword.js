$(document).ready(function () {
  // Validation function for email
  function validateEmail() {
    const email = document.getElementById("emailInput");
    localStorage.setItem("currentUserEmail", email.value);
    const emailError = document.getElementById("emailError");
    let isValid = true;

    if (!email.value) {
      emailError.innerHTML = "Email is required!";
      isValid = false;
    } else if (!/^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$/.test(email.value)) {
      emailError.innerHTML = "Please enter a valid email address!";
      isValid = false;
    } else {
      emailError.innerHTML = "";
    }

    return isValid;
  }

  // Fade-in and fade-out popup message
  function showPopupMessage(popupMessage) {
    $(popupMessage).fadeIn(500).delay(2000).fadeOut(500);
  }

  // Form submission handler
  $("#forgotPasswordForm").submit(function (e) {
    e.preventDefault();
    if (validateEmail()) {
      showPopupMessage("#popupMessage2");
      setTimeout(function () {
        window.location.href = "verifyEmail.html";
      }, 2000);
    }
  });

  // Redirect to login page with a popup
  $("#loginButton").click(function () {
    showPopupMessage("#popupMessage1");
    setTimeout(function () {
      window.location.href = "login.html";
    }, 2000);
  });
});
