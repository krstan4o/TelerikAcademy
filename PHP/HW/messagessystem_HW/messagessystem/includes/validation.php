<?php
function validateUsername($username, &$errors){
    $validUsernameChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_";
    if (strlen($username) < 4) {
        array_push($errors, 'Името трябва да бъде поне 5 символа.');
        return;
    }
    if (strlen($username) >= 20) {
        array_push($errors, 'Името трябва да бъде по-малко от 20 символа.');
        return;
    }
    
    for ($j = 0; $j < strlen($username); $j++){
        $isValidChar = false;
        for ($i = 0; $i < strlen($validUsernameChars); $i++){
            if($username[$j] == $validUsernameChars[$i]){
                $isValidChar = true;
                break;
            }
        }
        if($isValidChar == false){
            array_push($errors, 'Името трябва да съдържа само малки и голями латински букви, както и "_".');
            return;
        }
    }

}

function validatePassword($password, &$errors){
    if(strlen($password) <= 5){
       array_push($errors, 'Паролата трябва да бъде поне 6 символа.');
    }
    if(strlen($password) >= 25){
       array_push($errors, 'Паролата трябва да бъде по-малка от 25 символа.');
       return;
    }
}
?>
