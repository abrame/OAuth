$(document).ready(function () {

  // send GET request to Google
  $("#logMeIn").click(function () {
    var myData = {
      response_type: "code",
      client_id: "780082496750.apps.googleusercontent.com",
      redirect_uri: "http://localhost:55373/loginServer.php",
      scope: "https%3A%2F%2Fwww.googleapis.com%2Fauth%2Fuserinfo.email+https%3A%2F%2Fwww.googleapis.com%2Fauth%2Fuserinfo.profile"
    };

    $.get("https://accounts.google.com/o/oauth2/auth", myData, logMeInSuccess);
  });

});

logMeInSuccess = function (data, textStatus, jqXHR) {
  alert(textStatus);
}