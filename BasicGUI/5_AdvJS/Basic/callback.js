// callback

console.log("Hello");
console.log("How are you?");
console.log("What are you doing?");

// o/p:
// Hello
// How are you?
// What are you doing?

// if we want to give timestamp to any intermidiate code part so it will execute after some perticuler time than we add timeout function with it which work as callback

console.log("Hello");
setTimeout(function () {
  console.log("How are you?");
}, 1000);
console.log("What are you doing?");

// o/p:
// Hello
// What are you doing?
// How are you? (it will be print after 1000ms)

// ----------------------------------------------------------------

// Issues with callbacks:

// 1) callBack Hell: when multiple call back function is write in nestesd form

// Structure of callbacks hell also know as "Pyramid of doom"

// ex: for check any stable things is alive thing or not
// check -> touch -> ifMove -> animal/human etc.

function checkAlive(isAlive, callback) {
  setTimeout(function () {
    if (isAlive) {
      console.log("It’s alive!");
      callback();
    } else {
      console.log("It’s not alive.");
    }
  }, 1000);
}

function touch(callback) {
  setTimeout(function () {
    console.log("Touched the object...");
    callback(true); // Assuming it moves after touching
  }, 1000);
}

function ifMove(moved, callback) {
  setTimeout(function () {
    if (moved) {
      console.log("It moved!");
      callback();
    } else {
      console.log("No movement detected.");
    }
  }, 1000);
}

function identify(callback) {
  setTimeout(function () {
    console.log("It’s an animal!");
    callback();
  }, 1000);
}

// Callback Hell structure:
checkAlive(true, function () {
  touch(function (moved) {
    ifMove(moved, function () {
      identify(function () {
        console.log("Process completed.");
      });
    });
  });
});
// o/p:
// It’s alive!
// Touched the object...
// It moved!
// It’s an animal!
// Process completed.

// 2) Inversion of control: This problem occurs when we give out code's one function control to another function, and we don't sure wether that function is execute or not.
