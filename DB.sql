CREATE TABLE t_role(
	id SERIAL,
	role_code VARCHAR(5) NOT NULL,
	role_name VARCHAR(20) NOT NULL,

	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_at TIMESTAMP,
	is_active BOOLEAN NOT NULL,

	CONSTRAINT role_pk PRIMARY KEY(id),
	CONSTRAINT role_bk UNIQUE(role_code)
);

CREATE TABLE t_file (
    id SERIAL,
    file_title TEXT NOT NULL,
    file_extension VARCHAR(5) NOT NULL,

	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_by INT,
	updated_at TIMESTAMP,
	is_active BOOLEAN NOT NULL,

    CONSTRAINT file_pk PRIMARY KEY(id)
);

CREATE TABLE t_user (
	id SERIAL,
	fullname VARCHAR(100) NOT NULL,
	email VARCHAR(30) NOT NULL,
	pwd TEXT NOT NULL,
	user_role INT NOT NULL,
	photo_profile INT,	
	
	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_by INT,
	updated_at TIMESTAMP,
	is_active BOOLEAN NOT NULL,

	CONSTRAINT user_pk PRIMARY KEY(id),
	CONSTRAINT user_bk UNIQUE(email),
	CONSTRAINT user_role_fk FOREIGN KEY(user_role) REFERENCES t_role(id),
	CONSTRAINT photo_profile_id FOREIGN KEY(photo_profile) REFERENCES t_file(id)
);

CREATE TABLE t_class (
	id SERIAL,
	class_code VARCHAR(7) NOT NULL,
	class_name VARCHAR(30) NOT NULL,
	class_desc TEXT NOT NULL,
	class_photo INT, --nullable
	lecturer_id INT NOT NULL,
	
	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_by INT,
	updated_at TIMESTAMP,
	is_active BOOLEAN NOT NULL,

	CONSTRAINT class_pk PRIMARY KEY(id),
	CONSTRAINT class_bk UNIQUE(class_code),
	CONSTRAINT class_lecturer_id FOREIGN KEY(lecturer_id) REFERENCES t_user(id),
	CONSTRAINT class_photo_id FOREIGN KEY (class_photo) REFERENCES t_file(id)
);

CREATE TABLE t_learning(
	id SERIAL,
	day_name VARCHAR(10) NOT NULL,
	learning_date DATE NOT NULL,
	class_id INT,
	
	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_by INT,
	updated_at TIMESTAMP,
	is_active BOOLEAN NOT NULL,
	
	CONSTRAINT learning_pk PRIMARY KEY(id),
	CONSTRAINT learning_class_id FOREIGN KEY(class_id) REFERENCES t_class(id)
);

CREATE TABLE t_class_enrollment (
	id SERIAL,
	class_id INT NOT NULL,
	student_id INT NOT NULL,
	
	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_by INT,
	updated_at TIMESTAMP,
	is_active BOOLEAN NOT NULL,

	CONSTRAINT class_enrollment_pk PRIMARY KEY(id),
	CONSTRAINT class_enrollment_fk FOREIGN KEY(class_id) REFERENCES t_class(id),
	CONSTRAINT class_student_fk FOREIGN KEY(student_id) REFERENCES t_user(id),
	CONSTRAINT class_enrollment_ck UNIQUE(class_id, student_id) 
);

CREATE TABLE t_session (
	id SERIAL,
	learning_id INT NOT NULL,
	session_title VARCHAR(50) NOT NULL,
	session_start TIME NOT NULL,
	session_end TIME NOT NULL,
	
	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_by INT,
	updated_at TIMESTAMP,
	is_active BOOLEAN NOT NULL,

	CONSTRAINT session_pk PRIMARY KEY(id),
	CONSTRAINT session_title_bk UNIQUE(session_title),
	CONSTRAINT session_learning_fk FOREIGN KEY(learning_id) REFERENCES t_learning(id),
	CONSTRAINT session_time_ck UNIQUE(session_start, session_end)
);

CREATE TABLE t_attendance (
	id SERIAL,
	student_id INT NOT NULL,
	session_id INT NOT NULL,
	is_approve BOOLEAN NOT NULL,
	
	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_by INT,
	updated_at TIMESTAMP,
	is_active BOOLEAN NOT NULL,

	CONSTRAINT attendance_pk PRIMARY KEY(id),
	CONSTRAINT attendance_student_fk FOREIGN KEY(student_id) REFERENCES t_user(id),
	CONSTRAINT attendance_session_fk FOREIGN KEY(session_id) REFERENCES t_session(id),
	CONSTRAINT attendance_ck UNIQUE(student_id, session_id)
);

CREATE TABLE t_material (
	id SERIAL,
	material_title VARCHAR(100) NOT NULL,
	material_content TEXT NOT NULL,
	session_id INT NOT NULL,
	
	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_by INT,
	updated_at TIMESTAMP,
	is_active BOOLEAN NOT NULL,

	CONSTRAINT material_pk PRIMARY KEY(id),
	CONSTRAINT material_session_fk FOREIGN KEY(session_id) REFERENCES t_session(id)
);

CREATE TABLE t_material_dtl (
	id SERIAL,
	material_id INT NOT NULL,
	material_file INT NOT NULL,
	
	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_by INT,
	updated_at TIMESTAMP,
	is_active BOOLEAN NOT NULL,

	CONSTRAINT material_dtl_pk PRIMARY KEY(id),
	CONSTRAINT material_dtl_fk FOREIGN KEY(material_id) REFERENCES t_material(id),
	CONSTRAINT material_dtl_file_fk FOREIGN KEY(material_file) REFERENCES t_file(id)
);

CREATE TABLE t_forum (
	id SERIAL,
	forum_title VARCHAR(30) NOT NULL,
	forum_content TEXT NOT NULL,
	session_id INT NOT NULL,
	lecturer_id INT NOT NULL,
	
	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_by INT,
	updated_at TIMESTAMP,
	is_active BOOLEAN NOT NULL,

	CONSTRAINT forum_pk PRIMARY KEY(id),
	CONSTRAINT forum_session_fk FOREIGN KEY(session_id) REFERENCES t_session(id),
	CONSTRAINT forum_lecturer_fk FOREIGN KEY(lecturer_id) REFERENCES t_user(id),
	CONSTRAINT session_id_unique UNIQUE(session_id)
);
--model double
CREATE TABLE t_forum_dtl (
	id SERIAL,
	forum_id INT NOT NULL,
	user_id INT NOT NULL,
	forum_dtl_content TEXT NOT NULL,
	
	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_by INT,
	updated_at TIMESTAMP,
	is_active BOOLEAN NOT NULL,

	CONSTRAINT forum_dtl_pk PRIMARY KEY(id),
	CONSTRAINT forum_dtl_fk FOREIGN KEY(forum_id) REFERENCES t_forum(id),
	CONSTRAINT forum_user_fk FOREIGN KEY(user_id) REFERENCES t_user(id)
);

CREATE TABLE t_assignment (
	id SERIAL,
	assignment_title VARCHAR(100) NOT NULL,
	assignment_duration INT NOT NULL,
	session_id INT NOT NULL,
	
	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_by INT,
	updated_at TIMESTAMP,
	is_active BOOLEAN NOT NULL,

	CONSTRAINT assignment_pk PRIMARY KEY(id),
	CONSTRAINT assignment_session_fk FOREIGN KEY(session_id) REFERENCES t_session(id)
);


select * from t_question _choice

select * from t_assignment_dtl
----ini utk question selain file
CREATE TABLE t_question (
	id SERIAL,
	question_content TEXT NOT NULL,
	question_type VARCHAR(30) NOT NULL, 
	
	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_by INT,
	updated_at TIMESTAMP,
	is_active BOOLEAN NOT NULL,

	CONSTRAINT question_pk PRIMARY KEY(id)
);
SELECT setval('public.t_question_id_seq', (SELECT MAX(id) FROM t_question));

SELECT pg_get_serial_sequence('t_question', 'id');


CREATE TABLE t_assignment_dtl (
	id SERIAL,
	question_id INT NOT NULL,
	assignment_id INT NOT NULL,
	
	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_by INT,
	updated_at TIMESTAMP,
	is_active BOOLEAN NOT NULL,

	CONSTRAINT assignment_dtl_pk PRIMARY KEY(id),
	CONSTRAINT assignment_dtl_question_fk FOREIGN KEY(question_id) REFERENCES t_question(id),
	CONSTRAINT assignment_dtl_fk FOREIGN KEY(assignment_id) REFERENCES t_assignment(id);
	CONSTRAINT assignment_dtl_ck UNIQUE(question_id, assignment_id)
);
select * from t_submission_dtl tsd _dtl 
t_assignment_dtl

CREATE TABLE t_question_choice (
	id SERIAL,
	question_id INT NOT NULL,
	option_abc CHAR(1) NOT NULL,
	option_content TEXT NOT NULL,
	is_correct BOOLEAN NOT NULL,
	
	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_by INT,
	updated_at TIMESTAMP,
	is_active BOOLEAN NOT NULL,

	CONSTRAINT question_choice_pk PRIMARY KEY(id),
	CONSTRAINT question_choice_fk FOREIGN KEY(question_id) REFERENCES t_question(id)
);

CREATE TABLE t_question_file (
	id SERIAL,
	question_id INT NOT NULL,
	file_content INT NOT NULL,
	
	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_by INT,
	updated_at TIMESTAMP,
	is_active BOOLEAN NOT NULL,

	CONSTRAINT question_file_pk PRIMARY KEY(id),
	CONSTRAINT question_file_fk FOREIGN KEY(question_id) REFERENCES t_question(id),
	CONSTRAINT question_file_content_fk FOREIGN KEY(file_content) REFERENCES t_file(id)
);

CREATE TABLE t_submission (
	id SERIAL,
	assignment_id INT NOT NULL,
	student_id INT NOT NULL,
	submission_grade FLOAT NOT NULL,
	
	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_by INT,
	updated_at TIMESTAMP,
	is_active BOOLEAN NOT NULL,

	CONSTRAINT submission_pk PRIMARY KEY(id),
	CONSTRAINT submission_assignment_fk FOREIGN KEY(assignment_id) REFERENCES t_assignment(id),
	CONSTRAINT submission_student_fk FOREIGN KEY(student_id) REFERENCES t_user(id)
);

CREATE TABLE t_submission_dtl (
	id SERIAL,
	submission_id INT NOT NULL,
	question_id INT NOT NULL,
	submission_content TEXT, --buat essay
	submission_choice INT,
	
	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_by INT,
	updated_at TIMESTAMP,
	is_active BOOLEAN NOT NULL,

	CONSTRAINT submission_dtl_pk PRIMARY KEY(id),
	CONSTRAINT submission_dtl_fk FOREIGN KEY(submission_id) REFERENCES t_submission(id),
	CONSTRAINT submission_dtl_question_fk FOREIGN KEY(question_id) REFERENCES t_question(id);
	CONSTRAINT submission_dtl_choice_fk FOREIGN KEY(submission_choice) REFERENCES t_question_choice(id)
);

select*from t_question_choice tqc 

t_assignment_dtl tad 

SELECT setval('id', (SELECT MAX(id) FROM t_question));

ALTER TABLE t_submission_dtl
ADD CONSTRAINT submission_dtl_question_fk FOREIGN KEY(question_id) REFERENCES t_question(id);

submission_dtl_question_fk FOREIGN KEY(question_id) REFERENCES t_question(id);

submission_dtl_question_fk FOREIGN KEY(question_id) REFERENCES t_question(id);

select *from  t_submission_dtl

submission_dtl
CREATE TABLE t_submission_dtl_file (
	id SERIAL,
	submission_dtl_id INT NOT NULL,
	submission_file INT NOT NULL,
	
	created_by INT NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_by INT,
	updated_at TIMESTAMP,
	is_active BOOLEAN NOT NULL,

	CONSTRAINT submission_dtl_file_pk PRIMARY KEY(id),
	CONSTRAINT submission_dtl_id_fk FOREIGN KEY(submission_dtl_id) REFERENCES t_submission_dtl(id),
	CONSTRAINT submission_dtl__file_fk FOREIGN KEY(submission_file) REFERENCES t_file(id)
);

--------------------dml------------------


INSERT INTO t_role (role_code, role_name, created_at, is_active) VALUES
('SYS', 'System', NOW(), FALSE);

('ADM', 'Super Admin', NOW(), 1, TRUE),
('STD', 'Student', NOW(), 1, TRUE),
('LCT', 'Lecturer', NOW(), 1, TRUE);

INSERT INTO t_file (file_title, file_extension, created_by, created_at, ver, is_active) VALUES
('C# Basics', '.pdf', 1, NOW(), 1, TRUE),
('Database Tutorial', '.pdf', 2, NOW(), 1, TRUE),
('ASP.NET MVC Guide', '.pdf', 3, NOW(), 1, TRUE),
('Entity Framework Tutorial', '.cs', 4, NOW(), 1, TRUE),
('LINQ Basics', '.pdf', 5, NOW(), 1, TRUE);

INSERT INTO t_user (fullname, email, pwd, user_role, created_by, created_at, ver, is_active) VALUES
('student 1','std1@mail.com', 'pass', 2, 1, NOW(), 1, TRUE),
('lecturer 1','lect1@mail.com', 'pass', 3, 1, NOW(), 1, TRUE),
('super admin','sa@mail.com', 'sa', 1, 1, NOW(), 1, TRUE),
('student 2','std2@mail.com', 'pass', 2, 1, NOW(), 1, TRUE),
('student 3', 'std3@mail.com', 'pass', 2, 1, NOW(), 1, TRUE);

INSERT INTO t_class (class_code, class_name, class_desc, class_photo, lecturer_id, created_by, created_at, ver, is_active) VALUES
('CLS001', 'Algoritma Dasar Pemrograman', 'class alpro', 1, 2, 1, NOW(), 1, TRUE),
('CLS002', 'Pemodelan Basis Data', 'class pbd', 2, 2, 1, NOW(), 1, TRUE),
('CLS003', 'Strategi Algoritma', 'class sa', 2,  2, 1, NOW(), 1, TRUE),
('CLS004', 'Informatika Untuk Masyarakat', 'class ium', 3, 2, 1, NOW(), 1, TRUE),
('CLS005', 'Sistem Basis Data', 'class basis data', 4, 2, 1, NOW(), 1, TRUE);

INSERT INTO t_learning (day_name, learning_date, class_id, created_by, created_at, updated_by, updated_at, ver, is_active) VALUES
('Monday', '2023-01-02', 1, 1, NOW(), 1, NOW(), 1, TRUE),
('Tuesday', '2023-01-03', 2, 3, NOW(), 3, NOW(), 1, TRUE),
('Wednesday', '2023-01-04', 2, 1, NOW(), 1, NOW(), 1, TRUE),
('Thursday', '2023-01-05', 1, 2, NOW(), 2, NOW(), 1, TRUE),
('Friday', '2023-01-06', 3, 2, NOW(), 2, NOW(), 1, TRUE);

INSERT INTO t_class_enrollment (class_id, student_id, created_by, created_at, ver, is_active) VALUES
(1, 1, 1, NOW(), 1, TRUE),
(2, 1, 1, NOW(), 1, TRUE),
(3, 1, 1, NOW(), 1, TRUE),
(4, 1, 1, NOW(), 1, TRUE),
(5, 1, 1, NOW(), 1, TRUE);

INSERT INTO t_session (learning_id, session_title, session_start, session_end, created_by, created_at, updated_by, updated_at, ver, is_active)
VALUES
(1, 'Sesi Pagi SQL', '09:00:00', '11:00:00', 1, NOW(), 1, NOW(), 1, TRUE),
(2, 'Sesi Pagi Algritma Pemrograman', '10:30:00', '12:30:00', 2, NOW(), 2, NOW(), 1, TRUE),
(3, 'Sesi Siang SQL', '13:00:00', '15:00:00', 3, NOW(), 3, NOW(), 1, TRUE),
(4, 'Sesi Pagi Informatika Untuk Masyarakat', '14:30:00', '16:30:00', 1, NOW(), 4, NOW(), 1, TRUE),
(5, 'Sesi Pagi Strategi Algoritma', '08:00:00', '10:00:00', 2, NOW(), 5, NOW(), 1, TRUE);

INSERT INTO t_attendance (student_id, session_id, isApprove, created_by, created_at, updated_by, updated_at, ver, is_active)
VALUES
(1, 1, TRUE, 1, NOW(), 1, NOW(), 1, TRUE),
(2, 2, TRUE, 2, NOW(), 2, NOW(), 1, TRUE),
(3, 3, FALSE, 3, NOW(), 3, NOW(), 1, TRUE),
(4, 4, TRUE, 2, NOW(), 4, NOW(), 1, TRUE),
(5, 5, TRUE, 4, NOW(), 5, NOW(), 1, TRUE);

INSERT INTO t_material (material_title, material_content, session_id, created_by, created_at, updated_by, updated_at, is_active)
VALUES
('Advanced Programming', 'This material covers the advance of programming.', 1, 1, NOW(), 1, NOW(), TRUE)

('Database Design Principles', 'Learn about the fundamental principles of database design.', 2, 2, NOW(), 2, NOW(), 1, TRUE),
('Advanced .NET Development', 'Explore advanced techniques in .NET development.', 3, 3, NOW(), 3, NOW(), 1, TRUE),
('Entity Relationship Diagrams', 'Understanding ER diagrams in database design.', 4, 1, NOW(), 4, NOW(), 1, TRUE),
('Query Optimization Techniques', 'Optimizing SQL queries for better performance.', 5, 2, NOW(), 5, NOW(), 1, TRUE);


INSERT INTO t_learning_material_dtl (material_id, material_file, created_by, created_at, ver, is_active) VALUES
(1, 1, 1, NOW(), 1, TRUE),
(2, 2, 1, NOW(), 1, TRUE),
(3, 3, 1, NOW(), 1, TRUE),
(4, 4, 1, NOW(), 1, TRUE),
(5, 5, 1, NOW(), 1, TRUE);

INSERT INTO t_forum (forum_title, forum_content, session_id, lecturer_id, created_by, created_at, updated_by, updated_at, ver, is_active)
VALUES
('C# Discussion', 'Discussing C# programming language topics.', 1, 2, 1, NOW(), 1, NOW(), 1, TRUE),
('Database Discussion', 'Discussing topics related to databases.', 2, 2, 2, NOW(), 2, NOW(), 1, TRUE),
('.NET Framework Discussion', 'Discussing various aspects of .NET Framework.', 3, 2, 3, NOW(), 3, NOW(), 1, TRUE),
('Programming Basics Q&A', 'Questions and answers about programming basics.', 4, 2, 4, NOW(), 4, NOW(), 1, TRUE),
('Database Design Q&A', 'Questions and answers about database design principles.', 5, 2, 5, NOW(), 5, NOW(), 1, TRUE);
select*from t_user
INSERT INTO t_forum_dtl (forum_id, user_id, forum_dtl_content, created_by, created_at, updated_by, updated_at, ver, is_active)
VALUES
(5, 17, 'left join digunakan ketika....', 17, NOW(), 4, NOW(), 1, TRUE)
(1, 1, 'Apa itu DDL?', 1, NOW(), 1, NOW(), 1, TRUE),
(2, 2, 'Apa itu database normalization?', 2, NOW(), 2, NOW(), 1, TRUE),
(3, 3, 'Perbedaan .NET Core dan .NET Framework?', 3, NOW(), 3, NOW(), 1, TRUE),
(4, 4, 'Apa itu RDMS?', 4, NOW(), 4, NOW(), 1, TRUE),
(5, 5, 'Kapan menggunakan LEFT JOIN dan RIGHT JOIN?', 5, NOW(), 5, NOW(), 1, TRUE);

INSERT INTO t_assignment (assignment_title, assignment_duration, session_id, created_by, created_at, updated_by, updated_at, is_active)
VALUES
('Programming Assignment 2', 120, 1, 1, NOW(), 1, NOW(), TRUE)

('Database Design Project', 120, 2, 2, NOW(), 2, NOW(), 1, TRUE),
('.NET Coding Challenge', 120, 3, 3, NOW(), 3, NOW(), 1, TRUE),
('SQL Query Optimization', 120, 4, 1, NOW(), 4, NOW(), 1, TRUE),
('C# and ASP.NET MVC Project', 120, 5, 2, NOW(), 5, NOW(), 1, TRUE);


INSERT INTO t_question (question_content, question_type, created_by, created_at, updated_by, updated_at, ver, is_active)
VALUES
('Apa itu bahasa pemrograman?', 'Multiple Choice', 1, NOW(), 1, NOW(), 1, TRUE),
('Apa yang dimaksud dengan OOP?', 'Essay', 2, NOW(), 2, NOW(), 1, TRUE),
('Bagaimana cara mengoptimalkan query SQL?', 'Essay', 3, NOW(), 3, NOW(), 1, TRUE),
('Apa perbedaan antara INNER JOIN dan LEFT JOIN?', 'Multiple Choice', 1, NOW(), 4, NOW(), 1, TRUE),
('Apa kegunaan dari MVC dalam pengembangan web?', 'Essay', 2, NOW(), 5, NOW(), 1, TRUE);

INSERT INTO t_assignment_dtl (question_id, assignment_id, created_by, created_at, ver, is_active) VALUES
(1, 1, 2, NOW(), 1, TRUE),
(2, 2, 2, NOW(), 1, TRUE),
(3, 3, 2, NOW(), 1, TRUE),
(4, 4, 2, NOW(), 1, TRUE),
(5, 5, 2, NOW(), 1, TRUE);

INSERT INTO t_question_choice (question_id, option_abc, option_content, isCorrect, created_by, created_at, updated_by, updated_at, ver, is_active)
VALUES
(1, 'A', 'Java', FALSE, 1, NOW(), 1, NOW(), 1, TRUE),
(1, 'B', 'C++', FALSE, 2, NOW(), 2, NOW(), 1, TRUE),
(1, 'C', 'Python', FALSE, 3, NOW(), 3, NOW(), 1, TRUE),
(1, 'D', 'JavaScript', FALSE, 4, NOW(), 4, NOW(), 1, TRUE),
(1, 'E', 'PHP', FALSE, 5, NOW(), 5, NOW(), 1, TRUE),

(2, 'A', 'Object-Oriented Programming', TRUE, 1, NOW(), 1, NOW(), 1, TRUE),
(2, 'B', 'Procedural Programming', FALSE, 2, NOW(), 2, NOW(), 1, TRUE),
(2, 'C', 'Functional Programming', FALSE, 3, NOW(), 3, NOW(), 1, TRUE),
(2, 'D', 'Structured Programming', FALSE, 4, NOW(), 4, NOW(), 1, TRUE),
(2, 'E', 'Event-Driven Programming', FALSE, 5, NOW(), 5, NOW(), 1, TRUE),

(3, 'A', 'Indexing', FALSE, 1, NOW(), 1, NOW(), 1, TRUE),
(3, 'B', 'Query Optimization', TRUE, 2, NOW(), 2, NOW(), 1, TRUE),
(3, 'C', 'Normalization', FALSE, 3, NOW(), 3, NOW(), 1, TRUE),
(3, 'D', 'Transaction Management', FALSE, 4, NOW(), 4, NOW(), 1, TRUE),
(3, 'E', 'Stored Procedures', FALSE, 5, NOW(), 5, NOW(), 1, TRUE),

(4, 'A', 'Benar', FALSE, 1, NOW(), 1, NOW(), 1, TRUE),
(4, 'B', 'Salah', TRUE, 2, NOW(), 2, NOW(), 1, TRUE),

(5, 'A', 'Memisahkan antara tampilan dan logika bisnis', TRUE, 1, NOW(), 1, NOW(), 1, TRUE),
(5, 'B', 'Membuat pengembangan lebih efisien', FALSE, 2, NOW(), 2, NOW(), 1, TRUE),
(5, 'C', 'Memisahkan antara data dan tampilan', FALSE, 3, NOW(), 3, NOW(), 1, TRUE),
(5, 'D', 'Memudahkan pemeliharaan kode', FALSE, 4, NOW(), 4, NOW(), 1, TRUE),
(5, 'E', 'Semua jawaban benar', FALSE, 5, NOW(), 5, NOW(), 1, TRUE);

INSERT INTO t_question_file (question_id, file_content, created_by, created_at, updated_by, updated_at, ver, is_active)
VALUES
(1, 1, 1, NOW(), 1, NOW(), 1, TRUE),
(2, 2, 2, NOW(), 2, NOW(), 1, TRUE),
(3, 3, 3, NOW(), 3, NOW(), 1, TRUE),
(4, 4, 4, NOW(), 4, NOW(), 1, TRUE),
(5, 5, 5, NOW(), 5, NOW(), 1, TRUE);


INSERT INTO t_submission (assignment_id, student_id, submission_grade, created_by, created_at, updated_by, updated_at, ver, is_active)
VALUES
(1, 1, 85.0, 1, NOW(), 1, NOW(), 1, TRUE),
(2, 2, 90.0, 2, NOW(), 2, NOW(), 1, TRUE),
(3, 3, 88.0, 3, NOW(), 3, NOW(), 1, TRUE),
(4, 4, 92.0, 4, NOW(), 4, NOW(), 1, TRUE),
(5, 5, 91.0, 5, NOW(), 5, NOW(), 1, TRUE);


INSERT INTO t_submission_dtl (submission_id, submission_content, submission_choice, created_by, created_at, updated_by, updated_at, ver, is_active)
VALUES
(1, 'C# adalah bahasa pemrograman...', NULL, 1, NOW(), 1, NOW(), 1, TRUE),
(2, 'Normalisasi database adalah...', NULL, 2, NOW(), 2, NOW(), 1, TRUE),
(3, NULL, 1, 3, NOW(), 3, NOW(), 3, TRUE),
(4, 'Composite atribute adalah...', NULL, 4, NOW(), 4, NOW(), 1, TRUE),
(5, 'LEFT JOIN digunakan ketika...', NULL, 5, NOW(), 5, NOW(), 1, TRUE);

INSERT INTO t_submission_dtl_file (submission_dtl_id, submission_file, created_by, created_at, ver, is_active) VALUES
(1, 1, 1, NOW(), 1, TRUE),
(2, 2, 1, NOW(), 1, TRUE),
(3, 3, 1, NOW(), 1, TRUE),
(4, 4, 1, NOW(), 1, TRUE),
(5, 5, 1, NOW(), 1, TRUE);

-----------------------------dml test----------
--alter semua table wkwkwk
ALTER TABLE t_role DROP COLUMN ver;
ALTER TABLE t_file DROP COLUMN ver;
ALTER TABLE t_user DROP COLUMN ver;
ALTER TABLE t_class DROP COLUMN ver;
ALTER TABLE t_learning DROP COLUMN ver;
ALTER TABLE t_class_enrollment DROP COLUMN ver;
ALTER TABLE t_session DROP COLUMN ver;
ALTER TABLE t_attendance DROP COLUMN ver;
ALTER TABLE t_material DROP COLUMN ver;
ALTER TABLE t_material_dtl DROP COLUMN ver;
ALTER TABLE t_forum DROP COLUMN ver;
ALTER TABLE t_forum_dtl DROP COLUMN ver;
ALTER TABLE t_assignment DROP COLUMN ver;
ALTER TABLE t_question DROP COLUMN ver;
ALTER TABLE t_assignment_dtl DROP COLUMN ver;
ALTER TABLE t_question_choice DROP COLUMN ver;
ALTER TABLE t_question_file DROP COLUMN ver;
ALTER TABLE t_submission DROP COLUMN ver;
ALTER TABLE t_submission_dtl DROP COLUMN ver;
ALTER TABLE t_submission_dtl_file DROP COLUMN ver;

--select test
SELECT * FROM t_role;
SELECT * FROM t_file;
SELECT * FROM t_user;
SELECT * FROM t_class;
SELECT * FROM t_learning;
SELECT * FROM t_class_enrollment;
SELECT * FROM t_session;
SELECT * FROM t_attendance;
SELECT * FROM t_material;
SELECT * FROM t_material_dtl;
SELECT * FROM t_forum;
SELECT * FROM t_forum_dtl;
SELECT * FROM t_assignment;
SELECT * FROM t_question;
SELECT * FROM t_assignment_dtl;
SELECT * FROM t_question_choice;
SELECT * FROM t_question_file;
SELECT * FROM t_submission;
SELECT * FROM t_submission_dtl;
SELECT * FROM t_submission_dtl_file;	
-----
SELECT learning.id AS learning_id, learning.day_name, learning.learning_date,
       t_class.id AS class_id, t_class.class_name, t_class.lecturer_id,
       t_user.fullname AS lecturer_name
FROM t_learning AS learning
JOIN t_class AS t_class ON learning.class_id = t_class.id
JOIN t_user AS t_user ON t_class.lecturer_id = t_user.id
WHERE t_class.lecturer_id = 16;
---------
UPDATE t_user
SET pwd = 'y'
WHERE id = 16;
-------------
DELETE FROM t_submission _dtl _file  WHERE id = 19 OR id = 18;
----------------------

ALTER TABLE t_class
ALTER COLUMN class_photo DROP NOT NULL;

ALTER TABLE t_question_choice 
RENAME COLUMN is_corrrect TO is_correct;

ALTER TABLE t_forum
DROP CONSTRAINT session_topic_unique;

ALTER TABLE t_forum
ADD CONSTRAINT session_id_unique UNIQUE(session_id);
-----------------------
ALTER TABLE t_role
ADD COLUMN updated_by INT;

UPDATE t_role
SET updated_by = 1;

ALTER TABLE t_role
ALTER COLUMN updated_by SET NOT NULL;

------------------------------

SELECT c.*
FROM t_class c
WHERE NOT EXISTS (
    SELECT 1
    FROM t_class_enrollment ce
    WHERE ce.class_id = c.id
    AND ce.student_id = 17 
)


SELECT 
    f.id as forum_id,
    f.forum_title,
    s.session_title,
    fd.id as forum_dtl_id,
    fd.user_id,
    fd.forum_dtl_content
FROM 
    t_forum f
JOIN 
    t_session s ON f.session_id = s.id
JOIN 
    t_forum_dtl fd ON f.id = fd.forum_id
WHERE 
    f.id = 5; 




