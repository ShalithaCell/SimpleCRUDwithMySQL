#Version  1.0
create database test;

use test;

create table items
( 
ID int auto_increment primary key,
productName nvarchar(100),
productPrice decimal(12,2),
isActive tinyint
);


