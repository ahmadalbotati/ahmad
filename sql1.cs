-- 1. Create Department Table (Primary Structure)
CREATE TABLE DEPARTMENT (
    Dname VARCHAR(50) NOT NULL,
    Dnumber INT PRIMARY KEY,
    Mgr_SSN CHAR(9),
    Mgr_Start_Date DATE
);

-- 2. Create Employee Table
CREATE TABLE EMPLOYEE (
    Fname VARCHAR(20) NOT NULL,
    Lname VARCHAR(20) NOT NULL,
    SSN CHAR(9) PRIMARY KEY,
    Bdate DATE,
    Gender CHAR(1),
    Dno INT,
    Super_SSN CHAR(9),
    CONSTRAINT FK_Emp_Dept FOREIGN KEY (Dno) REFERENCES DEPARTMENT(Dnumber),
    CONSTRAINT FK_Emp_Sup FOREIGN KEY (Super_SSN) REFERENCES EMPLOYEE(SSN)
);

-- 3. Add Foreign Key to Department (Circular reference fix)
ALTER TABLE DEPARTMENT 
ADD CONSTRAINT FK_Dept_Mgr FOREIGN KEY (Mgr_SSN) REFERENCES EMPLOYEE(SSN);

-- 4. Create Project Table
CREATE TABLE PROJECT (
    Pname VARCHAR(50) NOT NULL,
    Pnumber INT PRIMARY KEY,
    Plocation VARCHAR(50),
    Dnum INT NOT NULL,
    CONSTRAINT FK_Proj_Dept FOREIGN KEY (Dnum) REFERENCES DEPARTMENT(Dnumber)
);

-- 5. Create Dependent Table (Weak Entity)
CREATE TABLE DEPENDENT (
    ESSN CHAR(9),
    Dependent_Name VARCHAR(50),
    Gender CHAR(1),
    Bdate DATE,
    PRIMARY KEY (ESSN, Dependent_Name),
    CONSTRAINT FK_Dep_Emp FOREIGN KEY (ESSN) REFERENCES EMPLOYEE(SSN) ON DELETE CASCADE
);

-- 6. Create Works_On Table (Many-to-Many)
CREATE TABLE WORKS_ON (
    ESSN CHAR(9),
    Pno INT,
    Hours DECIMAL(3,1),
    PRIMARY KEY (ESSN, Pno),
    CONSTRAINT FK_Work_Emp FOREIGN KEY (ESSN) REFERENCES EMPLOYEE(SSN),
    CONSTRAINT FK_Work_Proj FOREIGN KEY (Pno) REFERENCES PROJECT(Pnumber)
);


-- Insert Departments first (with null Mgr_SSN temporarily to avoid FK conflict)
INSERT INTO DEPARTMENT (Dname, Dnumber) VALUES ('HR', 1), ('IT', 2), ('Sales', 3);

-- Insert Employees
INSERT INTO EMPLOYEE (Fname, Lname, SSN, Bdate, Gender, Dno) VALUES 
('Ahmed', 'Ali', '111', '1990-01-01', 'M', 1),
('Sara', 'Nour', '222', '1995-05-12', 'F', 2),
('Omar', 'Zaid', '333', '1988-11-20', 'M', 2),
('Mona', 'Samy', '444', '1992-03-15', 'F', 3),
('Zaki', 'Hassan', '555', '1985-07-30', 'M', 1);

-- Update Managers
UPDATE DEPARTMENT SET Mgr_SSN = '111', Mgr_Start_Date = '2020-01-01' WHERE Dnumber = 1;
UPDATE DEPARTMENT SET Mgr_SSN = '222', Mgr_Start_Date = '2021-06-01' WHERE Dnumber = 2;


-- Update an employee's department
UPDATE EMPLOYEE SET Dno = 3 WHERE SSN = '555';

-- Delete a dependent record
DELETE FROM DEPENDENT WHERE ESSN = '111' AND Dependent_Name = 'John';

-- Retrieve all employees working in IT (Dnumber 2)
SELECT Fname, Lname FROM EMPLOYEE WHERE Dno = 2;

-- Find all employees and their project assignments with working hours
SELECT E.Fname, E.Lname, P.Pname, W.Hours
FROM EMPLOYEE E
JOIN WORKS_ON W ON E.SSN = W.ESSN
JOIN PROJECT P ON W.Pno = P.Pnumber;