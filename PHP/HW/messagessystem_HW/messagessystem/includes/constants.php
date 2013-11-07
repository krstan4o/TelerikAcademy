<?php
$errors = array();
$groups = array();
$orders = array('DESC'=>'↓', 'ASC'=>'↑');


if (count($groups) == 0) {
    $connection = mysqli_connect('localhost', 'krstan4o', 'D@tas0l', 'messagessystemdb');
    mysqli_query($connection, 'SET NAMES utf8');
    if ($connection) {
        $q = mysqli_query($connection, 'SELECT * FROM groups');
        while ($rows = $q -> fetch_assoc()){
            $groups[(int)$rows['group_id']] = $rows['group_name'];
        }
    }
}
?>
