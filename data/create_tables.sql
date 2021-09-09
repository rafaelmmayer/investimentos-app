CREATE TABLE if not exists Classes(
	id serial not null,
	Descricao varchar(100) not null,
	primary key(id)
);

CREATE TABLE if not exists Ativos(
	id serial not null,
	Codigo varchar(50) not null,
	Descricao varchar(200) not null,
	ClasseId int not null,
	Primary key(id),
	foreign key(ClasseId) references Classes(id)
);

create table if not exists Aportes(
	id serial not null,
	"Data" Date default CURRENT_DATE,
	Quantidade int not null,
	PrecoEnvio numeric not null,
	AtivoId int not null,
	Primary key(id),
	foreign key(AtivoId) references Ativos(id)
);

insert into Classes (Descricao) values ('Caixa');
insert into Classes (Descricao) values ('Ação Brasil');
insert into Classes (Descricao) values ('FII');
insert into Classes (Descricao) values ('Internacional');

insert into Ativos (Codigo, Descricao, ClasseId) values ('BBDC4', 'BRADESCO PN EJ N1', 2);
insert into Ativos (Codigo, Descricao, ClasseId) values ('MGLU3', 'MAGAZ LUIZA ON NM', 2);

insert into Aportes (Quantidade, PrecoEnvio, AtivoId) values (8, 24.93, 2);