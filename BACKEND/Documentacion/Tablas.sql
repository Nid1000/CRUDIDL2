
CREATE TABLE persona_tipo_documento
(
    id INT AUTO_INCREMENT,
    codigo VARCHAR(20) NOT NULL,
    descripcion VARCHAR(50) NOT NULL,
	/*CAMPOS DE AUDITORIA*/
    user_create INT NOT NULL,
    user_update INT NULL,
    date_created TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    date_update TIMESTAMP NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
	PRIMARY KEY (id)
);


CREATE TABLE persona
(
	id int AUTO_INCREMENT,
	id_tipo_documento int not null,
	nombres varchar(100),
	apellido_paterno varchar(100),
	apellido_materno varchar(100),
	direccion varchar(300),
	telefono varchar(30),
	user_create INT NOT NULL,
    user_update INT NULL,
    date_created TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    date_update TIMESTAMP NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
	PRIMARY KEY (id),
	CONSTRAINT fk_persona_tipo_documento FOREIGN KEY (id_tipo_documento) REFERENCES persona_tipo_documento(id)
);

/* DATOS INICIALES */
INSERT INTO persona_tipo_documento (codigo, descripcion, user_create)
SELECT 'DNI', 'DNI', 1
WHERE NOT EXISTS (SELECT 1 FROM persona_tipo_documento WHERE codigo = 'DNI');

INSERT INTO persona_tipo_documento (codigo, descripcion, user_create)
SELECT 'CE', 'Carnet de extranjería', 1
WHERE NOT EXISTS (SELECT 1 FROM persona_tipo_documento WHERE codigo = 'CE');




