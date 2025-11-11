ALTER TABLE turno
	ALTER COLUMN fecha_registracion DATETIME NOT NULL

-- DEFAULT
ALTER TABLE turno
DROP CONSTRAINT DF_turno_fecha_registracion;

-- CHECK
ALTER TABLE turno
DROP CONSTRAINT ck_turno_fechas;

ALTER TABLE dbo.turno
	ADD CONSTRAINT DF_turno_fecha_registracion
		DEFAULT GETDATE() FOR fecha_registracion;

SELECT *
FROM turno;

SELECT *
FROM paciente

DELETE FROM telefono WHERE id_paciente = 6;
DELETE FROM turno WHERE id_paciente = 6;
DELETE FROM paciente WHERE id_paciente = 6;