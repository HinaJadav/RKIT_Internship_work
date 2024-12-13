// OOPs

// Object
const user = {
  // properties
  userName: "Priyank",
  email: "priyank@gmail.com",
  address: "Gujarat, India",

  // methods
  getUserDetails: function () {
    // "this" : declare current context
    console.log(`UserName: ${this.userName}`);
    console.log(this);
  },
};

console.log(user.getUserDetails());

console.log(this); // {}
// "this" keyword: in local scope it will give present all object
// In global scope it will give "window obj"

// ------------------------------------------------------------------------------------------------
// Construction function in class: It is used for makes multiple new objects from same class(obj literal)

function createUser(username, email, password) {
  this.username = username;
  this.email = email;
  this.password = password;

  return this;
}

const user1 = createUser("Nahii", "nahii@gmail.com", "nahii123");

// console.log(user1);
// username: 'Nahii',
// email: 'nahii@gmail.com',
// password: 'nahii123'

const user2 = createUser("PU", "pu@gmail.com", "pu123");
console.log(user1);
// username: 'Nahii',
// email: 'nahii@gmail.com',
// password: 'nahii123'

// problem: user1 all values overwrite by user2 all values
// It is due to not creating new object and use current object and update it's values

// Solution: "new" keyword

const user3 = new createUser("Madhu", "madhu@gmail.com", "madhu123");

const user4 = new createUser("Anu", "anu@gmail.com", "anu123");

console.log(user3);
console.log(user4);
// output:
// createUser {
//     username: 'Madhu',
//     email: 'madhu@gmail.com',
//     password: 'madhu123'
// }
// createUser {
//     username: 'Anu',
//     email: 'anu@gmail.com',
//     password: 'anu123'
// }

// function is indirectly has Object datatype so we can set prototype for this also

createUser.prototype.printMassage = function () {
  console.log(`Welcome, ${this.username}`);
};

const user5 = createUser("Anu1", "anu1@gmail.com", "anu123");
console.log(user5);

// user5.printMassage();
// output: const user4 = new createUser("Anu", "anu@gmail.com", "anu123");
// Here it gives error because user5 object not recognise the method printMassage

// Solution: "new" keyword
// Using "new" keyword we can introduced our included methods into prototype

const user6 = new createUser("Anu1", "anu1@gmail.com", "anu123");
console.log(user6);
user6.printMassage();
// output:
// createUser {
//     username: 'Anu1',
//     email: 'anu1@gmail.com',
//     password: 'anu123'
// }
// Welcome, Anu1

// ----------------------------------------------------------------
// object is indirectly work as super class
Object.prototype.welcomeMassage = function () {
  console.log(`Welcome user!, ${this.username}`);
};

user6.welcomeMassage(); //output: Welcome user!, Anu1
// here, we are able to access those  prototype which we not declare into user6's prototype but we declare into Object prototype which is superset of all objects, arrays,functions

Array.welcomeMassage(); //output: Welcome user!, undefined

// ----------------------------------------------------------------

// __proto__: use to provide inheritance because using it one object able to access properties of another object which ref gives it by __proto__

const human = {
  isHarmful: true,
};

const man = {
  name: "PU",
  //   __proto__: human, // using this man class also able to access all properties and methods of human class
};

// console.log(man.name); //PU
// console.log(man.isHarmful); //true

// Modern Syntex for this type inheritance
Object.setPrototypeOf(man, human);
console.log(man.name); //PU
console.log(man.isHarmful); //true