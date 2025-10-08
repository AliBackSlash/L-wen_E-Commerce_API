using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Löwen.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCk_MustThereOneMainImageForProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                    CREATE OR REPLACE FUNCTION check_main_image_count()
                    RETURNS TRIGGER AS $$
                    BEGIN
                        IF EXISTS (
                            SELECT 1
                            FROM (
                                SELECT ""ProductId""
                                FROM ""Images""
                                GROUP BY ""ProductId""
                                HAVING COUNT(*) FILTER (WHERE ""IsMain"") <> 1
                            ) AS bad
                        ) THEN
                            RAISE EXCEPTION 'Each product must have exactly one main image';
                        END IF;
                        RETURN NULL;
                    END;
                    $$ LANGUAGE plpgsql;

                    CREATE TRIGGER trg_check_main_image
                    AFTER INSERT ON ""Images""
                    FOR EACH STATEMENT
                    EXECUTE FUNCTION check_main_image_count();");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP TRIGGER IF EXISTS trg_check_main_image ON ""ProductImages"";
                DROP FUNCTION IF EXISTS check_main_image_count();
                ");
        }
    }
}
