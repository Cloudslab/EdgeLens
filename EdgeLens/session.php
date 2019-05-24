<!-- 
	Coded By:
	Shreshth Tuli
-->

<?php
   if(isset($_FILES['image'])){
      $errors= array();
      $file_name = $_FILES['image']['name'];
      $file_size =$_FILES['image']['size'];
      $file_tmp =$_FILES['image']['tmp_name'];
      $file_type=$_FILES['image']['type'];
      $file_ext=strtolower(end(explode('.',$_FILES['image']['name'])));
      
      $extensions= array("jpeg","jpg","png");
      
      if(in_array($file_ext,$extensions)=== false){
         $errors[]="extension not allowed, please choose a JPEG or PNG file.";
      }
      
      if($file_size > 200097152){
         $errors[]='File size must be <= 200 MB';
      }
      
      if(empty($errors)==true){
         move_uploaded_file($file_tmp,"uploads/".$file_name);
         echo "Success";
      }else{
         print_r($errors);
      }
   }
   
   copy('10.253.221.29/filename.jpg', 'file.jpg');
?>
<html>
   <body>
      
      <form action="" method="POST" enctype="multipart/form-data">
         <input type="file" name="image" />
         <input type="submit"/>
      </form>
      
   </body>
</html>





<!-- 
	Coded By:
	Shreshth Tuli
-->

<?php
   $data = file_get_contents('php://input');
   if (!(file_put_contents('input.jpg',$data) === FALSE)) {
   	echo "File xfer completed."; // file could be empty, though
   	exec("cp input.jpg ./Yolo/test/images/");
   	exec("cd Yolo/test/");
   	exec("python test_images.py params.py")
   	exec("mv ./output/0_0.jpg ../../output.jpg")
   	
   }
   else echo "File xfer failed.";
   
   
?>