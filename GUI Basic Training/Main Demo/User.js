
function generateRandomNumericId(length) {
    let id = '';

    for (let i = 0; i < length; i++) {
        const randomNumber = Math.floor(Math.random() * 10); // Generate a random number between 0 and 9
        id += randomNumber;
    }

    return id;
}
class User {
    //global api
    static url = "https://65449cca5a0b4b04436c98a9.mockapi.io/quotes/users";

    //consructur
    //Creates an instance of User.
    constructor(userName, email, password) {
        this.createdAt = new Date();
        this.userName = userName;
        this.email = email;
        this.password = password;
        this.favourites = [];
        this.storageType = "";
        this.id = generateRandomNumericId(3); //
    }
    
    //logOut
    static logOut(storageType) {
        //check for storage type and remove it 
        if (storageType === 'localStorage') {
            localStorage.removeItem('user'); //for local storage
        }
        else {
            sessionStorage.removeItem('user');//for session storage
        }
    }

    //fetch users    
    static async #fetchUsers() {
        try {
            const response = await fetch(this.url);

            if (response.ok) {
                // If the request was successful
                let data = await response.json();
                // console.log(data);
                // Handle success and return the JSON data
                return data;
              } else {
                // If the response status is not in the range 200-299, it means there was an error
                // Throw an error with an appropriate message
                throw new Error(`Error: ${response.status} - ${response.statusText}`);
              }
        } catch (error) {
            // console.error(error);
            throw error;
        }
    }

    //set users
    static async #setUser(userList) {
        try {
            let response = await fetch(this.url, {
              method: "POST",
              headers: {
                "Content-Type": "application/json",
              },
              body: JSON.stringify(userList),
            });
          
            if (response.ok) {
              // If the request was successful
              let data = await response.json();
              // Handle success and return the JSON data
              return data;
            } else {
              // If the response status is not in the range 200-299, it means there was an error
              // Throw an error with an appropriate message
              throw new Error(`Error: ${response.status} - ${response.statusText}`);
            }
          } catch (error) {
            // Handle any exceptions or network errors
            throw new Error(`Network error: ${error.message}`);
          }
        }                

    //set the user storage    
    static #setUserStorage(user) {
            if (user.storageType === 'localStorage') {
            //if remember me set local storage
            localStorage.setItem("user", JSON.stringify(user));
        }    
        else {
            //set session storage
            sessionStorage.setItem("user", JSON.stringify(user));
        }    
    }    

    //login
    static async logIn(email, password, storageType) {
        try {
            // Verify user
            let currUser = null;
            //fetch records from api
            let users = await this.#fetchUsers();
            for (const user of users) {
                //verrify parameters
                if (user.email === email && user.password === password) {
                    currUser = user;
                    break;
                }    
            }    
            //if verified set user
            if (currUser) {
                //change user storagetype
                currUser.storageType = storageType;
                this.#setUserStorage(currUser); // set user
                // console.log("User logged in successfully!" + currUser.userName);
                return currUser;
            } else {
                alert("User not found");
                // console.log("Invalid email or password.");
            }    
        } catch (e) {
            // Show error message
            console.error(e);
        }    
    }    
    
    //signup
    // Sign up the user. 
    //This is called when the user clicks the sign up button
    static async signUp(user) {
        //fetch data from api
            /// Fetch and parse user list.
            let userList = "";
            let users = await this.#fetchUsers();
            // This is called after a request is made and the list is ready to be sent
            userList = users;
            // console.log(userList);
            
            // Push new object into data
            userList.push(user);
            
            // Push updated data into the API
            this.#setUser(user);
            
            // Set user storage
            this.#setUserStorage(user);

    }
}

export default User;
