using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Löwen.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductLove_Trigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""

                create or replace function increase_product_love_count()
                returns trigger as $$
                begin
                    raise notice 'Trigger fired! ProductId = %', NEW."ProductId";

                    update "Products"
                    set "LoveCount" = "LoveCount" + 1
                    where "Id" = NEW."ProductId";

                    return NEW;
                end;
                $$ language plpgsql;



                create trigger trg_increase_product_love
                after insert on "LovesProductUser"
                for each row
                execute function increase_product_love_count();

                """);

            migrationBuilder.Sql("""
               
                create or replace function decrease_product_love_count()
                returns trigger as 
                $$
                begin
                	 update "Products"
                    set "LoveCount" = "LoveCount" - 1
                    where "Id" = OLD."ProductId";
                	return OLD;
                end;
                $$ language plpgsql;


                create or replace trigger trg_decrease_product_love
                after delete on "LovesProductUser" 
                for each row 
                execute function decrease_product_love_count();
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("drop trigger if exists trg_increase_product_love on \"LovesProductUser\";");
            migrationBuilder.Sql("drop trigger if exists trg_decrease_product_love on \"LovesProductUser\";");
        }
    }
}
