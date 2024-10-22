// this: give information about current context
// In node environment "this" points to the "{}" empty object
// In Browser "this" points to the "Window" object

// arrow function:
// basic syntax: () => {}

class X_man {
  constructor(name, weapon) {
    this.name = name;
    this.weapon = weapon;
  }
}

const user1 = new X_man("Deadpool", "Katana");

// Implicitly return using arrow function (no need to use "return" keyword)
const userInfo = () => user1;

// Explicitly return using arrow function
const userMassage = (name) => {
  console.log(`Hello, ${name}`);
};

const user = userInfo();
userMassage(user.name);
// o/p:
// Hello, Deadpool
