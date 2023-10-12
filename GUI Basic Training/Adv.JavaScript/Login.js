onload = () => {
  // Retrieve email and password from cookies.  
  const user = JSON.parse(sessionStorage.getItem("user"));
  if(user) {
  // Redirect to the home page.
  window.location.href = "Index.html";
  }
};


//#region showError
function showError(input,errorMessage) {
  input.parentElement.classList.add('error');
  input.focus();
  input.value = '';
  input.placeholder = errorMessage;
}
//#endregion

//#region success
function success(input) {
  input.parentElement.classList.remove('error');
}
//#endregion


const loginForm = document.querySelector("#sign-in-form");

loginForm.addEventListener("submit", (event)=>{
  // Retrieve email and password from cookies.
  const userNameInput = document.querySelector("#login-userName").value;
  const passwordInput = document.querySelector("#login-password").value;

  // Get the email and password cookies.
  // const emailCookie = document.cookie.match(/email=([^;]+)/);
  // const passwordCookie = document.cookie.match(/password=([^;]+)/);
  const user = JSON.parse(localStorage.getItem("users"));
  
  // If the cookies are found, fill the input fields.
  let userFlag = false,pswdFlag = false;
  let User = user.every(element => {
    console.log(element.UserName, userNameInput);
    if (element.UserName == userNameInput && element.Password == passwordInput) {
      userFlag = true;
      pswdFlag = true;
      return element;
    }
    else if (element.UserName == userNameInput){
      userFlag = true;
    }
    else if (element.Password == passwordInput){
      pswdFlag = true;
    }
  });
  if(userFlag && pswdFlag) {
    sessionStorage.setItem("user", JSON.stringify(User));  
    event.preventDefault();    
    // Redirect to the home page.
    window.location.href = "Index.html";
  }
  else if(userFlag){
    success(document.querySelector("#login-userName"));
    showError(document.querySelector("#login-Password"),"Password Invalid");
  }
  else {
    showError(document.querySelector("#login-password"),"Password Invalid");
    showError(document.querySelector("#login-userName"),"userName Invalid");

  }
return;    
}
);

