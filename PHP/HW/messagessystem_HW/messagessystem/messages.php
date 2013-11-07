<?php
session_start();
$pageTitle = 'Съобщения';
include './includes/constants.php';
mb_internal_encoding('UTF-8');
if ($_POST) {
        $title = trim($_POST['title']);
        $message = trim($_POST['message']);
        
        $err = false;
        if (strlen($title) < 1 || strlen($title) > 50) {
            echo 'Заглавието трябва да бъде между 1 и 50 символа дълго.';
            $err = true;
        }
        if (strlen($message) < 1 || strlen($message) > 250) {
            echo 'Съобщението трябва да бъде между 1 и 250 символа дълго.';
            $err = true;
        }
        if (!$err) {
            $title = mysqli_real_escape_string($connection, $title);
            $message = mysqli_real_escape_string($connection, $message);
            $userId = (int)$_SESSION['userIndex'];           
            $groupId = $_POST['group'];
            $query = "INSERT INTO messages(user_id, message_title, message_text, date_created, group_id) VALUES('".$userId."','".$title."','".$message."',". "NOW()," .$groupId. ")";
            mysqli_query($connection, $query);
            if (mysqli_errno($connection)) {
                echo mysqli_error($connection);
            }
            header('Location: messages.php');           
            exit;
        }
    }
if(isset($_SESSION['isLogged']) && $_SESSION['isLogged']){
include './includes/header.php';
    echo 'Добре дошъл <strong>'.$_SESSION['userName'].'</strong>';
    echo '. <a href="logout.php">Излез</a><br>';
    echo '<br>';
    
    echo 'Филтрирай: ';
    echo '<select id="filter" name="filter">';
    echo '<option value="all" selected="selected">Всички</option>';
    foreach ($groups as $key => $value) {
        echo '<option value="'.$key.'"';
        if ($key == $_GET['filter']) {
            echo ' selected="selected"';
        }
        echo '">'.$value.'</option>';
    }
    echo '</select><br>';
    
    echo 'Подреди по дата:    <select name="order" id="order">';
    foreach ($orders as $key => $value) {
          echo '<option value="'.$key.'"';
          if ($key == $_GET['order']) {
            echo ' selected="selected"';
          }
        echo '">'.$value.'</option>';
     }
    echo '</select>';
    echo '  <a id="filter-btn" href="javascript:void(0)">Go</a>';
    ?>

<?php
    function getMessages($filter="all", $order= "DESC")
    {
        include './includes/constants.php';
        echo '<div id="wrapper">';
        $connection = mysqli_connect('localhost', 'krstan4o', 'D@tas0l', 'messagessystemdb');
        mysqli_query($connection, 'SET NAMES utf8');
        $q = mysqli_query($connection, 'SELECT * FROM messages ORDER BY date_created '.$order);
        if ($filter != "all") {
        
              $q = mysqli_query($connection, "SELECT * FROM messages "."WHERE group_id=" .(int)$filter. " ORDER BY date_created ".$order);
              if (mysqli_error($connection)) {
                  echo mysqli_error($connection);
              }
        }
         
    echo '<table>';
    while ($rows = $q -> fetch_assoc()){
        echo '<tr>';
        echo '<td>';
            echo '<div class="info-holder">';
                echo '<p> Date: ';
                    echo $rows['date_created'];
                echo '</p>';
                echo '<p> Created by: ';
                    $userId = (int)$rows['user_id'];
                    $query = mysqli_query($connection, 'SELECT user_name FROM users WHERE user_id ='.$userId);
                    $arr = $query -> fetch_assoc();
                    $username = $arr['user_name'];
                    echo $username;
                echo '</p>';
                echo '<p>Group: '.$groups[$rows['group_id']].'</p>';
            echo '</div>';
        echo '</td>';
        echo '<td>';
        echo '<div class="message-holder">Message title: <strong>'.$rows['message_title'].'</strong>';
            echo '<p class="message">'.$rows['message_text'].'</p>';
        echo '</div>';    
        echo '</td>';       
        echo '</tr>';
        
    }
       echo '</table></div><br>';
    }   
    
 
    
	if ($_GET) {
        if (isset($_GET['order'])) {
            $order = $_GET['order'];
        }
        else {
             $order = "DESC";
        }
        if (isset($_GET['filter'])) {
            $filter = $_GET['filter'];
        }
        else {
             $filter = "DESC";
        }
        getMessages($filter, $order);
        
    }
    else {
          getMessages();   
    }
 
  
    echo '<div id="new-message">';
    echo '<form class="formm" method="POST"><fieldset>';
    echo '<legend>Създай ново съобщение</legend>';
    echo 'Заглавие: <input id="title" type="text" name="title"/><br>';
    echo 'Група: ';
    echo '<select name="group">';
    foreach ($groups as $key => $value) {
        echo '<option value="'.$key.'">'.$value.'</option>';
    }
    echo '</select><br>';
    echo 'Съобщение: <textarea id="message" name="message"></textarea><br>';
    echo '<button type="submit">Създай</button>
        </fieldset></form></div>';
}
else{
   header('Location: index.php');
   exit;
}
?>
<script>
    $("#filter-btn").on('click', function() {
        var that = this;
        var filterValue = $("#filter").val();
        var orderValue = $("#order").val();     
        window.location='messages.php?order='+orderValue+'&filter='+filterValue;
    });  
</script>
<?php
include './includes/footer.php';
?>