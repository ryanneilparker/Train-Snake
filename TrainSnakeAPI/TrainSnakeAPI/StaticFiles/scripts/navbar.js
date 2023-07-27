var currentLocation = window.location.href;
var links = document.getElementsByTagName("a");
for (var i = 0; i < links.length; i++) {
  if (links[i].href === currentLocation) {
    links[i].className += " active";
  }
}

const logoutButton = document.getElementById("logout");
logoutButton.addEventListener("click", logoutUser);

function logoutUser() {
	localStorage.clear();
	window.location.href = "/";
}
