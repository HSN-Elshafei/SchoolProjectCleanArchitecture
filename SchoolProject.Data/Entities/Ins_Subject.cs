using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
	public class Ins_Subject
	{
		[Key]
		public int InsId { set; get; }
		[Key]
		public int SubId { set; get; }
		[ForeignKey(nameof(InsId))]
		[InverseProperty(nameof(Instructor.Ins_Subjects))]
		public virtual Instructor Instructor { set; get; }
		[ForeignKey(nameof(SubId))]
		[InverseProperty(nameof(Subject.Ins_Subjects))]
		public virtual Subject Subject { set; get; }
	}
}
