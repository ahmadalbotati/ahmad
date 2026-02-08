public enum QType { MCQ, TF, Essay }
public class Course {
    public int Id; public string Title; public decimal MaxDeg;
    public List<Exam> Exams = new();
}
public class Student {
    public int Id; public string Name, Email, SNum;
}
public class Exam {
    public int Id; public string Title; public int CourseId;
    public List<Question> Questions = new();
}
// الوراثة (Inheritance)
public abstract class Question { public int Id; public string Text; public decimal Marks; }
public class MCQ : Question { public string A, B, C, D; public char Correct; }
public class TF : Question { public bool Correct; }
public class Essay : Question { public string Criteria; }

// جداول الربط (M:N)
public class StudentCourse { public int StudentId, CourseId; public decimal? Grade; }
public class InstructorCourse { public int InstructorId, CourseId; }


public class ExamContext : DbContext {
    public DbSet<Course> Courses { get; set; }
    public DbSet<Question> Questions { get; set; }

    protected override void OnModelCreating(ModelBuilder mb) {
        // مفاتيح مركبة (Composite Keys)
        mb.Entity<StudentCourse>().HasKey(sc => new { sc.StudentId, sc.CourseId });
        mb.Entity<InstructorCourse>().HasKey(ic => new { ic.InstructorId, ic.CourseId });

        // قيود فريدة (Unique)
        mb.Entity<Student>().HasIndex(s => s.Email).IsUnique();

        // إعداد الوراثة (TPH) في جدول واحد
        mb.Entity<Question>().HasDiscriminator<string>("Type")
            .HasValue<MCQ>("M").HasValue<TF>("T").HasValue<Essay>("E");

        // شروط التحقق (Check Constraints)
        mb.Entity<Question>().ToTable(t => t.HasCheckConstraint("CK_Marks", "Marks > 0"));
    }
}