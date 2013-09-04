<?php
  $clientID = "780082496750.apps.googleusercontent.com";
  $clientSecret = "EzplRHeeDJOiHsnv8V062Sud";
  $redirectURI = "http://localhost:55373/loginServer.php";
  
  parse_str($_SERVER["QUERY_STRING"], $output);
  
  // recevied authorization code, get token
  if (isset($output["code"])) {
    $mode = 0;
    $code = $output["code"];
   
    $request = new HttpRequest("https://accounts.google.com/o/oauth2/token", HttpRequest::METH_POST);
   
    $postData = array("code" => $code, "client_id" => $clientID, "client_secret" => $clientSecret, "redirect_uri" => $redirectURI, "grant_type" =>"authorization_code");
   
    $request->addPostFields($postData);
   
    $request->send();
   
  }
  
  // received token, do something with it
  else {
    $mode = 1;
  }
  
?>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
</head>
<body>
  <h2>Login Sserver</h2>
  <?php
  if ($mode == 0) {
   echo "<p>Code: $code</p>"; 
  }
  else if ($code == 1) {
   print_r($_POST); 
  }
  ?>
</body>
</html>
