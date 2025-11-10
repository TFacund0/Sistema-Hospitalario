SELECT *
FROM paciente

EXEC sp_help 'turno';

ALTER TABLE paciente
ADD CONSTRAINT DF_paciente_fecha_registracion DEFAULT (GETDATE()) FOR fecha_registracion;

ALTER TABLE paciente
ALTER COLUMN fecha_registracion datetime NOT NULL;

ALTER TABLE paciente
DROP CONSTRAINT DF_paciente_fecha_registracion;  -- reemplazá con tu nombre real

SELECT *
FROM internacion;

SELECT *
FROM estado_cama;

SELECT *
FROM cama;

SELECT *
FROM estado_paciente;

DELETE FROM estado_paciente WHERE estado_paciente.id_estado_paciente = 15

SELECT *
FROM paciente;

DELETE FROM paciente WHERE paciente.id_paciente = 25

SELECT *
FROM telefono

DELETE FROM telefono WHERE telefono.id_paciente = 25

SELECT *
FROM turno

SELECT *
FROM estado_turno

DELETE FROM estado_turno WHERE estado_turno.id_estado_turno = 4
DELETE FROM turno WHERE turno.id_turno = 15


SELECT *
FROM internacion

SELECT *
FROM procedimiento

SELECT *
FROM cama

SELECT *
FROM estado_cama

UPDATE cama
SET id_estado_cama = 1
WHERE id_cama = 73;

DELETE FROM procedimiento WHERE id_procedimiento = 13

DELETE FROM internacion WHERE internacion.id_internacion = 3

INSERT INTO estado_paciente (nombre) VALUES ('egresado');
INSERT INTO estado_turno (nombre) VALUES ('pendiente');
INSERT INTO estado_turno (nombre) VALUES ('atendido');

SELECT *
FROM habitacion h
	JOIN tipo_habitacion th ON h.id_tipo_habitacion = th.id_tipo_habitacion

SELECT *
FROM cama h
	JOIN estado_cama th ON h.id_estado_cama = th.id_estado_cama

INSERT INTO turno (fecha_turno, id_procedimiento, id_paciente, id_medico, id_estado_turno) VALUES (GETDATE(), 2, 13, 35, 1);
INSERT INTO turno (fecha_turno, id_procedimiento, id_paciente, id_medico, id_estado_turno) VALUES (GETDATE() + 5, 2, 13, 35, 14);
INSERT INTO turno (fecha_turno, id_procedimiento, id_paciente, id_medico, id_estado_turno) VALUES (GETDATE() + 1, 2, 13, 35, 15);

-- Ejemplo para paciente.fecha_creacion
SELECT *
FROM paciente
WHERE fecha_registracion < '1753-01-01';

-- Ejemplo para turno.fecha_registro
SELECT *
FROM turno
WHERE fecha_registracion < '1753-01-01';

UPDATE turno
SET fecha_registracion = '2025-01-01'
WHERE fecha_registracion < '1753-01-01';

ALTER TABLE turno
ADD CONSTRAINT DF_turno_fecha_creacion
DEFAULT (GETDATE()) FOR fecha_registracion;

ALTER TABLE turno
DROP CONSTRAINT DF_turno_fecha_registracion;  -- reemplazá con tu nombre real
