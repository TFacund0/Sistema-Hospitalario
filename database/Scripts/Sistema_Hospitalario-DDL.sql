USE Sistema_Hospitalario;
GO

--> =================================================================
--> ===================== TABLA ESTADO PACIENTE =====================
--> =================================================================
CREATE TABLE estado_paciente
(
  id_estado_paciente INT IDENTITY(1,1) NOT NULL,
  nombre             VARCHAR(30) NOT NULL,
  CONSTRAINT pk_estado_paciente PRIMARY KEY (id_estado_paciente),
  CONSTRAINT uq_nombre_estado_paciente UNIQUE (nombre)
);
GO

--> =================================================================
--> ========================== TABLA PACIENTE =======================
--> =================================================================
CREATE TABLE paciente
(
  id_paciente        INT IDENTITY(1,1) NOT NULL,
  dni                INT NOT NULL,
  nombre             VARCHAR(50) NOT NULL,
  apellido           VARCHAR(50) NOT NULL,
  fecha_nacimiento   DATE NOT NULL,
  observaciones      VARCHAR(300) NOT NULL,
  direccion          VARCHAR(200) NOT NULL,
  correo_electronico VARCHAR(150) NOT NULL,
  id_estado_paciente INT NOT NULL,
  CONSTRAINT pk_paciente PRIMARY KEY (id_paciente),
  CONSTRAINT fk_paciente_estado_paciente FOREIGN KEY (id_estado_paciente)
    REFERENCES estado_paciente(id_estado_paciente),
  CONSTRAINT uq_paciente_dni UNIQUE (dni),
  CONSTRAINT ck_paciente_fecha_nac CHECK (fecha_nacimiento <= CAST(GETDATE() AS DATE))
);
GO

--> =================================================================
--> ===================== TABLA TIPO HABITACION =====================
--> =================================================================
CREATE TABLE tipo_habitacion
(
  id_tipo_habitacion INT IDENTITY(1,1) NOT NULL,
  nombre             VARCHAR(30) NOT NULL,
  CONSTRAINT pk_tipo_habitacion PRIMARY KEY (id_tipo_habitacion),
  CONSTRAINT uq_nombre_tipo_habitacion UNIQUE (nombre)
);
GO

--> =================================================================
--> ========================= TABLA HABITACION ======================
--> =================================================================
CREATE TABLE habitacion
(
  nro_habitacion     INT IDENTITY(1,1) NOT NULL,
  nro_piso           INT NOT NULL,
  id_tipo_habitacion INT NOT NULL,
  CONSTRAINT pk_habitacion PRIMARY KEY (nro_habitacion),
  CONSTRAINT fk_habitacion_tipo_habitacion FOREIGN KEY (id_tipo_habitacion)
    REFERENCES tipo_habitacion(id_tipo_habitacion),
  CONSTRAINT ck_habitacion_piso CHECK (nro_piso >= 0)
);
GO

--> =================================================================
--> ======================== TABLA ESTADO CAMA ======================
--> =================================================================
CREATE TABLE estado_cama
(
  id_estado_cama INT IDENTITY(1,1) NOT NULL,
  disponibilidad VARCHAR(30) NOT NULL,
  CONSTRAINT pk_estado_cama PRIMARY KEY (id_estado_cama),
  CONSTRAINT uq_estado_cama_disponibilidad UNIQUE (disponibilidad)
);
GO

--> =================================================================
--> ============================ TABLA CAMA =========================
--> =================================================================
CREATE TABLE cama
(
  id_cama         INT IDENTITY(1,1) NOT NULL,
  nro_habitacion  INT NOT NULL,
  id_estado_cama  INT NOT NULL,
  CONSTRAINT pk_cama PRIMARY KEY (id_cama, nro_habitacion),
  CONSTRAINT fk_cama_habitacion FOREIGN KEY (nro_habitacion)
    REFERENCES habitacion(nro_habitacion),
  CONSTRAINT fk_cama_estado_cama FOREIGN KEY (id_estado_cama)
    REFERENCES estado_cama(id_estado_cama)
);
GO

--> =================================================================
--> ========================= TABLA ESPECIALIDAD ====================
--> =================================================================
CREATE TABLE especialidad
(
  id_especialidad INT IDENTITY(1,1) NOT NULL,
  nombre          VARCHAR(50) NOT NULL,
  CONSTRAINT pk_especialidad PRIMARY KEY (id_especialidad),
  CONSTRAINT uq_especialidad_nombre UNIQUE (nombre)
);
GO

--> =================================================================
--> ============================ TABLA MEDICO =======================
--> =================================================================
CREATE TABLE medico
(
  id_medico         INT IDENTITY(1,1) NOT NULL,
  matricula         VARCHAR(50) NOT NULL,
  nombre            VARCHAR(50) NOT NULL,
  apellido          VARCHAR(50) NOT NULL,
  direccion         VARCHAR(150) NOT NULL,
  correo_electronico VARCHAR(150) NOT NULL,
  id_especialidad   INT NOT NULL,
  CONSTRAINT pk_medico PRIMARY KEY (id_medico),
  CONSTRAINT fk_medico_especialidad FOREIGN KEY (id_especialidad)
    REFERENCES especialidad(id_especialidad),
  CONSTRAINT uq_medico_matricula UNIQUE (matricula)
);
GO

--> =================================================================
--> ======================== TABLA PROCEDIMIENTO ====================
--> =================================================================
CREATE TABLE procedimiento
(
  id_procedimiento INT IDENTITY(1,1) NOT NULL,
  nombre           VARCHAR(100) NOT NULL,
  CONSTRAINT pk_procedimiento PRIMARY KEY (id_procedimiento),
  CONSTRAINT uq_procedimiento_nombre UNIQUE (nombre)
);
GO

--> =================================================================
--> ========================= TABLA INTERNACION =====================
--> =================================================================
CREATE TABLE internacion
(
  id_internacion  INT IDENTITY(1,1) NOT NULL,
  fecha_inicio    DATE NOT NULL,
  fecha_fin       DATE NULL,
  motivo          VARCHAR(200) NOT NULL,
  id_cama         INT NOT NULL,
  nro_habitacion  INT NOT NULL,
  id_paciente     INT NOT NULL,
  id_medico       INT NOT NULL,
  id_procedimiento INT NOT NULL,
  CONSTRAINT pk_internacion PRIMARY KEY (id_internacion),
  CONSTRAINT fk_internacion_cama FOREIGN KEY (id_cama, nro_habitacion)
    REFERENCES cama(id_cama, nro_habitacion),
  CONSTRAINT fk_internacion_paciente FOREIGN KEY (id_paciente)
    REFERENCES paciente(id_paciente),
  CONSTRAINT fk_internacion_medico FOREIGN KEY (id_medico)
    REFERENCES medico(id_medico),
  CONSTRAINT fk_internacion_procedimiento FOREIGN KEY (id_procedimiento)
    REFERENCES procedimiento(id_procedimiento),
  CONSTRAINT ck_internacion_fechas CHECK (fecha_fin >= fecha_inicio)
);
GO

--> =================================================================
--> ============================ TABLA TELEFONO =====================
--> =================================================================
CREATE TABLE telefono
(
  numero_telefono  VARCHAR(30) NOT NULL,      
  id_nroTelefono   INT IDENTITY(1,1) NOT NULL,
  id_paciente      INT NOT NULL,
  CONSTRAINT pk_telefono PRIMARY KEY (id_paciente, id_nroTelefono),
  CONSTRAINT fk_telefono_paciente FOREIGN KEY (id_paciente)
    REFERENCES paciente(id_paciente),
  CONSTRAINT uq_telefono_numero UNIQUE (id_paciente, numero_telefono)
);
GO

--> =================================================================
--> ======================== TABLA ESTADO_TURNO =====================
--> =================================================================
CREATE TABLE estado_turno
(
  id_estado_turno INT IDENTITY(1,1) NOT NULL,
  nombre          VARCHAR(30) NOT NULL,
  CONSTRAINT pk_estado_turno PRIMARY KEY (id_estado_turno),
  CONSTRAINT uq_estado_turno_nombre UNIQUE (nombre)
);
GO

--> =================================================================
--> ============================== TABLA TURNO ======================
--> =================================================================
CREATE TABLE turno
(
  id_turno         INT IDENTITY(1,1) NOT NULL,
  fecha_turno      DATE NOT NULL,
  fecha_registracion DATE NOT NULL,
  id_procedimiento INT NOT NULL,
  id_paciente      INT NOT NULL,
  id_medico        INT NOT NULL,
  id_estado_turno  INT NOT NULL,
  CONSTRAINT pk_turno PRIMARY KEY (id_turno),
  CONSTRAINT fk_turno_procedimiento FOREIGN KEY (id_procedimiento)
    REFERENCES procedimiento(id_procedimiento),
  CONSTRAINT fk_turno_paciente FOREIGN KEY (id_paciente)
    REFERENCES paciente(id_paciente),
  CONSTRAINT fk_turno_medico FOREIGN KEY (id_medico)
    REFERENCES medico(id_medico),
  CONSTRAINT fk_turno_estado_turno FOREIGN KEY (id_estado_turno)
    REFERENCES estado_turno(id_estado_turno),
  CONSTRAINT ck_turno_fechas CHECK (fecha_registracion <= fecha_turno)
);
GO

--> =================================================================
--> ======================= TABLA SUBESPECIALIDAD ===================
--> =================================================================
CREATE TABLE subespecialidad
(
  id_subespecialidad INT IDENTITY(1,1) NOT NULL,
  nombre             VARCHAR(30) NOT NULL,
  id_especialidad    INT NOT NULL,
  CONSTRAINT pk_subespecialidad PRIMARY KEY (id_especialidad, id_subespecialidad),
  CONSTRAINT fk_subespecialidad_especialidad FOREIGN KEY (id_especialidad)
    REFERENCES especialidad(id_especialidad),
  CONSTRAINT uq_subespecialidad_nombre UNIQUE (id_especialidad, nombre)
);
GO

--> =================================================================
--> ======================== TABLA ESTADO_USUARIO ===================
--> =================================================================
CREATE TABLE estado_usuario
(
  id_estado_usuario INT IDENTITY(1,1) NOT NULL,
  nombre            VARCHAR(30) NOT NULL,
  CONSTRAINT pk_estado_usuario PRIMARY KEY (id_estado_usuario),
  CONSTRAINT uq_estado_usuario_nombre UNIQUE (nombre)
);
GO

--> =================================================================
--> =============================== TABLA ROL =======================
--> =================================================================
CREATE TABLE rol
(
  id_rol INT IDENTITY(1,1) NOT NULL,
  nombre VARCHAR(20) NOT NULL,
  CONSTRAINT pk_rol PRIMARY KEY (id_rol),
  CONSTRAINT uq_rol_nombre UNIQUE (nombre)
);
GO

--> =================================================================
--> ============================== TABLA USUARIO ====================
--> =================================================================
CREATE TABLE usuario
(
  id_usuario       INT IDENTITY(1,1) NOT NULL,
  username         VARCHAR(50) NOT NULL,
  password         VARCHAR(60) NOT NULL,
  nombre           VARCHAR(30) NOT NULL,
  apellido         VARCHAR(30) NOT NULL,
  email            VARCHAR(100) NOT NULL,
  id_estado_usuario INT NOT NULL,
  id_rol           INT NOT NULL,
  CONSTRAINT pk_usuario PRIMARY KEY (id_usuario),
  CONSTRAINT fk_usuario_estado_usuario FOREIGN KEY (id_estado_usuario)
    REFERENCES estado_usuario(id_estado_usuario),
  CONSTRAINT fk_usuario_rol FOREIGN KEY (id_rol)
    REFERENCES rol(id_rol),
  CONSTRAINT uq_usuario_username UNIQUE (username),
  CONSTRAINT uq_usuario_email UNIQUE (email)
);
GO


/* ================================================================
   ===============  DEFAULTS y CHECKS por tabla  ==================
   ================================================================ */

-- ======================== PACIENTE ==============================
GO
ALTER TABLE paciente
  ADD CONSTRAINT DF_paciente_observaciones DEFAULT('') FOR observaciones;
GO
ALTER TABLE paciente
  ADD CONSTRAINT DF_paciente_direccion DEFAULT('') FOR direccion;
GO
ALTER TABLE paciente
  ADD CONSTRAINT CK_paciente_dni_rango CHECK (dni BETWEEN 1000000 AND 99999999);
GO
ALTER TABLE paciente
  ADD CONSTRAINT CK_paciente_email_formato CHECK (correo_electronico LIKE '%@%.%');
GO
ALTER TABLE paciente
  ALTER COLUMN observaciones VARCHAR(200) NULL;


-- ========================= HABITACION ===========================
ALTER TABLE habitacion
  ADD CONSTRAINT DF_habitacion_nro_piso DEFAULT(0) FOR nro_piso;
GO


-- ======================== ESTADO_CAMA ===========================
ALTER TABLE estado_cama
  ADD CONSTRAINT DF_estado_cama_dispon DEFAULT('Disponible') FOR disponibilidad;
GO
ALTER TABLE estado_cama
  ADD CONSTRAINT CK_estado_cama_dispon CHECK (disponibilidad IN
    ('Disponible','Ocupada','Fuera de servicio','Limpieza', 'Mantenimiento'));
GO

-- =========================== MEDICO =============================
ALTER TABLE medico
  ADD CONSTRAINT DF_medico_direccion DEFAULT('') FOR direccion;
GO
ALTER TABLE medico
  ADD CONSTRAINT CK_medico_email_formato CHECK (correo_electronico LIKE '%@%.%');
GO


-- ======================== INTERNACION ===========================
ALTER TABLE internacion
  ADD CONSTRAINT DF_internacion_fecha_inicio DEFAULT(CAST(GETDATE() AS DATE)) FOR fecha_inicio;
GO
ALTER TABLE internacion
  ADD CONSTRAINT DF_internacion_motivo DEFAULT('Sin especificar') FOR motivo;
GO
ALTER TABLE internacion 
    ADD CONSTRAINT ck_internacion_fechas_fin_inicio CHECK (fecha_fin IS NULL OR fecha_fin >= fecha_inicio);
GO


-- ========================= TELEFONO =============================
ALTER TABLE telefono
  ADD CONSTRAINT CK_telefono_longitud CHECK (LEN(numero_telefono) BETWEEN 6 AND 30);
GO


-- =========================== TURNO ==============================
ALTER TABLE turno
  ADD CONSTRAINT DF_turno_fecha_registracion DEFAULT(CAST(GETDATE() AS DATE)) FOR fecha_registracion;
GO

ALTER TABLE turno 
  ADD CONSTRAINT CK_turno_no_pasado CHECK (fecha_turno >= CAST(GETDATE() AS DATE));
GO


-- ============================ USUARIO ===========================
ALTER TABLE usuario
  ADD CONSTRAINT CK_usuario_password_len CHECK (LEN([password]) >= 8);
GO
ALTER TABLE usuario
  ADD CONSTRAINT CK_usuario_email_formato CHECK (email LIKE '%@%.%');
GO