<?php
session_start();
$pageTitle = 'Вход';
include './includes/constants.php';

if(isset($_SESSION['isLogged']) && $_SESSION['isLogged']){
    header('Location: messages.php');
    exit;
}
?>
<?php
if ($_POST) {
    $username = trim($_POST['username']);
    
    $password = trim($_POST['password']);

    
    include './includes/validation.php';
    validateUsername($username, $errors);
    validatePassword($password, $errors);
    
    if(count($errors) == 0){
        $connection = mysqli_connect('localhost', 'krstan4o', 'D@tas0l', 'messagessystemdb');
        if ($connection) {
            $password = mysqli_real_escape_string($connection, $password);
            $query = "SELECT * FROM users WHERE user_name='" .$username. "' AND password='" .$password."'"; 
            $q = mysqli_query($connection, $query);
            $rows = $q -> fetch_assoc();
                   if($rows['user_name'] == $username){
                     $_SESSION['isLogged'] = true;
                     $_SESSION['userIndex'] = (int)$rows['user_id'];
                     $_SESSION['userName'] = $rows['user_name'];
                     header('Location: messages.php');
                     exit;
                } 
                else {
                    echo 'Невалидно име или парола.';
                }
               
        }
        else {
            echo 'No connection to database';    
        }
    }
    else{
      echo '<div id="error-container">';
        foreach ($errors as $err){
            echo $err . '<br>';
        }
      echo '</div>';
    }
}
include './includes/header.php';

?>
<div id="login-form">
    <form class="form" method="POST" action="index.php">
        <label for="username">Име </label>
        <input id="username" type="text" name="username" /><br>
         <label for="password">Парола </label>
         <input id="password" type="password" name="password" /><br>
         <button type="submit" id="submit-btn">Влез</button>    
         или <a href="register.php">регистрирай</a>
    </form>
</div>
<?php include './includes/footer.php';?>