using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Municipalities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipalities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    CountryId = table.Column<int>(type: "integer", nullable: false),
                    DepartmentId = table.Column<int>(type: "integer", nullable: false),
                    MunicipalityId = table.Column<int>(type: "integer", nullable: false),
                    Direction = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Municipalities_MunicipalityId",
                        column: x => x.MunicipalityId,
                        principalTable: "Municipalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CountryId",
                table: "Users",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentId",
                table: "Users",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_MunicipalityId",
                table: "Users",
                column: "MunicipalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Phone",
                table: "Users",
                column: "Phone",
                unique: true);

            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION sp_country_exists(p_country_id int)
                RETURNS boolean AS $$
                BEGIN
                    RETURN EXISTS(SELECT 1 FROM ""Countries"" c WHERE c.""Id"" = p_country_id);
                END;
                $$ LANGUAGE plpgsql;
            ");

            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION sp_department_exists(p_department_id int)
                RETURNS boolean AS $$
                BEGIN
                    RETURN EXISTS(SELECT 1 FROM ""Departments"" d WHERE d.""Id"" = p_department_id);
                END;
                $$ LANGUAGE plpgsql;
            ");

            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION sp_municipality_exists(p_municipality_id int)
                RETURNS boolean AS $$
                BEGIN
                    RETURN EXISTS(SELECT 1 FROM ""Municipalities"" m WHERE m.""Id"" = p_municipality_id);
                END;
                $$ LANGUAGE plpgsql;
            ");

            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION sp_phone_exists(p_phone text)
                RETURNS boolean AS $$
                BEGIN
                    RETURN EXISTS(SELECT 1 FROM ""Users"" u WHERE u.""Phone"" = p_phone);
                END;
                $$ LANGUAGE plpgsql;
            ");

      
            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION sp_create_user(
                    p_name text,
                    p_phone text,
                    p_country_id int,
                    p_department_id int,
                    p_municipality_id int,
                    p_direction text
                )
                RETURNS TABLE(
                    ""Id"" int,
                    ""Name"" text,
                    ""Phone"" text,
                    ""CountryId"" int,
                    ""DepartmentId"" int,
                    ""MunicipalityId"" int,
                    ""Direction"" text,
                    ""CountryName"" text,
                    ""DepartmentName"" text,
                    ""MunicipalityName"" text
                )
                AS $$
                DECLARE new_id int;
                BEGIN
                    INSERT INTO ""Users"" (""Name"", ""Phone"", ""CountryId"", ""DepartmentId"", ""MunicipalityId"", ""Direction"")
                    VALUES (p_name, p_phone, p_country_id, p_department_id, p_municipality_id, p_direction)
                    RETURNING ""Users"".""Id"" INTO new_id;

                    RETURN QUERY
                    SELECT
                        u.""Id"", u.""Name"", u.""Phone"", u.""CountryId"", u.""DepartmentId"", u.""MunicipalityId"", u.""Direction"",
                        c.""Name"" AS ""CountryName"",
                        d.""Name"" AS ""DepartmentName"",
                        m.""Name"" AS ""MunicipalityName""
                    FROM ""Users"" u
                    JOIN ""Countries"" c ON c.""Id"" = u.""CountryId""
                    JOIN ""Departments"" d ON d.""Id"" = u.""DepartmentId""
                    JOIN ""Municipalities"" m ON m.""Id"" = u.""MunicipalityId""
                    WHERE u.""Id"" = new_id;
                END;
                $$ LANGUAGE plpgsql;
            ");

            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION sp_get_all_users()
                RETURNS TABLE(
                    ""Id"" int,
                    ""Name"" text,
                    ""Phone"" text,
                    ""CountryId"" int,
                    ""DepartmentId"" int,
                    ""MunicipalityId"" int,
                    ""Direction"" text,
                    ""CountryName"" text,
                    ""DepartmentName"" text,
                    ""MunicipalityName"" text
                )
                AS $$
                BEGIN
                    RETURN QUERY
                    SELECT
                        u.""Id"", u.""Name"", u.""Phone"", u.""CountryId"", u.""DepartmentId"", u.""MunicipalityId"", u.""Direction"",
                        c.""Name"" AS ""CountryName"",
                        d.""Name"" AS ""DepartmentName"",
                        m.""Name"" AS ""MunicipalityName""
                    FROM ""Users"" u
                    JOIN ""Countries"" c ON c.""Id"" = u.""CountryId""
                    JOIN ""Departments"" d ON d.""Id"" = u.""DepartmentId""
                    JOIN ""Municipalities"" m ON m.""Id"" = u.""MunicipalityId"";
                END;
                $$ LANGUAGE plpgsql;
            ");

            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION sp_get_all_countries()
                RETURNS TABLE(
                    ""Id"" int,
                    ""Name"" text
                )
                AS $$
                BEGIN
                    RETURN QUERY
                    SELECT c.""Id"", c.""Name""
                    FROM ""Countries"" c;
                END;
                $$ LANGUAGE plpgsql;
            ");

            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION sp_get_all_departments()
                RETURNS TABLE(
                    ""Id"" int,
                    ""Name"" text
                )
                AS $$
                BEGIN
                    RETURN QUERY
                    SELECT d.""Id"", d.""Name""
                    FROM ""Departments"" d;
                END;
                $$ LANGUAGE plpgsql;
            ");

            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION sp_get_all_municipalities()
                RETURNS TABLE(
                    ""Id"" int,
                    ""Name"" text
                )
                AS $$
                BEGIN
                    RETURN QUERY
                    SELECT m.""Id"", m.""Name""
                    FROM ""Municipalities"" m;
                END;
                $$ LANGUAGE plpgsql;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Municipalities");

            migrationBuilder.Sql(@"DROP FUNCTION IF EXISTS sp_create_user(text, text, int, int, int, text);");
            migrationBuilder.Sql(@"DROP FUNCTION IF EXISTS sp_country_exists(int);");
            migrationBuilder.Sql(@"DROP FUNCTION IF EXISTS sp_department_exists(int);");
            migrationBuilder.Sql(@"DROP FUNCTION IF EXISTS sp_municipality_exists(int);");
            migrationBuilder.Sql(@"DROP FUNCTION IF EXISTS sp_phone_exists(text);");
            migrationBuilder.Sql(@"DROP FUNCTION IF EXISTS sp_get_all_users();");
            migrationBuilder.Sql(@"DROP FUNCTION IF EXISTS sp_get_all_countries();");
            migrationBuilder.Sql(@"DROP FUNCTION IF EXISTS sp_get_all_departments();");
            migrationBuilder.Sql(@"DROP FUNCTION IF EXISTS sp_get_all_municipalities();");
        }
    }
}
