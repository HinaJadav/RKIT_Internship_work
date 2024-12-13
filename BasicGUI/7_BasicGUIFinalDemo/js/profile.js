document.addEventListener("DOMContentLoaded", async () => {
  const email = localStorage.getItem("currentUserEmail");

  // Fetch and display user profile
  await loadUserProfile(email);

  const editIcon = document.getElementById("editIcon");
  const profileImage = document.getElementById("profileImage");

  // Event listener for changing the profile image
  editIcon.addEventListener("click", function () {
    const fileInput = document.createElement("input");
    fileInput.type = "file";
    fileInput.accept = "image/*";

    fileInput.addEventListener("change", function () {
      const file = fileInput.files[0];
      if (file) {
        const reader = new FileReader();
        reader.onload = function (e) {
          profileImage.src = e.target.result;
          // Store the new image in localStorage
          const currentUserEmail = localStorage.getItem("currentUserEmail");
          if (currentUserEmail) {
            const userData =
              JSON.parse(localStorage.getItem(currentUserEmail)) || {};
            userData.profileImage = e.target.result;
            localStorage.setItem(currentUserEmail, JSON.stringify(userData));
          }
        };
        reader.readAsDataURL(file);
      }
    });

    // Trigger file input click
    fileInput.click();
  });
});

// Load user profile from localStorage
async function loadUserProfile(email) {
  const userInfo = JSON.parse(localStorage.getItem(email));
  console.log(userInfo);

  if (userInfo) {
    // show user information below user's profile image
    const userNameElement = document.getElementById("userName");
    const emailElement = document.getElementById("email");

    if (userNameElement)
      userNameElement.textContent = userInfo.userName || "User";
    if (emailElement) emailElement.textContent = userInfo.email;

    // Populate input fields, only if elements exist
    const firstNameInput = document.querySelector(
      "input[placeholder='first name']"
    );
    const surNameInput = document.querySelector("input[placeholder='surname']");
    const courseInput = document.querySelector(
      "input[placeholder='enter course/major']"
    );
    const emailInput = document.querySelector(
      "input[placeholder='enter email id']"
    );
    const phoneInput = document.querySelector(
      "input[placeholder='enter phone number']"
    );
    const codingSkillsInput = document.querySelector(
      "input[placeholder='enter coding skills']"
    );
    const stateInput = document.querySelector("input[placeholder='state']");
    const countryInput = document.querySelector("input[placeholder='country']");
    const experienceLevelInput = document.querySelector(
      "input[placeholder='enter experience level']"
    );

    if (firstNameInput) firstNameInput.value = userInfo.userName || "";
    if (surNameInput) surNameInput.value = userInfo.surname || "";
    if (courseInput) courseInput.value = userInfo.course || "";
    if (emailInput) emailInput.value = userInfo.email;
    if (phoneInput) phoneInput.value = userInfo.phone || "";
    if (codingSkillsInput)
      codingSkillsInput.value = userInfo.codingSkills || "";
    if (stateInput) stateInput.value = userInfo.state || "";
    if (countryInput) countryInput.value = userInfo.country || "";
    if (experienceLevelInput)
      experienceLevelInput.value = userInfo.experienceLevel || "";

    // Load profile image if available
    const profileImage = document.getElementById("profileImage");
    if (userInfo.profileImage && profileImage) {
      profileImage.src = userInfo.profileImage;
    }
  }
}

// Save profile changes
document
  .querySelector(".profile-button")
  .addEventListener("click", saveProfile);

function saveProfile() {
  const email = localStorage.getItem("currentUserEmail");
  const userInfo = JSON.parse(localStorage.getItem(email)) || {};

  // Update user information from form fields
  userInfo.userName =
    document.querySelector("input[placeholder='first name']")?.value || "";
  userInfo.surname = // Corrected key
    document.querySelector("input[placeholder='surname']")?.value || "";
  userInfo.course =
    document.querySelector("input[placeholder='enter course/major']")?.value ||
    "";
  userInfo.email =
    document.querySelector("input[placeholder='enter email id']")?.value || "";
  userInfo.phone =
    document.querySelector("input[placeholder='enter phone number']")?.value ||
    "";
  userInfo.codingSkills =
    document.querySelector("input[placeholder='enter coding skills']")?.value ||
    "";
  userInfo.state =
    document.querySelector("input[placeholder='state']")?.value || "";
  userInfo.country =
    document.querySelector("input[placeholder='country']")?.value || "";
  userInfo.experienceLevel =
    document.querySelector("input[placeholder='enter experience level']")
      ?.value || "";

  // Save updated profile data back to localStorage
  localStorage.setItem(email, JSON.stringify(userInfo));

  showPopupMessage("#popupMessage");

  setTimeout(function () {
    window.location.href = "home.html";
  }, 1500);
}
function showPopupMessage(popupMessage) {
  $(popupMessage).fadeIn(500).delay(2000).fadeOut(500);
}
