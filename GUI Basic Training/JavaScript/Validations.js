// //#region Validations  
//   // Check the length of the Password
//   function ValidateParam(Password, MobileNo){

//   if (Password.length < 8) {
//     alert("Password length must be atleast 8 characters.");
//     e.preventDefault(); // Prevent the form from submitting.
//     return;
//   }

//   // Check if the Password contains at least one uppercase letter
//   if (!/[A-Z]/.test(Password)) {
//     alert("Password must contains one Upper Case character..");
//     e.preventDefault(); // Prevent the form from submitting.
//     return;
//   }

//   // Check if the Password contains at least one lowercase letter
//   if (!/[a-z]/.test(Password)) {
//     alert("Password must contains one lower character..");
//     e.preventDefault(); // Prevent the form from submitting.
//     return;
//   }

//   // Check if the Password contains at least one digit
//   if (!/[0-9]/.test(Password)) {
//     alert("Password must contains numeric character..");
//     e.preventDefault(); // Prevent the form from submitting.
//     return;
//   }
//   // Check if the Password contains at least one special character
//   if (!/[!@#$%^&*()_+~]/.test(Password)) {
//     alert("Password must contains one special character..");
//     e.preventDefault(); // Prevent the form from submitting.
//     return;
//   }
//     // Check if the mobile number is valid
//     if (MobileNo.length != 10) {
//       alert("Mobile number invalid.");
//       e.preventDefault(); // Prevent the form from submitting.
//       return;
//     }
//   }
// //#endregion


//#region Parameters
const FirstName = document.querySelector("input[name=FirstName]");
const LastName = document.querySelector("input[name=LastName]");
const MobileNo = document.querySelector("input[type=number]");
const Email = document.querySelector("input[type=email]");
const Password = document.querySelector("input[type=password]");
const CountryName = document.querySelector("#country");
const StateName = document.querySelector("#state");
//#endregion


Email.addEventListener("change",checkEmail(Email));
    


function checkEmail (input) {
    const re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    if(re.test(Email.value.trim())) {
        //Email.value = '';
        Email.parentElement.childNodes[7].style.display = "none";
    }
    else{
        showError(Email, "Invalid");
    }
}

function showError(input,errorMessage){
    input.parentElement.childNodes[7].style.display = "block";
    input.parentElement.childNodes[7].textContent = errorMessage
}