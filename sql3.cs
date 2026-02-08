-- Task 1: Company DB
CREATE TABLE Depts (ID INT PRIMARY KEY, Name VARCHAR(50) UNIQUE, MgrID INT);
CREATE TABLE Emps (ID INT PRIMARY KEY, FName VARCHAR(50), LName VARCHAR(50), Gender CHAR(1) CHECK(Gender IN('M','F')), Sal DECIMAL DEFAULT 3000, DID INT REFERENCES Depts(ID), Email VARCHAR(99));
CREATE TABLE Projs (ID INT PRIMARY KEY, Name VARCHAR(50), DID INT REFERENCES Depts(ID));
CREATE TABLE Works (EID INT REFERENCES Emps(ID), PID INT REFERENCES Projs(ID), Hrs INT, PRIMARY KEY(EID,PID));
CREATE TABLE Deps (ID INT PRIMARY KEY, EID INT REFERENCES Emps(ID) ON DELETE CASCADE, Name VARCHAR(50));

ALTER TABLE Depts ADD CONSTRAINT FK_Mgr FOREIGN KEY(MgrID) REFERENCES Emps(ID);

-- Task 2 & Session 4 (Top Queries)
SELECT * FROM products WHERE list_price > 1000;
SELECT state, COUNT(*) FROM customers GROUP BY state ORDER BY 2 DESC;
SELECT TOP 10 UPPER(first_name), LOWER(last_name) FROM customers;
SELECT p.product_name, ISNULL(b.brand_name, 'No Brand') FROM products p LEFT JOIN brands b ON p.brand_id = b.brand_id;
SELECT * FROM products WHERE list_price > (SELECT AVG(list_price) FROM products);


public class Course { public int Id; public string Title; public List<Exam> Exams = new(); }
public class Student { public int Id; public string Name, Email; }
public class Exam { public int Id; public int CourseId; public List<Question> Questions = new(); }

public abstract class Question { public int Id; public string Text; public decimal Marks; }
public class MCQ : Question { public string A, B, C, D; public char Correct; }
public class TF : Question { public bool Correct; }

public class StudentCourse { public int StudentId, CourseId; public decimal? Grade; }

public class ExamContext : DbContext {
    protected override void OnModelCreating(ModelBuilder mb) {
        mb.Entity<StudentCourse>().HasKey(sc => new { sc.StudentId, sc.CourseId });
        mb.Entity<Question>().HasDiscriminator<string>("Type").HasValue<MCQ>("M").HasValue<TF>("T");
    }
}


<div class="card">
    <h1 id="txt">Click below!</h1>
    <button onclick="get()">New Advice</button>
</div>
<script src="s.js"></script>