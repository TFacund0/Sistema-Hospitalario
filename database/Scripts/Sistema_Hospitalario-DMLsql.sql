--> ==================== CARGA DE REGISTROS EN LAS TABLAS ==================

--> ======================= TABLAS CATALOGO/MAESTRAS =======================

-- === ROLES ===
INSERT INTO dbo.rol (nombre) VALUES
('Administrador'),
('Administrativo'),
('Médico'),
('Gerente');

SELECT * FROM dbo.rol;

-- === ESTADO DE USUARIO ===
INSERT INTO dbo.estado_usuario (nombre) VALUES
('Activo'),
('Inactivo');

SELECT * FROM dbo.estado_usuario;

-- === ESTADO DE PACIENTE ===
INSERT INTO dbo.estado_paciente (nombre) VALUES
('Activo'),
('Internado'),
('Egresado');

SELECT * FROM dbo.estado_paciente;

-- === ESTADO DE TURNO ===
INSERT INTO dbo.estado_turno (nombre) VALUES
('Programado'),
('Confirmado'),
('Cancelado'),
('Atendido');

SELECT * FROM dbo.estado_turno;

-- === ESTADO DE CAMA ===
INSERT INTO dbo.estado_cama (disponibilidad) VALUES
('Disponible'),
('Ocupada'),
('Fuera de servicio'),
('Limpieza'),
('Mantenimiento');

SELECT * FROM dbo.estado_cama;

-- === TIPO DE HABITACIÓN ===
INSERT INTO dbo.tipo_habitacion (nombre) VALUES
('Individual'),
('Compartida'),
('Terapia Intensiva');

SELECT * FROM dbo.tipo_habitacion;

--> ======================= ESPECIALIDADES Y SUBESPECIALIDADES =======================
INSERT INTO dbo.especialidad (nombre) VALUES
('Clínica Médica'),
('Cardiología'),
('Pediatría');

SELECT * FROM dbo.especialidad;

INSERT INTO dbo.subespecialidad (id_especialidad, nombre) VALUES
(2, 'Electrofisiología'),
(2, 'Cardiología Intervencionista'),
(3, 'Neonatología');

SELECT * FROM dbo.subespecialidad;

-- === PROCEDIMIENTOS ===
INSERT INTO dbo.procedimiento (nombre) VALUES
('Ecografía abdominal'),
('Electrocardiograma'),
('Curación de heridas'),
('Consulta Médica'),
('Radiografía de tórax');

SELECT * FROM dbo.procedimiento;

--> ======================= USUARIOS/MEDICOS/PACIENTES =======================

-- === USUARIOS ===
INSERT INTO dbo.usuario (username, password, nombre, apellido, email, id_estado_usuario, id_rol)
VALUES
('admin', '0123456789', 'Carlos', 'Sosa', 'admin@hospital.com', 1, 1),
('marta', '0123456789', 'Marta', 'Gimenez', 'marta@hospital.com', 1, 2),
('drgomez', '0123456789', 'Luis', 'Gómez', 'gomez@hospital.com', 1, 3),
('gerente', '0123456789', 'María', 'López', 'gerente@hospital.com', 1, 4);

SELECT * FROM dbo.usuario;

-- === MÉDICOS ===
INSERT INTO dbo.medico (matricula, nombre, apellido, direccion, correo_electronico, id_especialidad)
VALUES
(12345, 'Luis', 'Gómez', 'Av. Libertad 123', 'gomez@hospital.com', 2),
(45678, 'Andrea', 'Martinez', 'Calle 9 N°450', 'martinez@hospital.com', 3),
(98765, 'Hernán', 'Ruiz', 'San Martín 580', 'ruiz@hospital.com', 1);

SELECT * FROM dbo.medico;

-- === PACIENTES ===
INSERT INTO dbo.paciente (dni, nombre, apellido, fecha_nacimiento, observaciones, direccion, correo_electronico, id_estado_paciente)
VALUES
('38544123', 'Juan', 'Pérez', '1995-06-21', 'Alergia a penicilina', 'San Martín 1200', 'juanperez@mail.com', 1),
('40778122', 'Lucía', 'Fernández', '1988-02-10', NULL, 'Belgrano 350', 'luciaf@mail.com', 1),
('39222111', 'Roberto', 'Ramírez', '1979-11-03', 'Hipertenso controlado', 'Sarmiento 860', 'roberramirez@mail.com', 1);

SELECT * FROM dbo.paciente;

-- === TELÉFONOS ===
INSERT INTO dbo.telefono (id_paciente, numero_telefono) VALUES
(3, '3794552211'),
(4, '3794589910'),
(5, '3794447788');

SELECT * FROM dbo.telefono;

--> ======================= HABITACIONES Y CAMAS =======================

-- === HABITACIONES ===
INSERT INTO dbo.habitacion (nro_piso, id_tipo_habitacion)
VALUES
(1, 1),
(1, 2),
(2, 3);

SELECT * FROM dbo.habitacion;

-- === CAMAS ===
INSERT INTO dbo.cama (nro_habitacion, id_estado_cama)
VALUES
(1, 8),
(1, 10),
(2, 11),
(3, 9);

SELECT * FROM dbo.cama;

--> ======================= TURNOS E INTERNACIONES =======================

-- === TURNOS ===
INSERT INTO dbo.turno (fecha_turno, fecha_registracion, id_procedimiento, id_paciente, id_medico, id_estado_turno)
VALUES
('2025-10-06 09:00', '2025-10-01', 2, 3, 1, 1),
('2025-10-06 11:00', '2025-10-02', 1, 4, 2, 2),
('2025-10-07 10:30', '2025-10-03', 3, 3, 1, 4),
('2025-10-08 14:00', '2025-10-04', 4, 5, 3, 1);

SELECT * FROM dbo.turno;

-- === INTERNACIONES ===
INSERT INTO dbo.internacion (fecha_inicio, fecha_fin, motivo, id_cama, nro_habitacion, id_paciente, id_medico, id_procedimiento)
VALUES
('2025-09-29', '2025-10-02', 'Neumonía', 3, 1, 3, 1, 4),
('2025-10-01', NULL, 'Observación posoperatoria', 3, 1, 4, 2, 1);

SELECT * FROM dbo.turno;

-- ================== Verificá los datos insertados ===================
SELECT * FROM dbo.rol;
SELECT * FROM dbo.usuario;
SELECT * FROM dbo.medico;
SELECT * FROM dbo.paciente;
SELECT * FROM dbo.turno;
SELECT * FROM dbo.internacion;

--> ========= ELIMINA LOS REGISTROS DE LAS TABLAS =============

-- 1. Tablas dependientes (hijas)
DELETE FROM dbo.internacion;
DELETE FROM dbo.turno;
DELETE FROM dbo.cama;
DELETE FROM dbo.habitacion;
DELETE FROM dbo.telefono;
DELETE FROM dbo.paciente;
DELETE FROM dbo.medico;
DELETE FROM dbo.usuario;

-- 2. Catálogos simples
DELETE FROM dbo.subespecialidad;
DELETE FROM dbo.especialidad;
DELETE FROM dbo.procedimiento;
DELETE FROM dbo.tipo_habitacion;
DELETE FROM dbo.estado_cama;
DELETE FROM dbo.estado_turno;
DELETE FROM dbo.estado_paciente;
DELETE FROM dbo.estado_usuario;
DELETE FROM dbo.rol;

-- ================= ELIMINA LA INFORMACION Y RESETEA EL IDENTITY ================
EXEC sp_msforeachtable "ALTER TABLE ? NOCHECK CONSTRAINT all";
EXEC sp_msforeachtable "DELETE FROM ?";
EXEC sp_msforeachtable "DBCC CHECKIDENT ('?', RESEED, 0)";
EXEC sp_msforeachtable "ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all";
