public enum QType { MCQ, TF, Essay }

public class Course {
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal MaxDegree { get; set; }
    public List<Exam> Exams { get; set; } = new();
    public List<StudentCourse> StudentCourses { get; set; } = new();
}

public class Student {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string StudentNum { get; set; }
    public List<ExamAttempt> Attempts { get; set; } = new();
}

public class Exam {
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime StartDate { get; set; }
    public int CourseId { get; set; }
    public Course Course { get; set; }
    public List<Question> Questions { get; set; } = new();
}

// Inheritance (TPH)
public abstract class Question {
    public int Id { get; set; }
    public string Text { get; set; }
    public decimal Marks { get; set; }
    public int ExamId { get; set; }
}

public class MCQ : Question { public string OptA, OptB, OptC, OptD; public char Correct; }
public class TF : Question { public bool Correct; }
public class Essay : Question { public string Criteria; }

// Many-to-Many Tables
public class StudentCourse {
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public decimal? Grade { get; set; }
}

public class ExamAttempt {
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int ExamId { get; set; }
    public decimal? Score { get; set; }
}





public class ExamContext : DbContext {
    public DbSet<Course> Courses { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<Question> Questions { get; set; }

    protected override void OnModelCreating(ModelBuilder mb) {
        // Composite Keys
        mb.Entity<StudentCourse>().HasKey(sc => new { sc.StudentId, sc.CourseId });

        // Constraints & Indexes
        mb.Entity<Student>(e => {
            e.HasIndex(s => s.Email).IsUnique();
            e.Property(s => s.Name).IsRequired().HasMaxLength(100);
        });

        // TPH Inheritance
        mb.Entity<Question>()
          .HasDiscriminator<string>("Type")
          .HasValue<MCQ>("MCQ").HasValue<TF>("TF").HasValue<Essay>("Essay");

        // Relationships & Cascades
        mb.Entity<Course>().HasMany(c => c.Exams).WithOne(e => e.Course).OnDelete(DeleteBehavior.Restrict);
        mb.Entity<Exam>().HasMany(e => e.Questions).WithOne().OnDelete(DeleteBehavior.Cascade);

        // Seed Data
        mb.Entity<Course>().HasData(new Course { Id = 1, Title = "EF Core", MaxDegree = 100 });
    }
}