using SchoolProject.Data.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
	public class Instructor : GeneralLocalizableEntity
	{
		public Instructor()
		{
			Instructors = new HashSet<Instructor>();
			Ins_Subjects = new HashSet<Ins_Subject>();
		}
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int InsId { get; set; }
		public string IName { get; set; }
		public string INameAr { get; set; }
		public string Address { get; set; }
		public string Position { get; set; }
		public int? SupervisorId { get; set; }
		public decimal Salary { get; set; }
		public int? DID { get; set; }
		[ForeignKey(nameof(DID))]
		[InverseProperty(nameof(Department.Instructors))]
		public virtual Department? Department { get; set; }
		[InverseProperty(nameof(Department.Instructor))]
		public virtual Department DepartmentManager { get; set; }
		[ForeignKey(nameof(SupervisorId))]
		[InverseProperty(nameof(Entities.Instructor.Instructors))]
		public virtual Instructor? Supervisor { get; set; }
		[InverseProperty(nameof(Entities.Instructor.Supervisor))]
		public virtual ICollection<Instructor> Instructors { get; set; }
		[InverseProperty(nameof(Ins_Subject.Instructor))]
		public virtual ICollection<Ins_Subject> Ins_Subjects { get; set; }

	}
}
