// const { userInfo, userMassage } = require("./module1");

// const user = userInfo();
// console.log(user);
// // o/p: { name: 'Deadpool', weapon: 'Katana' }

// userMassage(user.name);
// // o/p: Hello, Deadpool

// -----------------------------------
// ES6 module 
import { userInfo, userMassage } from "./module1.js";

const user = userInfo();
console.log(user);
// o/p: { name: 'Deadpool', weapon: 'Katana' }

userMassage(user.name);
// o/p: Hello, Deadpool
