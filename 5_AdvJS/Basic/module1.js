// class X_man {
//   constructor(name, weapon) {
//     this.name = name;
//     this.weapon = weapon;
//   }
// }

// const user1 = new X_man("Deadpool", "Katana");

// const userInfo = () => {
//   return user1;
// };

// function userMassage(name) {
//   console.log(`Hello, ${name}`);
// }

// module.exports = { userInfo, userMassage };

class X_man {
  constructor(name, weapon) {
    this.name = name;
    this.weapon = weapon;
  }
}

const user1 = new X_man("Deadpool", "Katana");

export const userInfo = () => {
  return user1;
};

export function userMassage(name) {
  console.log(`Hello, ${name}`);
}
