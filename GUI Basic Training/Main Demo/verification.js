import User from './User.js';

$.validator.addMethod("regex", function (value, element, param) {
    return this.optional(element) ||
        value.match(typeof param === "string" ? new RegExp(param) : param);
});


$("#login-form").validate({
    errorClass: 'error',
    rules: {
        email: {
            required: true,
            regex: "^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$",
        },
        password: {
            required: true,
            minlength: 6
        }
    },
    messages: {
        email: {
            required: "Please enter your email",
            regex: "Please enter a valid email address"
        },
        password: {
            required: "Please enter a password",
            minlength: "Password must be at least 6 characters long"
        }
    }
});

$('#login-btn').click(async () => {
    let password = $('input[type="password"]:first').val();
    let email = $('input[type="email"]:first').val();
    let storageType = $('input[name="rememberMe[]"]').checked ? 'localStorage' : 'sessionStorage';
    let user = await User.logIn(email, password, storageType);
    // form.preventDefault();
    if (user) {
        window.location.href = "Index.html";
    }
});

$("#signup-form").validate({
    errorClass: 'error',
    rules: {
        username: "required",
        email: {
            regex: "^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$",
            required: true,
        },
        password: {
            required: true,
            minlength: 6
        },
        "confirmPassword": {
            required: true,
            equalTo: "#password"
        }
    },
    messages: {
        username: "Please enter your username",
        email: {
            required: "Please enter your email </br>",
            regex: "Please enter a valid email address"
        },
        password: {
            required: "Please enter a password",
            minlength: "Password must be at least 6 characters long"
        },
        "confirmPassword": {
            required: "Please confirm your password",
            equalTo: "Passwords do not match"
        }
    }
});


$('#signup').on('click', signup);

async function signup() {
    console.log('signup');
    // Form is valid, you can submit it here
    let username = $('#userName').val();
    let password = $('input[type="password"]:last').val();
    let email = $('input[type="email"]:last').val();

    //make user and signup
    let user = new User(username, email, password)
    User.signUp(user);
    
}
