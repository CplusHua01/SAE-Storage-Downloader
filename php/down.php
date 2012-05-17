<?php
$storeapp=$_GET['storeapp'];
if(""==$storeapp || NULL==$storeapp)
	die();
//遍历Domain下所有文件
$stor = new SaeStorage();
 
$num = 0;
while ( $ret = $stor->getList($storeapp, "*", 100, $num ) ) {
        foreach($ret as $file) {
            echo "{$file}"."|";
            $num ++;
        }
}
echo "FILE DOWN OVER|";
echo $num;
?>