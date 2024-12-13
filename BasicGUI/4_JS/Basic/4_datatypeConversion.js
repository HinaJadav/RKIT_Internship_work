let marks = 99;
console.log(typeof marks); // number

marks = "99";
console.log(typeof marks); // string

// dataType conversion
let newMarks = Number(marks);
console.log(typeof newMarks); // number

let email = "email123";
console.log(typeof email); // string

// dataType conversion
let newEmail = Number(email);
console.log(typeof newEmail); // number
console.log(newEmail); // NaN --> hear not able to convert "email123" into number bcz it's not entire number value
// "confusion due to not do proper conversion"

// type conversion for "null" value
let nullVar = null;
let nullConversion = Number(nullVar);
console.log(nullConversion); // 0
// as per above output it will miss lead when we want "null" as output and get "0"

// type conversion for "undefined" value
let undefinedConversion = Number(undefined);
console.log(undefinedConversion); //NaN

// type conversion for "Boolean" values
let booleanVar = false;
let booleanConversion = Number(booleanVar);
console.log(booleanConversion); // 0
let booleanConversionInString1 = String(booleanVar);
console.log(booleanConversionInString1); // false
let booleanConversionInString2 = Number(booleanConversion);
console.log(booleanConversionInString2); // 0
