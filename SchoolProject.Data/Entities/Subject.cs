﻿using SchoolProject.Data.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
	public class Subject : GeneralLocalizableEntity
	{
		public Subject()
		{
			StudentsSubjects = new HashSet<StudentSubject>();
			DepartmetsSubjects = new HashSet<DepartmetSubject>();
			Ins_Subjects = new HashSet<Ins_Subject>();
		}
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int SubID { get; set; }
		public string SubjectName { get; set; }
		public string SubjectNameAr { get; set; }
		public DateTime Period { get; set; }
		[InverseProperty(nameof(StudentSubject.Subject))]
		public virtual ICollection<StudentSubject> StudentsSubjects { get; set; }
		[InverseProperty(nameof(DepartmetSubject.Subject))]
		public virtual ICollection<DepartmetSubject> DepartmetsSubjects { get; set; }
		[InverseProperty(nameof(Ins_Subject.Subject))]
		public virtual ICollection<Ins_Subject> Ins_Subjects { get; set; }
	}
}
