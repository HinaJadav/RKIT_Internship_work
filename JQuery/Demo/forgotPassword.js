$(document).ready(function () {
  // "fad" effect of jQuery
  function showPopupMessage() {
    $("#popupMessage").fadeIn(500).delay(2000).fadeOut(500);
  }

  $("#loginButton").click(function () {
    showPopupMessage();
    // "callback" function
    setTimeout(function () {
      window.location.href = "login.html";
    }, 2000);
  });
});
