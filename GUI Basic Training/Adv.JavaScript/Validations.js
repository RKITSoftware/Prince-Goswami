import signUp from './signUp.js';

//#region Parameters
let signupForm = document.querySelector('#sign-up-form');
let UserName = document.querySelector("#userName");
let MobileNo = document.querySelector("input[id='phoneNumber']");
let Email = document.querySelector("#email");
let Password = document.querySelector("#password");
let Country = document.querySelector("#country");
let State = document.querySelector("#state");
//#endregion

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

//#region validation calls
Email.addEventListener('change',checkEmail);
 function checkEmail(){
        const re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        if(!re.test(Email.value.trim())) {
            showError(Email,"Invalid email address");
        }
        else {
            success(Email);
        }
 }

MobileNo.addEventListener("change",()=>{
    const re = /^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$/
    if(!re.test(MobileNo.value.trim())) {
        showError(MobileNo,"Invalid number");
    }
    else {
        success(MobileNo);
    }
});

State.addEventListener('click',()=>{
    if(Country.value == -1){
        showError(Country,"please select a country");
    }
});

Password.addEventListener('change',()=>{
    const re = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/
    if(re.test(Password.value.trim())) {
        success(Password)
    }else {
        showError(Password,'Password is invalid');
    }
});
//#endregion

//#region get FieldName
function getFieldName(input) {
    return input.id.charAt(0).toUpperCase() + input.id.slice(1);
}
//#endregion

//#region onSubmit event
signupForm.addEventListener('submit', async () => {
    try{

        const data = await checkRequired([UserName,Email,Password,MobileNo,Country,State])
        signUp();
    }
    catch(error){
        console.log(error);
    }
});
//#endregion

//#region checkRequired 
function checkRequired(inputArr,form) 
{
    return new Promise((resolve, reject) => {
    let val = inputArr.every(function(input){
        if(input.value.trim() === ''){
            showError(input,`${getFieldName(input)} is required`)
            return false;
        }else {
            success(input);
            return true;
        }
    });
    if(val){
        return resolve();
    }
    else{
        return reject(new Error(`${getFieldName(input)} is required`));
    }
    });
}
    //#endregion
