var currentLocation = window.location.href;
var links = document.getElementsByTagName("a");
for (var i = 0; i < links.length; i++) {
  if (links[i].href === currentLocation) {
    links[i].className += " active";
  }
}

const userField = document.getElementById("user");
userField.innerText = "Welcome, " + localStorage.getItem("playerName");

const logoutButton = document.getElementById("logout");
logoutButton.addEventListener("click", logoutUser);

function logoutUser() {
	localStorage.clear();
	window.location.href = "/";
}
