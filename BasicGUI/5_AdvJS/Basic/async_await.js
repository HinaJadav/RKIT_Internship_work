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

// ----------------------------------------------------------------

// What happens when we call two different promises within one async-await function?

// Output is depends on the promise timeout value
// 1) if first promise has small timer value then the second promise than first promise result will be returned after it's timeout and second promise will be returned after it's timeout but it will also include the time which we consume during first promise execution.
// 2) if first promise has large timer value then the second promise than after first promise timer both promise will be returned.

const p1 = new Promise((resolve, reject) => {
  setTimeout(() => {
    resolve("Promie p1 resolved successfully.");
  }, 10000);
});

const p2 = new Promise((resolve, reject) => {
  setTimeout(() => {
    resolve("Promie p2 resolved successfully.");
  }, 5000);
});

async function handleTwoDiffPromiseWithAsynAwait() {
  const value1 = await p1;
  console.log(value1);
  console.log("Inside async & await after p1.");

  const value2 = await p2;
  console.log(value2);
  console.log("Inside async & await after p2.");
}

handleTwoDiffPromiseWithAsynAwait();
// o/p:
// Inside async & await.
// Promie p1 resolved successfully.
// Inside async & await after p1.
// Promie p2 resolved successfully.
// Inside async & await after p2.

// ----------------------------------------------------------------

// Reallife example of async-await

const USER_API = "https://api.github.com/users/HinaJadav";

async function handleUserData() {
  // Error handling
  try {
    const data = await fetch(USER_API);

    const jsonData = await data.json();

    console.log(jsonData);
  } catch (error) {
    console.log(error);
  }
}
handleUserData();
// o/p:
// {
//   login: 'HinaJadav',
//   id: 105739881,
//   node_id: 'U_kgDOBk12aQ',
//   avatar_url: 'https://avatars.githubusercontent.com/u/105739881?v=4',
//   gravatar_id: '',
//   url: 'https://api.github.com/users/HinaJadav',
//   html_url: 'https://github.com/HinaJadav',
//   followers_url: 'https://api.github.com/users/HinaJadav/followers',
//   following_url: 'https://api.github.com/users/HinaJadav/following{/other_user}',
//   gists_url: 'https://api.github.com/users/HinaJadav/gists{/gist_id}',
//   starred_url: 'https://api.github.com/users/HinaJadav/starred{/owner}{/repo}',
//   subscriptions_url: 'https://api.github.com/users/HinaJadav/subscriptions',
//   organizations_url: 'https://api.github.com/users/HinaJadav/orgs',
//   repos_url: 'https://api.github.com/users/HinaJadav/repos',
//   events_url: 'https://api.github.com/users/HinaJadav/events{/privacy}',
//   received_events_url: 'https://api.github.com/users/HinaJadav/received_events',
//   type: 'User',
//   user_view_type: 'public',
//   site_admin: false,
//   name: 'Hina Jadav',
//   company: null,
//   blog: '',
//   location: null,
//   email: null,
//   hireable: null,
//   bio: 'Perusing Computer Engineering From DDU  \r\n',
//   twitter_username: null,
//   public_repos: 20,
//   public_gists: 0,
//   followers: 2,
//   following: 3,
//   created_at: '2022-05-17T16:42:38Z',
//   updated_at: '2024-10-20T21:51:05Z'
// }

// --------------------------
// o/p: (If any error occurs like user_url is invalid)
// {
//   message: 'Not Found',
//   documentation_url: 'https://docs.github.com/rest',
//   status: '404'
// }
