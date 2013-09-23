<?php
mb_internal_encoding('UTF-8');
$pageTitle = 'Форма';
include 'includes/header.php';

if($_POST){
    $username=trim($_POST['username']);
    $username=  str_replace('!', '', $username);
    $phone=trim($_POST['phone']);
    $phone=  str_replace('!', '', $phone);
    $selectedGroup=(int)$_POST['group'];
    $error=false;
    if(mb_strlen($username)<4){
        echo '<p>Името е прекалено късо</p>';
        $error=true;
    }
    
    if(mb_strlen($phone)<6 || mb_strlen($phone)>12){
        echo '<p>невалиден телефон</p>';
        $error=true;
    }    
    if(!array_key_exists($selectedGroup, $groups)){
        echo '<p>невалидна група</p>';
        $error=true;
    }
    
    if(!$error){
        $result=$username.'!'.$phone.'!'.$selectedGroup."\n";
        if(file_put_contents('data.txt', $result,FILE_APPEND))
        {
            echo 'Записа е успешен';
        }
    }
    
    
}




?>
<a href="index.php">Списък</a>
<form method="POST">
    <div>Име:<input type="text" name="username" /></div>
    <div>Телефон:<input type="text" name="phone" /></div>
    <div>
        <select name="group">
            <?php
            foreach ($groups as $key=>$value) {
                echo '<option value="'.$key.'">' . $value .
                        '</option>';
            }
            ?>
        </select>           
    </div>        
    <div><input type="submit" value="Добави" /></div>
</form>
<?php
include 'includes/footer.php';
?>