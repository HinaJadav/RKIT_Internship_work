$(document).ready(function () {
  let todoList = [];

  // Function to update the Todo count and list display
  function updateTodoDisplay() {
    $("#todo-count").text(todoList.length); // Update Todo count

    const todoListContainer = $("#todo-list");
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
  $(".add-to-todo").on("click", function () {
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
  $("#todo-list").on("click", ".delete-icon", function () {
    const index = $(this).data("index");
    todoList.splice(index, 1); // Remove item from Todo list
    updateTodoDisplay(); // Update Todo display
  });
});
