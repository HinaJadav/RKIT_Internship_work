$(document).ready(function () {
  let todoList = [];

  // Function to update the Todo count and list display
  function updateTodoDisplay() {
    $("#todoCount").text(todoList.length); // Update Todo count

    const todoListContainer = $("#todoList");
    todoListContainer.empty(); // Clear previous list

    if (todoList.length === 0) {
      todoListContainer.append(
        '<li class="dropdown-item">No items in Todo</li>'
      );
    } else {
      // Loop through each Todo item and add it to the list
      todoList.forEach((item, index) => {
        const todoItem = `
            <li class="dropdown-item d-flex justify-content-between align-items-center">
              ${item.title}
              <i class="fas fa-trash-alt delete-icon" data-index="${index}" style="cursor: pointer; color: red;"></i>
            </li>`;
        todoListContainer.append(todoItem);
      });
    }
  }

  // Add item to Todo List when "Add" button is clicked
  $(".addToTodo").on("click", function () {
    const itemTitle = $(this).data("title");

    // Check if the item already exists in the Todo list
    if (!todoList.some((item) => item.title === itemTitle)) {
      todoList.push({ title: itemTitle }); // Add item to Todo list
      updateTodoDisplay(); // Update Todo display
    } else {
      alert("Item already in Todo list");
    }
  });

  // Remove item from Todo List when delete icon is clicked
  $("#todoList").on("click", ".delete-icon", function () {
    const index = $(this).data("index");
    todoList.splice(index, 1); // Remove item from Todo list
    updateTodoDisplay(); // Update Todo display
  });
});

document
  .getElementById("addContestForm")
  .addEventListener("submit", function (e) {
    e.preventDefault();

    // Get form values
    const platformName = document.getElementById("platformName").value;
    const contestLink = document.getElementById("contestLink").value;
    const contestImage = document.getElementById("contestImage").files[0];

    if (contestImage) {
      const reader = new FileReader();
      reader.onload = function (e) {
        const imageData = e.target.result;

        // Create a new card
        const newCard = document.createElement("div");
        newCard.classList.add("col-md-4");
        newCard.innerHTML = `
                <div class="card">
                    <div class="image-container">
                        <img src="${imageData}" class="card-img-top" alt="${platformName}">
                    </div>
                    <div class="card-body text-center">
                        <h5 class="card-title">${platformName}</h5>
                        <p class="card-text"><a href="${contestLink}" target="_blank">Learn more</a></p>
                        <button class="btn btn-primary add-to-todo" data-title="${platformName}">Add</button>
                    </div>
                </div>
            `;

        // Append the new card to the grid
        document.querySelector(".container .row").appendChild(newCard);

        // Reset the form and close the modal
        document.getElementById("addContestForm").reset();
        const modal = bootstrap.Modal.getInstance(
          document.getElementById("addContestModal")
        );
        modal.hide();
      };
      reader.readAsDataURL(contestImage); // Read the image as a data URL
    }
  });
