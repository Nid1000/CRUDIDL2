-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 24-02-2026 a las 19:11:01
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";
--
-- Base de datos: `demo_db`

CREATE TABLE `persona` (
  `id` int(11) NOT NULL,
  `id_tipo_documento` int(11) NOT NULL,
  `nombres` varchar(100) DEFAULT NULL,
  `apellido_paterno` varchar(100) DEFAULT NULL,
  `apellido_materno` varchar(100) DEFAULT NULL,
  `direccion` varchar(300) DEFAULT NULL,
  `telefono` varchar(30) DEFAULT NULL,
  `user_create` int(11) NOT NULL,
  `user_update` int(11) DEFAULT NULL,
  `date_created` timestamp NOT NULL DEFAULT current_timestamp(),
  `date_update` timestamp NULL DEFAULT NULL ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

INSERT INTO `persona` (`id`, `id_tipo_documento`, `nombres`, `apellido_paterno`, `apellido_materno`, `direccion`, `telefono`, `user_create`, `user_update`, `date_created`, `date_update`) VALUES
(4, 1, 'ssdasdadas', 'asdsads', 'dassdasd', 'ddassdds', '12112121221', 0, 0, '2026-02-24 23:09:34', NULL);


CREATE TABLE `persona_tipo_documento` (
  `id` int(11) NOT NULL,
  `codigo` varchar(20) NOT NULL,
  `descripcion` varchar(50) NOT NULL,
  `user_create` int(11) NOT NULL,
  `user_update` int(11) DEFAULT NULL,
  `date_created` timestamp NOT NULL DEFAULT current_timestamp(),
  `date_update` timestamp NULL DEFAULT NULL ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

INSERT INTO `persona_tipo_documento` (`id`, `codigo`, `descripcion`, `user_create`, `user_update`, `date_created`, `date_update`) VALUES
(1, 'DNI', 'DNI', 1, NULL, '2026-02-24 18:09:03', NULL),
(2, 'CE', 'Carnet de extranjería', 1, NULL, '2026-02-24 18:09:03', NULL);

ALTER TABLE `persona`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_persona_tipo_documento` (`id_tipo_documento`);

--
-- Indices de la tabla `persona_tipo_documento`
--
ALTER TABLE `persona_tipo_documento`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--
--
-- AUTO_INCREMENT de la tabla `persona`
--
ALTER TABLE `persona`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `persona_tipo_documento`
--
ALTER TABLE `persona_tipo_documento`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- Restricciones para tablas volcadas
--
--
-- Filtros para la tabla `persona`
--
ALTER TABLE `persona`
  ADD CONSTRAINT `fk_persona_tipo_documento` FOREIGN KEY (`id_tipo_documento`) REFERENCES `persona_tipo_documento` (`id`);
COMMIT;

-- CASA

CREATE TABLE IF NOT EXISTS `casa` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(100) NOT NULL,
  `direccion` varchar(300) DEFAULT NULL,
  `referencia` varchar(200) DEFAULT NULL,
  `id_propietario_persona` int(11) DEFAULT NULL,

  `user_create` int(11) NOT NULL,
  `user_update` int(11) DEFAULT NULL,
  `date_created` timestamp NOT NULL DEFAULT current_timestamp(),
  `date_update` timestamp NULL DEFAULT NULL ON UPDATE current_timestamp(),

  PRIMARY KEY (`id`),
  KEY `fk_casa_propietario_persona` (`id_propietario_persona`),
  CONSTRAINT `fk_casa_propietario_persona`
    FOREIGN KEY (`id_propietario_persona`) REFERENCES `persona` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--  TIPO DE MASCOTA

CREATE TABLE IF NOT EXISTS `mascota_tipo` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `codigo` varchar(30) NOT NULL,
  `descripcion` varchar(80) NOT NULL,

  `user_create` int(11) NOT NULL,
  `user_update` int(11) DEFAULT NULL,
  `date_created` timestamp NOT NULL DEFAULT current_timestamp(),
  `date_update` timestamp NULL DEFAULT NULL ON UPDATE current_timestamp(),

  PRIMARY KEY (`id`),
  UNIQUE KEY `uq_mascota_tipo_codigo` (`codigo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--  MASCOTA

CREATE TABLE IF NOT EXISTS `mascota` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_duenio_persona` int(11) NOT NULL,
  `id_mascota_tipo` int(11) NOT NULL,
  `id_casa` int(11) DEFAULT NULL,

  `nombre` varchar(100) NOT NULL,
  `sexo` enum('M','H') DEFAULT NULL,
  `fecha_nacimiento` date DEFAULT NULL,
  `color` varchar(60) DEFAULT NULL,
  `observaciones` varchar(300) DEFAULT NULL,
  `estado` tinyint(1) NOT NULL DEFAULT 1,

  `user_create` int(11) NOT NULL,
  `user_update` int(11) DEFAULT NULL,
  `date_created` timestamp NOT NULL DEFAULT current_timestamp(),
  `date_update` timestamp NULL DEFAULT NULL ON UPDATE current_timestamp(),

  PRIMARY KEY (`id`),
  KEY `fk_mascota_duenio_persona` (`id_duenio_persona`),
  KEY `fk_mascota_tipo` (`id_mascota_tipo`),
  KEY `fk_mascota_casa` (`id_casa`),

  CONSTRAINT `fk_mascota_duenio_persona`
    FOREIGN KEY (`id_duenio_persona`) REFERENCES `persona` (`id`),

  CONSTRAINT `fk_mascota_tipo`
    FOREIGN KEY (`id_mascota_tipo`) REFERENCES `mascota_tipo` (`id`),

  CONSTRAINT `fk_mascota_casa`
    FOREIGN KEY (`id_casa`) REFERENCES `casa` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- TIPO DE TRABAJO

CREATE TABLE IF NOT EXISTS `trabajo_tipo` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `codigo` varchar(30) NOT NULL,
  `descripcion` varchar(80) NOT NULL,

  `user_create` int(11) NOT NULL,
  `user_update` int(11) DEFAULT NULL,
  `date_created` timestamp NOT NULL DEFAULT current_timestamp(),
  `date_update` timestamp NULL DEFAULT NULL ON UPDATE current_timestamp(),

  PRIMARY KEY (`id`),
  UNIQUE KEY `uq_trabajo_tipo_codigo` (`codigo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- TRABAJO

CREATE TABLE IF NOT EXISTS `trabajo` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_persona` int(11) NOT NULL,
  `id_trabajo_tipo` int(11) NOT NULL,

  `empresa` varchar(150) DEFAULT NULL,
  `cargo` varchar(120) DEFAULT NULL,
  `direccion` varchar(300) DEFAULT NULL,
  `telefono` varchar(30) DEFAULT NULL,
  `estado` tinyint(1) NOT NULL DEFAULT 1,

  `user_create` int(11) NOT NULL,
  `user_update` int(11) DEFAULT NULL,
  `date_created` timestamp NOT NULL DEFAULT current_timestamp(),
  `date_update` timestamp NULL DEFAULT NULL ON UPDATE current_timestamp(),

  PRIMARY KEY (`id`),
  KEY `fk_trabajo_persona` (`id_persona`),
  KEY `fk_trabajo_tipo` (`id_trabajo_tipo`),

  CONSTRAINT `fk_trabajo_persona`
    FOREIGN KEY (`id_persona`) REFERENCES `persona` (`id`),

  CONSTRAINT `fk_trabajo_tipo`
    FOREIGN KEY (`id_trabajo_tipo`) REFERENCES `trabajo_tipo` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- HORARIO (entrada/salida) 

CREATE TABLE IF NOT EXISTS `trabajo_horario` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_trabajo` int(11) NOT NULL,
  `dia_semana` tinyint(1) NOT NULL,  
  `hora_entrada` time NOT NULL,
  `hora_salida` time NOT NULL,
  `tolerancia_minutos` int(11) NOT NULL DEFAULT 0,
  `estado` tinyint(1) NOT NULL DEFAULT 1,

  `user_create` int(11) NOT NULL,
  `user_update` int(11) DEFAULT NULL,
  `date_created` timestamp NOT NULL DEFAULT current_timestamp(),
  `date_update` timestamp NULL DEFAULT NULL ON UPDATE current_timestamp(),

  PRIMARY KEY (`id`),
  KEY `fk_trabajo_horario_trabajo` (`id_trabajo`),

  CONSTRAINT `fk_trabajo_horario_trabajo`
    FOREIGN KEY (`id_trabajo`) REFERENCES `trabajo` (`id`),

  UNIQUE KEY `uq_trabajo_dia` (`id_trabajo`, `dia_semana`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- DATOS INICIALES 

INSERT INTO `mascota_tipo` (`codigo`, `descripcion`, `user_create`)
VALUES
  ('PERRO', 'Perro', 1),
  ('GATO', 'Gato', 1)
ON DUPLICATE KEY UPDATE `descripcion` = VALUES(`descripcion`);

INSERT INTO `trabajo_tipo` (`codigo`, `descripcion`, `user_create`)
VALUES
  ('FIJO', 'Trabajo fijo', 1),
  ('TEMP', 'Temporal', 1)
ON DUPLICATE KEY UPDATE `descripcion` = VALUES(`descripcion`);