CREATE database finalSoftware;

USE finalSoftware;

CREATE TABLE usuario (
    usuario VARCHAR(500) NOT NULL PRIMARY KEY,
    password VARCHAR(255) NOT NULL,
    tipo_usuario INT ,
    realizo_voto bool ,
	nombres VARCHAR(275),
    apellidos VARCHAR(275),
    genero varchar (100),
    partido VARCHAR(300)
);

CREATE TABLE voto (
    id INT AUTO_INCREMENT PRIMARY KEY,
    usuario VARCHAR(500) NOT NULL,
    partido VARCHAR(300),
    hora varchar(50),
	fecha_voto DATETIME NOT NULL,
    ip VARCHAR(100),
    votos_validos_totales INT ,
    votos_fraude INT ,
    FOREIGN KEY (usuario) REFERENCES usuario(usuario)
);