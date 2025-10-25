using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Löwen.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDeactivateOtherDiscountsTrigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            -- Create trigger function to deactivate other discounts when a discount becomes active
            CREATE OR REPLACE FUNCTION public.deactivate_other_discounts()
            RETURNS trigger
            LANGUAGE plpgsql
            AS $$
            BEGIN
                -- Only act when the new row is active
                -- Use TG_OP to cover INSERT and UPDATE cases; WHEN clause on trigger also restricts but keep logic here for safety
                IF (TG_OP = 'INSERT' OR TG_OP = 'UPDATE') AND NEW.""IsActive"" = true THEN
                    UPDATE ""Discounts""
                    SET ""IsActive"" = false
                    WHERE ""Id"" <> NEW.""Id"" AND ""IsActive"" = true;
                END IF;

                RETURN NEW;
            END;
            $$;

            -- Create trigger that fires AFTER INSERT OR UPDATE for each row when the new row is active
            DROP TRIGGER IF EXISTS trg_deactivate_other_discounts ON ""Discounts"";
            CREATE TRIGGER trg_deactivate_other_discounts
            AFTER INSERT OR UPDATE ON ""Discounts""
            FOR EACH ROW
            WHEN (NEW.""IsActive"" IS TRUE)
            EXECUTE FUNCTION public.deactivate_other_discounts();
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"
            DROP TRIGGER IF EXISTS trg_deactivate_other_discounts ON ""Discounts"";
            DROP FUNCTION IF EXISTS public.deactivate_other_discounts();
            ");
        }
    }
}

