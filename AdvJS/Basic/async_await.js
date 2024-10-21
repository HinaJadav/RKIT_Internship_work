// async: It is a keyword which use before create asynchronous function.
async function getMassage() {
  return "Welcom async await."; // return some datatype values
}

// Async function always returns a promise. If you return any other data types then this function wraps that value into promise object and then return.

const massagePromise = getMassage();
console.log(massagePromise);
// o/p:
// Promise { 'Welcom async await.' }

// Handle promise object
massagePromise.then((result) => console.log(result));
// o/p: Welcom async await.

// ----------------------------------------------------------------
// Try to return promise object from async function
const newPromise = new Promise((resolve, reject) => {
  resolve("Promise resolved successfully.");
});

async function getPromise() {
  return newPromise;
}

const massagePromise2 = getPromise();
console.log(massagePromise2);
// o/p: Promise { <pending> } --> It will show current state of the promise object --> which is empty and at that point data will be not loaded in promise object

massagePromise2.then((result) => console.log(result));
// o/p: Promise resolved successfully.

// ----------------------------------------------------------------
// await with async : use to handle promises
// await: this keyword only used inside async function
// * async can use without await
// * await can't be used without async

const massagePromise3 = new Promise((resolve, reject) => {
  resolve("Promise resolved successfully with async & await.");
});

async function handlePromise() {
  const value = await massagePromise3;
  console.log(value);
}

handlePromise();
// o/p: Promise resolved successfully with async & await.

// ----------------------------------------------------------------

// set timer into async await function for work it as actual promise

const massagePromiseWithTimer = new Promise((resolve, reject) => {
  setTimeout(() => {
    resolve("Promise resolved successfully with async & await + Timer.");
  }, 1000);
});

async function handlePromiseWithTimer() {
  const value = await massagePromiseWithTimer;
  console.log(value);
}

handlePromiseWithTimer();
// o/p: Promise resolved successfully with async & await + Timer.

// ----------------------------------------------------------------
// Why use async-await function?

const massagePromise4 = new Promise((resolve, reject) => {
  setTimeout(() => {
    resolve("Promise4 resolved successfully.");
  }, 1000);
});

// Execution of Promise in normal way:
function handlePromiseInNormalWay() {
  massagePromise4.then((res) => {
    console.log(res);
  });
  console.log("Promise handle in normal way");
}

handlePromiseInNormalWay();
// o/p:
// Promise handle in normal way
// Promise4 resolved successfully.

// Problem: JS engin will not wait for promise result and it will execute code line by line and when it gets promises result it will print at that time --> It will leeds to inconsistances in case of "order of execution metters."

// ----------------------------------------------------------------
// Execution of promise function with async-await
async function handlePromiseWithAsynAwait() {
  const value = await massagePromise4;
  console.log(value);
  console.log("Inside async & await.");
}

handlePromiseWithAsynAwait();
// o/p:
// Promise4 resolved successfully.
// Inside async & await.

// * If we call multiple time promises into one async-await function then it will return all promises output with only that one promise function time duration
// ex: if we call two times promises within same async-await function then that promise function has timer of 1000ms then our entire async-await function output will be returned after 1000ms

// stop: 28:00
