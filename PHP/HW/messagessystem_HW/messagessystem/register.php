<?php
session_start();
$pageTitle = 'Регистрация';
include './includes/constants.php';

if(isset($_SESSION['isLogged']) && $_SESSION['isLogged']){
    header('Location: messages.php');
    exit;
}

 if ($_POST) {
    $username = trim($_POST['username']);
    
    $password = trim($_POST['password']);
    
    $password_re = trim($_POST['password_re']);
    
    if($password != $password_re){
         array_push($errors, 'Паролите несъвпадат.');
     }
    include './includes/validation.php';
    validateUsername($username, $errors);
    validatePassword($password, $errors);
 

    if(count($errors) >= 1){
        echo '<div id="error-container">';
        foreach ($errors as $err){
            echo $err . '<br>';
        }
      echo '</div>';
    }  
    else {
           $connection = mysqli_connect('localhost', 'krstan4o', 'D@tas0l', 'messagessystemdb');
           mysqli_query($connection, 'SET NAMES utf8');
    if ($connection) {
        $query = "SELECT COUNT(*) FROM users WHERE user_name ='".$username."'";
        $q = mysqli_query($connection, $query);
        $rows = $q -> fetch_assoc();
        if((int)$rows['COUNT(*)'] >= 1){
           echo 'Съществува вече потребител с такова име.';
        }
        else {
             $password = mysqli_real_escape_string($connection, $password);
             $query = "INSERT INTO users(user_name, password) VALUES('".$username."','".$password."')";
             $q = mysqli_query($connection, $query);
             header('Location: index.php');
        }
             if (mysqli_error($connection)) {
                 echo mysqli_error($connection);
             }
        }
    }
 }
include './includes/header.php';
?>

<div id="register-form">
    <form class="form" method="POST" action="register.php">
         <label for="username">Име </label><br>
         <input id="username" type="text" name="username" /><br>
         <label for="password">Парола </label><br>
         <input id="password" type="password" name="password" /><br>
         <label for="password-re">Повтори парола </label><br>
         <input id="password-re" type="password" name="password_re" /><br>
         <button type="submit" id="submit-btn">Регистрирай</button>    
    </form>
</div>

<?php
include './includes/footer.php';
?>