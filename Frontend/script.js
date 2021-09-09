window.onload = function checkAuthorization(){
    if(sessionStorage.getItem("token")=='Login or Password is incorrect'||sessionStorage.getItem("token")==null){
        if(window.location.pathname != "/index.html"){
            window.location = "index.html";
        }
    }
    else{
        
        switch (window.location.pathname){
            case '/admin.html':
                if(sessionStorage.getItem("role")!="admin")
                window.location = "access.html"
                break;
            case '/moderator.html':
                if(sessionStorage.getItem("role")!="moderator")
                window.location = "access.html"
                break;
            case '/main.html':
                if(sessionStorage.getItem("role")!="user")
                window.location = "access.html"
                break;
            default:
                break;
        }
    }
};
function sendLogin(){
    const url = "https://localhost:5001/api/user/login";
    let login = document.getElementById("login").value;
    let password = document.getElementById("password").value;
    let body = {
        "login": login,
        "password": password
    }     
    body = JSON.stringify(body);  
    fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: body
    })
    .then(res => res.text())
    .then(data => funcLogin(data))
    .catch(err => {
        console.log('Error message', err)
    });
   
}
function sendRegister(){
    const url = "https://localhost:5001/api/user/register";
    let login = document.getElementById("loginR").value;
    let password = document.getElementById("passwordR1").value;
    let cPassword = document.getElementById("passwordR2").value;
    let body = {
            "login": login,
            "password": password,
            "confirmPassword": cPassword
        }
    body = JSON.stringify(body);
    console.log(body);  
    fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: body
    })
    .then(res => funcRegister(res.status))
    .catch(err => {
        console.log('Error message', err)
    });
}
function funcRegister(res){
    if(res==201){
        if(document.getElementById("invalid")){
            showElement("invalid");
            document.getElementById("invalid").innerHTML="Registration was successful! Please log-in.";
        }
        if(document.getElementById("loginDiv")){
            showElement("loginDiv")
        }  
    }
    else{
        if(document.getElementById("invalid")){
            showElement("invalid");
            document.getElementById("invalid").innerHTML="Login is taken or passwords are not the same.";
        }
    }
}
function funcDeleteUser(res){
    if(res==204){
        document.getElementById("invalid").style.display="block";
        document.getElementById("invalid").innerHTML="User Deleted!";
    }
    else{
        showElement("invalid");
        document.getElementById("invalid").innerHTML="Something went wrong. User not Deleted.";
    }
}
function funcRegisterAdmin(res){
    if(res==201){
        document.getElementById("invalidR").style.display="block";
        document.getElementById("invalidR").innerHTML="Account created!";
    }
    else{
        showElement("invalidR");
        document.getElementById("invalidR").innerHTML="Something went wrong. Account not created.";
    }
}
function funcLogin(data){
    sessionStorage.setItem("token", data);
    if(sessionStorage.getItem("token")!='Login or Password is incorrect'){
        const jwt = parseJwt(sessionStorage.getItem("token"));
        sessionStorage.setItem("role", jwt['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']);
        sessionStorage.setItem("userId", jwt['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']);
        sessionStorage.setItem("user", jwt['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']);
        switch (jwt) {
           case "user":
               window.location = "main.html"
               break;
            case "moderator":
               window.location = "moderator.html"
               break;
            case "admin":
               window.location = "admin.html"
               break;
           default:
               break;
       }
    }
    document.getElementById("invalid").style.display="block";
    document.getElementById("invalid").innerHTML = 'Login or Password is incorrect';
}
function showElement(id){
    switch (id) {
        case "loginDiv":
            document.getElementById("registerDiv").style.display="none";
            break;
    case "registerDiv":
            document.getElementById("loginDiv").style.display="none";
            break;
        default:
            break;
    }
    document.getElementById(id).style.display="block";
}
function getUsers(){
    console.log(sessionStorage.getItem("token"));
    const url = "https://localhost:5001/api/user";
    fetch(url)
    .then(res => res.json())
    .then(data => console.log(data))
    .catch(err => {
        console.log('Error message', err)
    });
}
function parseJwt (token) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(atob(base64).split('').map(function(c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));
    return JSON.parse(jsonPayload);
    
}
function logout(){
    sessionStorage.setItem("token", null);
    sessionStorage.setItem("role", null);
    window.location="index.html";
}
function deleteUser(){
    if(sessionStorage.getItem("role")!="admin"){
        logout();
    }
    let userId = document.getElementById("deleteUserId").value;
    const url = "https://localhost:5001/api/user/"+userId;
    fetch(url, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'bearer ' + sessionStorage.getItem("token")
        }
    })
    .then(res => funcDeleteUser(res.status))
    .catch(err => {
        console.log('Error message', err)
    });
}
function sendRegisterAdmin(){
    if(sessionStorage.getItem("role")!="admin"){
        logout();
    }
    const url = "https://localhost:5001/api/user/register";
    let login = document.getElementById("loginR").value;
    let password = document.getElementById("passwordR1").value;
    let cPassword = document.getElementById("passwordR2").value;
    let body = {
            "login": login,
            "password": password,
            "confirmPassword": cPassword,
            "role": "admin"
        }
    body = JSON.stringify(body);
    console.log(body);  
    fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: body
    })
    .then(res => funcRegisterAdmin(res.status))
    .catch(err => {
        console.log('Error message', err)
    });
}
function sendRegisterModerator(){
    if(sessionStorage.getItem("role")!="admin"){
        logout();
    }
    const url = "https://localhost:5001/api/user/register";
    let login = document.getElementById("loginR").value;
    let password = document.getElementById("passwordR1").value;
    let cPassword = document.getElementById("passwordR2").value;
    let body = {
            "login": login,
            "password": password,
            "confirmPassword": cPassword,
            "role": "moderator"
        }
    body = JSON.stringify(body);
    console.log(body);  
    fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: body
    })
    .then(res => funcRegisterAdmin(res.status))
    .catch(err => {
        console.log('Error message', err)
    });
}
function muteUser(){
    if(sessionStorage.getItem("role")!="moderator"){
        logout();
    }
    let userId = document.getElementById("muteUserId").value;
    const url = "https://localhost:5001/api/user/"+userId+"/mute";
    fetch(url, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'bearer ' + sessionStorage.getItem("token")
        }
    })
    .then(res => funcUserModification(res.status))
    .catch(err => {
        console.log('Error message', err)
    });
}
function funcUserModification(res){
    if(res==200){
        document.getElementById("invalid").style.display="block";
        document.getElementById("invalid").innerHTML="User Muted!";
    }
    else{
        document.getElementById("invalid").style.display="block";
        document.getElementById("invalid").innerHTML="Something went wrong. User not muted.";
    }
}

function getMessages(){
    if(sessionStorage.getItem("role")!="moderator"){
        logout();
    }
    const url = "https://localhost:5001/api/message";
    fetch(url)
    .then(res => res.json())
    .then(data => printMessages(data))
    .catch(err => {
        console.log('Error message', err)
    });
}
function printMessages(data){
    document.getElementById("messageTBody").innerHTML = "";
    data.forEach(element => {
        var tr = document.createElement("tr");
        Object.keys(element).forEach(function(key){
            var td = document.createElement("td");
            td.appendChild(document.createTextNode(element[key]));
            tr.appendChild(td);
        });
        document.getElementById("messageTBody").appendChild(tr);
    });
    document.getElementById("messages").style.display="block";
}

function getUserMessages(){
    if(sessionStorage.getItem("role")!="moderator"){
        logout();
    }
    let userId = document.getElementById("messageUserId").value;
    const url = "https://localhost:5001/api/message/"+userId;
    fetch(url, {
        headers: {
            'Authorization': 'bearer ' + sessionStorage.getItem("token")
        }
    })
    .then(res => res.json())
    .then(data => printUserMessages(data))
    .catch(err => {
        console.log('Error message', err)
    });
}
function printUserMessages(data){
    document.getElementById("messageUserTBody").innerHTML = "";
    data.forEach(element => {
        var tr = document.createElement("tr");
        Object.keys(element).forEach(function(key){
            var td = document.createElement("td");
            td.appendChild(document.createTextNode(element[key]));
            tr.appendChild(td);
        });
        document.getElementById("messageUserTBody").appendChild(tr);
    });
    document.getElementById("messagesUser").style.display="block";
}

function removeMessage(){
    if(sessionStorage.getItem("role")!="moderator"){
        logout();
    }
    let messageId = document.getElementById("messageId").value;
    const url = "https://localhost:5001/api/message/"+messageId;
    fetch(url, {
        method: 'PUT',
        headers: {
            'Authorization': 'bearer ' + sessionStorage.getItem("token")
        }
    })
    .then(res => funcMessage(res.status))
    .catch(err => {
        console.log('Error message', err)
    });
}
function removeUserMessage(){
    if(sessionStorage.getItem("role")!="moderator"){
        logout();
    }
    let userId = document.getElementById("messageUserIdR").value;
    const url = "https://localhost:5001/api/message/user/"+userId;
    fetch(url, {
        method: 'PUT',
        headers: {
            'Authorization': 'bearer ' + sessionStorage.getItem("token")
        }
    })
    .then(res => funcMessage(res.status))
    .catch(err => {
        console.log('Error message', err)
    });
}
function funcMessage(res){
    if(res==200){
        document.getElementById("invalid").style.display="block";
        document.getElementById("invalid").innerHTML="Action Successful!";
    }
    else{
        showElement("invalid");
        document.getElementById("invalid").innerHTML="Something went wrong.";
    }
}


function getUsers(){
    if(sessionStorage.getItem("role")!="moderator"){
        logout();
    }
    const url = "https://localhost:5001/api/user";
    fetch(url, {
        headers: {
            'Authorization': 'bearer ' + sessionStorage.getItem("token")
        }
    })
    .then(res => res.json())
    .then(data => printUsers(data))
    .catch(err => {
        console.log('Error message', err)
    });
}

function printUsers(data){
    console.log(data);
    document.getElementById("usersTBody").innerHTML = "";
    data.forEach(element => {
        var tr = document.createElement("tr");
        Object.keys(element).forEach(function(key){
            var td = document.createElement("td");
            td.appendChild(document.createTextNode(element[key]));
            tr.appendChild(td);
        });
        document.getElementById("usersTBody").appendChild(tr);
    });
    document.getElementById("users").style.display="block";
}
function getUser(){
    if(sessionStorage.getItem("role")!="moderator"){
        logout();
    }
    let userId = document.getElementById("userId").value;
    const url = "https://localhost:5001/api/user/"+userId;
    fetch(url, {
        headers: {
            'Authorization': 'bearer ' + sessionStorage.getItem("token")
        }
    })
    .then(res => res.json())
    .then(data => printUser(data))
    .catch(err => {
        console.log('Error message', err)
    });
}
function printUser(data){
    console.log(data);
    document.getElementById("userTBody").innerHTML = "";
        var tr = document.createElement("tr");
        Object.keys(data).forEach(function(key){
            var td = document.createElement("td");
            td.appendChild(document.createTextNode(data[key]));
            tr.appendChild(td);
        });
        document.getElementById("userTBody").appendChild(tr);
    document.getElementById("user").style.display="block";
}

function getHero(){
    
}