drop database if exists ongMU_db;
create database ongMU_db;
use ongMU_db;

create table users(
	id int not null auto_increment primary key,
    password varchar(255) default null,
    username varchar(255) default null,
    isAdmin boolean default null,
    createdAt datetime not null,
    updatedAt datetime not null
) engine=InnoDB;

create table clips (
	clip_id int not null auto_increment primary key,
    clip_name varchar(40) not null,
    clip_duration varchar(8) not null,
    clip_trailer_img longtext null 
) engine=InnoDB charset utf8mb4;

create table submitted_user_rating (
	user_id int not null primary key,
    clip_id int not null,
    rating enum('Liked', 'Regular', 'Disliked'),
    foreign key (user_id) references users(id) on update cascade on delete restrict,
    foreign key (clip_id) references clips(clip_id) on update cascade on delete restrict
) engine=InnoDB;

select * from users;
