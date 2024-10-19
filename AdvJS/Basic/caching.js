// Apply caching in FIBONACCI problem for memorized results so no need to recalc

let fibonacci = {}; // cache object

function getFibonacci(num) {
  console.log(fibonacci);
  if (num === 0 || num === 1) {
    return num;
  } else if (fibonacci[num]) {
    console.log("Cached value used");
    return fibonacci[num];
  } else {
    fibonacci[num] = getFibonacci(num - 1) + getFibonacci(num - 2);

    return fibonacci[num];
  }
}

console.log(getFibonacci(6)); // Outputs 8
console.log(getFibonacci(6)); // Uses cached value, outputs 8

// Output:
// {}
// {}
// {}
// {}
// {}
// {}
// {}
// { '2': 1 }
// { '2': 1, '3': 2 }
// Cached value used
// { '2': 1, '3': 2, '4': 3 }
// Cached value used
// { '2': 1, '3': 2, '4': 3, '5': 5 }
// Cached value used
// 8
// { '2': 1, '3': 2, '4': 3, '5': 5, '6': 8 }
// Cached value used
// 8
