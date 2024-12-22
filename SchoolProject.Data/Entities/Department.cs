using SchoolProject.Data.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
	public class Department : GeneralLocalizableEntity
	{
		public Department()
		{
			Students = new HashSet<Student>();
			DepartmentSubjects = new HashSet<DepartmetSubject>();
			Instructors = new HashSet<Instructor>();
		}
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int? DID { get; set; }
		public string DName { get; set; }
		public string DNameAr { get; set; }
		public int? InsManager { get; set; }
		public virtual ICollection<Student> Students { get; set; }
		[InverseProperty(nameof(DepartmetSubject.Department))]
		public virtual ICollection<DepartmetSubject> DepartmentSubjects { get; set; }
		[InverseProperty(nameof(Instructor.Department))]
		public virtual ICollection<Instructor> Instructors { get; set; }
		[ForeignKey(nameof(InsManager))]
		[InverseProperty(nameof(Instructor.DepartmentManager))]
		public virtual Instructor? Instructor { get; set; }
	}
}
