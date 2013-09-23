<?php
$pageTitle='Списък';
include 'includes/header.php';

?>
<a href="form.php">Добави нов контакт</a>
<table border="1">
    <tr>
        <td>Име</td>
        <td>Телефон</td>
        <td>Група</td>
    </tr>
    <?php
    if(file_exists('data.txt')){
        $result=  file('data.txt');
        foreach ($result as $value) {
            $columns=  explode('!', $value);            
            echo '<tr>
                <td>'.$columns[0].'</td>
                <td>'.$columns[1].'</td>
                <td>'.$groups[trim($columns[2])].'</td>
                </tr>';
        }
    }
    
    
    ?>

    
</table>
<?php
include 'includes/footer.php';
?>
