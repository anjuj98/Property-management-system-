function checkFullname() {
    var isName = /^[a-zA-Z\s]+$/;
    let fullName = document.getElementById('name');

    if (fullName.value.trim() === "") {
        SetError(fullName, "Full name required");
    }
    else if (!isName.test(fullName.value.trim())) {
        SetError(fullName, "Full name cannot be  numbers or special characters");

    }
    else {
        setSuccess(fullName)
    }
}


function checkEmail() {
    var isEmail = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    let emailField = document.getElementById('email');

    if (emailField.value.trim() === "") {
        SetError(emailField, "Email required");
    }
    else if (!isEmail.test(emailField.value.trim())) {
        SetError(emailField, "Invalid email format");

    }
    else {
        setSuccess(emailField)
    }
}


function checkPhone() {
    var isPhone = /^\d{10}$/;
    let phoneField = document.getElementById('phone');
    if (phoneField.value.trim() === "") {
        SetError(phoneField, "Phone number required");
    } else if (!isPhone.test(phoneField.value.trim())) {
        SetError(phoneField, "Phone number must contain only digits and not allow alphabets");
    } else if (phoneField.value.trim().length !== 10) {
        SetError(phoneField, "Phone number must contain exactly 10 digits");
    } else {
        setSuccess(phoneField)
    }
}


function checkMessage() {
    let messageField = document.getElementById('message');

    if (messageField.value.trim() === "") {
        SetError(messageField, "Please enter your message");
    }
    else {
        setSuccess(messageField)
    }
}
function SetError(input, message) {
    let submitbutton = document.getElementById("button");
    const formControl = input.parentElement;
    const errorDiv = formControl.querySelector('.error-message');
    errorDiv.innerText = message;
    errorDiv.style.display = "block";
    submitbutton.disabled = true;
}

function setSuccess(input) {
    let submitbutton = document.getElementById("button");
    const formControl = input.parentElement;
    const errorDiv = formControl.querySelector('.error-message');
    errorDiv.innerText = "";
    errorDiv.style.display = "none";
    submitbutton.disabled = false;
}



function checkValidation() {
    var isFullNameValid = checkFullname();
    var isEmailValid = checkEmail();
    var isPhoneValid = checkPhone();
    var isMessageValid = checkMessage();

    if (isFullNameValid && isEmailValid && isPhoneValid && isMessageValid) {
        return true;
    } else {
        return false;
    }
}


