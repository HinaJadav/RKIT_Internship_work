$(document).ready(function () {
  // OTP input focus handling
  $(".otpInput").on("input", function () {
    if (this.value.length === this.maxLength) {
      $(this).next(".otpInput").focus();
    }
  });

  function showPopupMessage(popupMessage) {
    $(popupMessage).fadeIn(500).delay(2000).fadeOut(500);
  }

  // Resend OTP handling
  $("#resendOTPBtn").click(function (e) {
    e.preventDefault();
    showPopupMessage("#popupMessage2");
  });

  // Verify OTP
  $("#verifyEmailForm").submit(function (e) {
    e.preventDefault();

    let otp = "";
    let isValid = true;

    // * Get Content (JQuery) : val()
    $(".otpInput").each(function () {
      otp += $(this).val();
      if ($(this).val() === "") isValid = false;
    });

    if (!isValid || otp.length !== 4) {
      $("#otpError").text("Please enter a 4-digit OTP code.");
      return;
    } else {
      $("#otpError").text("");
      showPopupMessage("#popupMessage1");
      setTimeout(() => {
        window.location.href = "setNewPassword.html";
      }, 2000);
    }
  });
});
