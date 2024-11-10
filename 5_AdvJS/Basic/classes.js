// class = special function in JS

// class can be define in two ways:
// 1) class declaration
class X_man {
  constructor(name, weapon) {
    this.name = name;
    this.weapon = weapon;
  }
}

const user1 = new X_man("Deadpool", "Katana");
console.log(user1);
// o/p: X_man { name: 'Deadpool', weapon: 'Katana' }

// 2) class expression

// class without name
const Avengers = class {
  constructor(name, weapon) {
    this.name = name;
    this.weapon = weapon;
  }
};

const user2 = new Avengers("Iron Man", "Iron Man suit");
console.log(user2);
// o/p: Avengers { name: 'Iron Man', weapon: 'Iron Man suit' }

const Avengers1 = class Marvel {
  constructor(name, weapon) {
    this.name = name;
    this.weapon = weapon;
  }
};

const user3 = new Avengers1("Thor", "hammer");
console.log(user3);
// o/p: Marvel { name: 'Thor', weapon: 'hammer' }

// ----------------------------------------------------------------

// static keyword:

// Static class: JS class which includes only static properties and methods
class WonderWoman {
  static weapon = "lasso";

  static print() {
    console.log("Hello wonderWoman");
  }
}

console.log(WonderWoman.weapon);
// o/p: lasso

// We can call static method by using direct class name no need to create instance of class
WonderWoman.print();
// o/p: Hello wonderWoman

//----------------------------------------------------------------

// private properties:

class Hulk {
  #isNotWeapon = true;

  static print(isNotWeapon) {
    console.log("No, hulk has not weapon.");
  }
}

Hulk.print();
// o/p: No, hulk has not weapon.

// ----------------------------------------------------------------

// Inheritance: using "extend" keyword

class MarvelHeros {
  constructor(name) {
    this.name = name;
  }

  print() {
    console.log(`Welcome in wor ${this.name}`);
  }
}

class AvengersHeros extends MarvelHeros {
  constructor(name, weapon) {
    super(name);
    this.weapon = weapon;
  }

  print() {
    console.log(`${this.name} comes with ${this.weapon}`);
  }
}

const man = new MarvelHeros("Deadpool");
man.print();
// o/p:
// Welcome in wor Deadpool

const deadpool = new AvengersHeros("Deadpool", "Katana");
deadpool.print();

// o/p:
// Deadpool comes with Katana
