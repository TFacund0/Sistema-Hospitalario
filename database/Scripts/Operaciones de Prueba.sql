SELECT *
FROM paciente
JOIN estado_paciente ON paciente.id_estado_paciente = estado_paciente.id_estado_paciente;

SELECT i.id_internacion, p.id_paciente, p.nombre, p.id_estado_paciente, e.nombre
FROM internacion i
JOIN paciente p ON p.id_paciente = i.id_paciente
JOIN estado_paciente e ON p.id_estado_paciente = e.id_estado_paciente;

SELECT *
FROM cama
JOIN estado_cama ON estado_cama.id_estado_cama = cama.id_estado_cama

SELECT *
FROM turno
JOIN estado_turno ON estado_turno.id_estado_turno = turno.id_estado_turno;

SELECT *
FROM estado_turno

INSERT INTO estado_turno (nombre) VALUES ('En Curso');
DELETE FROM estado_turno 
WHERE nombre LIKE 'Confirmado';

DELETE FROM turno
WHERE id_turno = 5;

DELETE FROM estado_turno
WHERE id_estado_turno = 1 OR id_estado_turno = 2;

INSERT INTO estado_turno (nombre) VALUES ('Pendiente');

SELECT *
FROM turno

SELECT *
FROM procedimiento

SELECT *
FROM paciente

SELECT *
FROM medico

SELECT *
FROM estado_turno

INSERT INTO turno 
	(fecha_turno, fecha_registracion, id_procedimiento, id_paciente, id_medico, id_estado_turno)
	VALUES 
	('20251015', GETDATE(), 3, 6, 1, 6);

INSERT INTO turno 
	(fecha_turno, fecha_registracion, id_procedimiento, id_paciente, id_medico, id_estado_turno)
	VALUES 
	('20251014', GETDATE(), 2, 8, 2, 5);

INSERT INTO turno 
    (fecha_turno, fecha_registracion, id_procedimiento, id_paciente, id_medico, id_estado_turno)
VALUES 
    ('2025-10-14', GETDATE(), 4, 5, 2, 6);
