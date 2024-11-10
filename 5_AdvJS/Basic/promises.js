// Problem: Handling asynchronous operations using "callbacks"
// In the following, we're passing a callback function to `createWor`, which will call `readyForWor` when it's ready.
// Simple function to simulate createWor
function createWor(marvels, callback) {
  // Simulate async operation using setTimeout
  setTimeout(() => {
    for (let i = 0; i < marvels.length; i++) {
      callback(marvels[i]); // Call the callback function for each marvel
    }
  }, 1000); // Simulate a 1-second delay
}

// Array of Marvel characters
const marvels = ["Ironman", "Thor", "Deadpool", "Batman"];

// Call createWor using a callback
createWor(marvels, function (marvelId) {
  readyForWor(marvelId); // Call the readyForWor function
});

// Mock readyForWor function to log the marvelId
function readyForWor(marvelId) {
  console.log("Ready for:", marvelId);
}

// Inversion of Control Issue:
// When using callbacks, we hand over the control of the callback to the `createWor` function.
// This can lead to problems since we lose control over when and how the callback will be executed.
// Callbacks may be called multiple times or even never called, which is risky and can cause bugs.

// Solution: Use Promises
// A promise is an object that represents the eventual completion (or failure) of an asynchronous operation and its resulting value.
// Promises solve the Inversion of Control issue by allowing us to attach callbacks (using `.then()` or `.catch()`) that are called when the promise is settled (either resolved or rejected).

// We use `createWor` to return a promise instead of relying on callback control.
// Initially, the promise object will hold an undefined value, but after the asynchronous operation finishes, the promise is settled with a resolved value.

const promise = createWor(marvels); // async operation
// The promise object will initially be in the pending state: { data: undefined }
// Once the asynchronous operation completes, the promise will either be fulfilled with data or rejected with an error.

// Example with `fetch` API (for getting user profile):
// const googleProfile = "https://api.github.com/users/HinaJadav";
// const user = fetch(googleProfile); // fetch returns a promise

// console.log(user); // logs the pending promise

// ----------------------------------------------------------------

// * Promises: It is a object representing the eventual completion or failure of an asynchronous operation.

// ----------------------------------------------------------------
