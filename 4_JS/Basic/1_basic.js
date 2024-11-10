// JS is synchronous single thrreaded language

// 3 dufferent type of declare variable: var, let, const

// 1) const
const massage1 = "hello";
console.log(massage1);
// output: hello

// try to update variable first:
// massage1 = "How are you?";
// console.log(massage1);
// output : TypeError: Assignment to constant variable.
// we can't update variable value which declare using "const"

// 2) let:
let massage2 = "hello from let";
console.log(massage2);

// try to update variable value :
massage2 = "How are you?";
console.log(massage2);
// output: How are you?

// try to redeclare variable:
// let massage2 = "I am fine"; // SyntaxError: Identifier 'massage2' has already been declared

// 3) var:
var massage3 = "Hello from var";
console.log(massage3); // Hello from var

// try to update variable value:
massage3 = "How are you?";
console.log(massage3); // How are you?

// try to redeclare variable:
var massage3 = "I am fine";
console.log(massage3); // I am fine

// -----------------------------------------------------------------------------------------------

// Categories of Datatypes:

// 1) Primitive types: (Call by value, fetch data value into memory and updation done on that fetched data value copy)[7] : String, Boolean, Number, Null, undefined, Symbol, Bigint

// 2) Non-primitive types: (Call by refernce) : Arrays, Object, Functions

// This categories given based on how data store in memory or how we access that data from memory like, call by reference or call by value

// -----------------------------------------------------------------------------------------------

// JS is Dynamically typed langusge : Because in JS type checking for variable value, function's argumrnts and return type is checked at Runtime and also here no need to declare variable with specific datatype.

// -----------------------------------------------------------------------------------------------

// Non-primitive datatypes:
// => all this type pf datatypes, typeof output = object in case of function it returns function which means function object

// Array:

const names = ["Hema", "Nahii", "Raha"];

console.log(typeof names); // object

// Object:

const car = {
  name: "BMW",
  color: "black",
};

console.log(typeof car); // object

// function(variable type declaration)

const printHello = function () {
  console.log("Hello");
};

printHello(); // Hello

console.log(typeof printHello); // function (function object)
