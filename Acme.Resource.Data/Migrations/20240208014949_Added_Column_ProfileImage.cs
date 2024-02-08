using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acme.Resource.Data.Migrations
{
	/// <inheritdoc />
	public partial class Added_Column_ProfileImage : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<string>(
				name: "ProfileImage",
				table: "Employee",
				type: "nvarchar(max)",
				nullable: true);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "ProfileImage",
				table: "Employee");
		}
	}
}
