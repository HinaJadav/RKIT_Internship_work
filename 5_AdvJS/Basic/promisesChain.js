// Solution of Problem "callback hell": promises chain

// ex:
// ex: for check any stable things is alive thing or not
// check -> touch -> ifMove -> animal/human etc.

// Convert each function to return a Promise

function checkAlive(isAlive) {
  return new Promise((resolve, reject) => {
    setTimeout(function () {
      if (isAlive) {
        console.log("It’s alive!");
        resolve(); // Proceed if alive
      } else {
        reject("It’s not alive."); // Reject if not alive
      }
    }, 1000);
  });
}

function touch() {
  return new Promise((resolve) => {
    setTimeout(function () {
      console.log("Touched the object...");
      resolve(true); // Assuming it moves after touching
    }, 1000);
  });
}

function ifMove(moved) {
  return new Promise((resolve, reject) => {
    setTimeout(function () {
      if (moved) {
        console.log("It moved!");
        resolve(); // Proceed if moved
      } else {
        reject("No movement detected."); // Reject if no movement
      }
    }, 1000);
  });
}

function identify() {
  return new Promise((resolve) => {
    setTimeout(function () {
      console.log("It’s an animal!");
      resolve("Process completed."); // Resolve with a completion message
    }, 1000);
  });
}

// Promise chain to handle the operations
checkAlive(true)
  .then(() => touch()) // Once checkAlive resolves, call touch
  .then((moved) => ifMove(moved)) // Once touch resolves, pass moved to ifMove
  .then(() => identify()) // Once ifMove resolves, call identify
  .then((message) => console.log(message)) // Once identify resolves, log the message
  .catch((error) => console.error("Error:", error)); // Handle any errors

//  o/p:
// It’s alive!
// Touched the object...
// It moved!
// It’s an animal!
// Process completed.
