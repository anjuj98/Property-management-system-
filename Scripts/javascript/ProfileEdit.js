
function checkFirstName() {
    var isName = /^[a-zA-Z]+$/;
    let name = document.getElementById('firstname');
    if (name.value.trim() === "")
    {
        SetError(name, "First name is required");
    }
    else if (!isName.test(name.value.trim()))
    {
        SetError(name, "First name cannot be  numbers or special characters");
    }
    else
    {
        setSuccess(name)
    }
}

function checkLastName() {
    var isName = /^[a-zA-Z]+$/;
    let name = document.getElementById('lastname');
    if (name.value.trim() === "") {
        SetError(name, "Last name is required");
    }
    else if (!isName.test(name.value.trim())) {
        SetError(name, "Last name cannot be  numbers or special characters");
    }
    else {
        setSuccess(name)
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

function checkAddress()
{
    let addressField = document.getElementById("address");
    if (addressField.value.trim() === "") {
        SetError(addressField, "Address is required");
    }
    else {
        setSuccess(addressField)

    }
}

function checkPincode() {
    var isPincode = /^[0-9]{6}$/;
    let pincodeField = document.getElementById("pincode");
    if (pincodeField.value.trim() === "") {
        SetError(pincodeField, "Pincode is required");
    } else if (!isPincode.test(pincodeField.value.trim())) {
        SetError(pincodeField, "Pincode must contain six digits and not allow alphabets");
    }
    else {
        setSuccess(pincodeField)

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