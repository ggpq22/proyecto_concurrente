create table cuenta
(
	idCuenta integer IDENTITY(1,1) primary key,
	usuario varchar(50),
	pass varchar(50),
	recibeLocalizaciones integer,
	state integer
);

create table grupo
(
	idGrupo integer IDENTITY(1,1) primary key,
	nombre varchar(50),
	idAnfitrion integer foreign key references cuenta,
	state integer
);

create table grupo_cuentas
(
	idGrupo integer foreign key references grupo,
	idCuenta integer foreign key references cuenta,
	state integer
);

create table historial
(
	idHistorial integer IDENTITY(1,1) primary key,
	idGrupo integer foreign key references grupo,
	idCuenta integer foreign key references cuenta,
	fecha DateTime,
	lat decimal,
	long decimal,
	state integer
);

create table servidor
(
	idServidor integer IDENTITY(1,1) primary key,
	nombre varchar(50),
	ip varchar(50),
	puerto varchar(50),
	state integer
);