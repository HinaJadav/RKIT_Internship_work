// == (Loose Equality):

console.log(5 == "5");
// o/p: true (string '5' is coerced to number 5)
console.log(true == 1);
// o/p: true (true is coerced to number 1)
console.log(null == undefined);
// o/p: true (they are loosely equal)

// ----------------------------------------------------------------
// === (Strict Equality):

console.log(5 === "5");
// o/p: false (no type coercion, different types)
console.log(true === 1);
// o/p: false (different types: boolean and number)
console.log(null === undefined);
// o/p: false (different types)
console.log("Hina" === "Hina");
// o/p: true

// ----------------------------------------------------------------
// != (Loose Inequality):

console.log(5 != "5");
// o/p: false (because '5' is coerced to 5)
console.log(true != 1);
// o/p: false (true is coerced to 1)

// ----------------------------------------------------------------
// !== (Strict Inequality):
console.log(5 !== "5");
// o/p: true (different types)
console.log(true !== 1);
// o/p: true (different types: boolean vs number)

// ----------------------------------------------------------------
// (== and !=) : allow type coercion, meaning they compare values after converting them to a common type.

// (=== and !==) : are strict, meaning they check both the type and the value without type coercion.
