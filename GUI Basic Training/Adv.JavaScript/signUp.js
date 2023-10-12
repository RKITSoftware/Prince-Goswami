const form = document.querySelector('#sign-up-form');

//#region autoIncrement
function autoIncrement() {
  let id = Number(localStorage.id);
  localStorage.setItem("id", JSON.stringify(id+1));
  return id;
}
//#endregion

const signUp = () =>{

  //#region Parameters
  const UserName = document.querySelector("#userName").value;
  const MobileNo = document.querySelector("#phoneNumber").value;
  const Email = document.querySelector("#email").value;
  const Password = document.querySelector("#password").value;
  const CountryName = document.querySelector("#country").value;
  const StateName = document.querySelector("#state").value;
  //#endregion

//#region User Class
class User {
  constructor(UserName, MobileNo, Email, Password, CountryName, StateName) {
    //ValidateParam(Password, MobileNo);
    this.id = autoIncrement();
    this.UserName = UserName;
    this.MobileNo = MobileNo;
    this.Email = Email;
    this.Password = Password;
    this.CountryName = CountryName;
    this.StateName = StateName;
}
}
//#endregion

//#region Create a new User object.
const user = new User(UserName, MobileNo, Email, Password, CountryName, StateName);
//#endregion

//#region Session
sessionStorage.setItem("user", JSON.stringify(user));
//#endregion 

//#region localStorage
let userList = JSON.parse(localStorage.users);
if (userList == "") {
  userList = [];
}
//const userList = JSON.parse(userJSON);
userList.push(user);
const userJSONUpdated = JSON.stringify(userList);
localStorage.setItem("users", userJSONUpdated);
//#endregion 
  
// Redirect to the home page.
    e.preventDefault();
  window.location.href = "Index.html";
};


export default signUp;