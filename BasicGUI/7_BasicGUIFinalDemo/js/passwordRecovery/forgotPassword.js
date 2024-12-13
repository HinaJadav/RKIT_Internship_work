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

  // Set OTP
  function setOtpInSessionStorage(otp) {
    sessionStorage.setItem("otp", otp);
  }

  // Send OTP
  // function sendOTP() {
  //   let otpValue = Math.floor(Math.random() * 10000);
  //   let emailBody = `<h2> Your OTP is ${otpValue} </h2>`;

  //   Email.send({
  //     SecureToken: "e0be2614-77ec-49d0-b1cf-cb9374892f98",
  //     To: email.value,
  //     From: "hinajadav21@gamil.com",
  //     Subject: "Email verification using OTP.",
  //     Body: emailBody,
  //   }).then(() => {
  //     showPopupMessage("#popupMessage2" + email.value);
  //     setOtpInSessionStorage(otpValue);
  //   });
  // }
  function sendOTP() {
    let otpValue = Math.floor(Math.random() * 10000); // Generate a 4-digit OTP
    let emailBody = `<h2>Your OTP is ${otpValue}</h2>`;

    const email = document.getElementById("emailInput"); // Make sure email is correctly referenced

    console.log(otpValue);
    Email.send({
      SecureToken: "E5C5653DD3232C4FBD9470B9A169981FA7CB",
      To: email.value,
      From: "jadavhinaa@gmail.com",
      Subject: "Email verification using OTP",
      Body: emailBody,
    })
      .then(() => {
        console.log("OTP sent successfully to " + email.value);
        showPopupMessage("#popupMessage2");
        setOtpInSessionStorage(otpValue);
      })
      .catch((error) => {
        console.error("Error sending OTP: ", error);
        alert("There was an issue sending the OTP. Please try again.");
      });
  }

  // Fade-in and fade-out popup message
  function showPopupMessage(popupMessage) {
    $(popupMessage).fadeIn(500).delay(2000).fadeOut(500);
  }

  // Form submission handler
  $("#forgotPasswordForm").submit(function (e) {
    e.preventDefault();
    if (validateEmail()) {
      sendOTP();
      setTimeout(function () {
        window.location.href = "verifyEmail.html";
      }, 2000);
    }
  });
});
