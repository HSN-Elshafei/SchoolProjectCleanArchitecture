using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateConfig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Instructor_InsManager",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Ins_Subject_Instructor_InsId",
                table: "Ins_Subject");

            migrationBuilder.DropForeignKey(
                name: "FK_Ins_Subject_Subjects_SubId",
                table: "Ins_Subject");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructor_Departments_DID",
                table: "Instructor");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructor_Instructor_SupervisorId",
                table: "Instructor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instructor",
                table: "Instructor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ins_Subject",
                table: "Ins_Subject");

            migrationBuilder.RenameTable(
                name: "Instructor",
                newName: "Instructors");

            migrationBuilder.RenameTable(
                name: "Ins_Subject",
                newName: "Ins_Subjects");

            migrationBuilder.RenameIndex(
                name: "IX_Instructor_SupervisorId",
                table: "Instructors",
                newName: "IX_Instructors_SupervisorId");

            migrationBuilder.RenameIndex(
                name: "IX_Instructor_DID",
                table: "Instructors",
                newName: "IX_Instructors_DID");

            migrationBuilder.RenameIndex(
                name: "IX_Ins_Subject_InsId",
                table: "Ins_Subjects",
                newName: "IX_Ins_Subjects_InsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instructors",
                table: "Instructors",
                column: "InsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ins_Subjects",
                table: "Ins_Subjects",
                columns: new[] { "SubId", "InsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Instructors_InsManager",
                table: "Departments",
                column: "InsManager",
                principalTable: "Instructors",
                principalColumn: "InsId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ins_Subjects_Instructors_InsId",
                table: "Ins_Subjects",
                column: "InsId",
                principalTable: "Instructors",
                principalColumn: "InsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ins_Subjects_Subjects_SubId",
                table: "Ins_Subjects",
                column: "SubId",
                principalTable: "Subjects",
                principalColumn: "SubID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_Departments_DID",
                table: "Instructors",
                column: "DID",
                principalTable: "Departments",
                principalColumn: "DID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_Instructors_SupervisorId",
                table: "Instructors",
                column: "SupervisorId",
                principalTable: "Instructors",
                principalColumn: "InsId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Instructors_InsManager",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Ins_Subjects_Instructors_InsId",
                table: "Ins_Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Ins_Subjects_Subjects_SubId",
                table: "Ins_Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Departments_DID",
                table: "Instructors");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Instructors_SupervisorId",
                table: "Instructors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instructors",
                table: "Instructors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ins_Subjects",
                table: "Ins_Subjects");

            migrationBuilder.RenameTable(
                name: "Instructors",
                newName: "Instructor");

            migrationBuilder.RenameTable(
                name: "Ins_Subjects",
                newName: "Ins_Subject");

            migrationBuilder.RenameIndex(
                name: "IX_Instructors_SupervisorId",
                table: "Instructor",
                newName: "IX_Instructor_SupervisorId");

            migrationBuilder.RenameIndex(
                name: "IX_Instructors_DID",
                table: "Instructor",
                newName: "IX_Instructor_DID");

            migrationBuilder.RenameIndex(
                name: "IX_Ins_Subjects_InsId",
                table: "Ins_Subject",
                newName: "IX_Ins_Subject_InsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instructor",
                table: "Instructor",
                column: "InsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ins_Subject",
                table: "Ins_Subject",
                columns: new[] { "SubId", "InsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Instructor_InsManager",
                table: "Departments",
                column: "InsManager",
                principalTable: "Instructor",
                principalColumn: "InsId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ins_Subject_Instructor_InsId",
                table: "Ins_Subject",
                column: "InsId",
                principalTable: "Instructor",
                principalColumn: "InsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ins_Subject_Subjects_SubId",
                table: "Ins_Subject",
                column: "SubId",
                principalTable: "Subjects",
                principalColumn: "SubID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructor_Departments_DID",
                table: "Instructor",
                column: "DID",
                principalTable: "Departments",
                principalColumn: "DID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructor_Instructor_SupervisorId",
                table: "Instructor",
                column: "SupervisorId",
                principalTable: "Instructor",
                principalColumn: "InsId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
