onload = () => {
    // Retrieve email and password from cookies.  
    const user = JSON.parse(sessionStorage.getItem("user"));
    if(user) {
    // Redirect to the home page.
    window.location.href = "./success.html";
    }
  };
  

const form = document.querySelector("form");

form.addEventListener("submit", (event)=>{
    // Retrieve email and password from cookies.
    const emailInput = document.querySelector("input[type=email]").value;
    const passwordInput = document.querySelector("input[type=password]").value;
  
    // Get the email and password cookies.
    // const emailCookie = document.cookie.match(/email=([^;]+)/);
    // const passwordCookie = document.cookie.match(/password=([^;]+)/);
    const user = JSON.parse(localStorage.getItem("users"));
    
    // If the cookies are found, fill the input fields.
    
    user.forEach(element => {
      console.log(element.Email, emailInput);
      if (element.Email == emailInput && element.Password == passwordInput) {
      
      sessionStorage.setItem("user", JSON.stringify(element));
      
      event.preventDefault();    
      // Redirect to the home page.
      window.location.href = "./success.html";
      }
    });
  return;    
}
);

