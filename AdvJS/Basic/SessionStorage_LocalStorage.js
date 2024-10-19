let key = prompt("Enter cookie key");
let value = prompt("Enter cookie value");
document.cookie = key + "=" + value;

// encodeURIComponent() : encode value of cookies key-value before store cookie, For improve security
// decodeURIComponent() : decode value of cookies key-value after store
// Store the encoded key-value pair
// document.cookie = `${encodeURIComponent(key)}=${encodeURIComponent(value)}`;

// set cookie expire time.
document.cookie = "UserName = Nahii; expires=Sat, 19 Oct 2024 09:35:00 GMT";
// This will output the decoded value in the console (browser decodes it automatically when reading cookies)
console.log(document.cookie);
