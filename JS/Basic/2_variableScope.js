// Variable scope:

// 1) var:
function exampleVar() {
  if (true) {
    var testVar = "Hello from var";
  }
  console.log(testVar); // output: "Hello from var" because var is function-scoped
}

exampleVar();

// This will give an error if uncommented because testVar is not defined globally
// console.log(testVar);

// => Scope of "var" variable is either Global or functional scope.

// ----------------------------------------------------------------

// 2) let:
function exampleLet() {
  if (true) {
    let testLet = "Hello from let";
    console.log(testLet); // output: "Hello from let"
  }

  // This will throw an error because testLet is block-scoped and can't be accessed outside the block
  // console.log(testLet);
}

exampleLet();

// ----------------------------------------------------------------
function exampleConst() {
  if (true) {
    const testConst = "Hello from const";
    console.log(testConst); // output: "Hello from const"
  }

  // This will throw an error because testConst is block-scoped
  // console.log(testConst);
}

exampleConst();

// => "const" and "let" : variable declared using this have : block scope

// ----------------------------------------------------------------

var first;
console.log(first); // output: undefined

let second;
console.log(second); // output: undefined

// const third; // output: SyntaxError: Missing initializer in const declaration
// console.log(third);

// => "var" and "let" : variable declared without intialization but "const" variable can't be declared without intialization

// ----------------------------------------------------------------
