
const form = document.getElementById("myForm");
let id = 0;
localStorage.setItem("id", id);

form.addEventListener("submit", (e) => {

//#region Parameters
  const FirstName = document.querySelector("input[name=FirstName]").value;
  const LastName = document.querySelector("input[name=LastName]").value;
  const MobileNo = document.querySelector("input[type=number]").value;
  const Email = document.querySelector("input[type=email]").value;
  const Password = document.querySelector("input[type=password]").value;
  const CountryName = document.querySelector("#country").value;
  const StateName = document.querySelector("#state").value;
//#endregion

//#region User Class
class User {
  constructor(firstName, lastName, MobileNo, Email, Password, CountryName, StateName) {
    //ValidateParam(Password, MobileNo);
    this.id = autoIncrement(id);
    this.firstName = firstName;
    this.lastName = lastName;
    this.MobileNo = MobileNo;
    this.Email = Email;
    this.Password = Password;
    this.CountryName = CountryName;
    this.StateName = StateName;
  }
}
//#endregion

//#region autoIncrement
function autoIncrement() {
  id = localStorage.getItem("id", id);
  localStorage.setItem("id", id++);
  return id;
}
//#endregion

//#region Create a new User object.
const user = new User(FirstName, LastName, MobileNo, Email, Password, CountryName, StateName);
//#endregion

  
//#region Session
sessionStorage.setItem("user", JSON.stringify(user));
//#endregion 

//#region localStorage
let userJSON = localStorage.getItem("users");
if (userJSON == "") {
  userJSON = [];
}
const userList = JSON.parse(userJSON);
userList.push(user);
const userJSONUpdated = JSON.stringify(userList);
localStorage.setItem("users", userJSONUpdated);
//#endregion 
  
  // Redirect to the home page.
    e.preventDefault();
  window.location.href = "success.html";
});

let clearAllCookies = () => {
  // Get all cookies.
  let cookies = document.cookie.split(";");

  // Iterate through all cookies and delete them.
  for (let i = 0; i < cookies.length; i++) {
    document.cookie = cookies[i] + "=; expires=Thu, 01 Jan 1970 00:00:00 GMT";
  }
}