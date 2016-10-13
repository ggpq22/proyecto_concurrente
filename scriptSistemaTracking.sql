create table cuenta
(
	idCuenta integer primary key,
	usuario varchar(50),
	pass varchar(50)
);

create table grupo
(
	idGrupo integer primary key,
	idAnfitrion integer foreign key references cuenta
);

create table grupo_cuentas
(
	idGrupo integer foreign key references grupo,
	idCuenta integer foreign key references cuenta
);

create table historial
(
	idHistorial integer primary key,
	idGrupo integer foreign key references grupo,
	idCuenta integer foreign key references cuenta,
	fecha DateTime,
	lat decimal,
	long decimal
);

create table servidor
(
	idServidor integer primary key,
	nombre varchar(50),
	ip varchar(50),
	puerto varchar(50)
);