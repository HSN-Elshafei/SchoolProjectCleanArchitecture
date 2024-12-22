using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentSubjects",
                table: "StudentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_StudentSubjects_SubID",
                table: "StudentSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmentSubjects",
                table: "DepartmentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentSubjects_SubID",
                table: "DepartmentSubjects");

            migrationBuilder.DropColumn(
                name: "StudSubID",
                table: "StudentSubjects");

            migrationBuilder.DropColumn(
                name: "DeptSubID",
                table: "DepartmentSubjects");

            migrationBuilder.AlterColumn<string>(
                name: "SubjectNameAr",
                table: "Subjects",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "SubjectName",
                table: "Subjects",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<int>(
                name: "InsManager",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentSubjects",
                table: "StudentSubjects",
                columns: new[] { "SubID", "StudID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmentSubjects",
                table: "DepartmentSubjects",
                columns: new[] { "SubID", "DID" });

            migrationBuilder.CreateTable(
                name: "Instructor",
                columns: table => new
                {
                    InsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    INameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupervisorId = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructor", x => x.InsId);
                    table.ForeignKey(
                        name: "FK_Instructor_Departments_DID",
                        column: x => x.DID,
                        principalTable: "Departments",
                        principalColumn: "DID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Instructor_Instructor_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Instructor",
                        principalColumn: "InsId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ins_Subject",
                columns: table => new
                {
                    InsId = table.Column<int>(type: "int", nullable: false),
                    SubId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ins_Subject", x => new { x.SubId, x.InsId });
                    table.ForeignKey(
                        name: "FK_Ins_Subject_Instructor_InsId",
                        column: x => x.InsId,
                        principalTable: "Instructor",
                        principalColumn: "InsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ins_Subject_Subjects_SubId",
                        column: x => x.SubId,
                        principalTable: "Subjects",
                        principalColumn: "SubID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_InsManager",
                table: "Departments",
                column: "InsManager",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ins_Subject_InsId",
                table: "Ins_Subject",
                column: "InsId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructor_DID",
                table: "Instructor",
                column: "DID");

            migrationBuilder.CreateIndex(
                name: "IX_Instructor_SupervisorId",
                table: "Instructor",
                column: "SupervisorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Instructor_InsManager",
                table: "Departments",
                column: "InsManager",
                principalTable: "Instructor",
                principalColumn: "InsId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Instructor_InsManager",
                table: "Departments");

            migrationBuilder.DropTable(
                name: "Ins_Subject");

            migrationBuilder.DropTable(
                name: "Instructor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentSubjects",
                table: "StudentSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmentSubjects",
                table: "DepartmentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_Departments_InsManager",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "InsManager",
                table: "Departments");

            migrationBuilder.AlterColumn<string>(
                name: "SubjectNameAr",
                table: "Subjects",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "SubjectName",
                table: "Subjects",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "StudSubID",
                table: "StudentSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Students",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                table: "Students",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Students",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Students",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "DeptSubID",
                table: "DepartmentSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentSubjects",
                table: "StudentSubjects",
                column: "StudSubID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmentSubjects",
                table: "DepartmentSubjects",
                column: "DeptSubID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjects_SubID",
                table: "StudentSubjects",
                column: "SubID");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentSubjects_SubID",
                table: "DepartmentSubjects",
                column: "SubID");
        }
    }
}
