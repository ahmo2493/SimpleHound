// <auto-generated />
using MenuProgect1.SQLDatabase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MenuProgect1.Migrations
{
    [DbContext(typeof(MenuDBContext))]
    [Migration("20190221030116_Update-Database -Context MenuDBContext")]
    partial class UpdateDatabaseContextMenuDBContext
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MenuProgect1.SQLDatabase.MenuEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Categories");

                    b.Property<string>("Items");

                    b.Property<decimal>("Prices");

                    b.Property<int>("Tables");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("MenuEntry");
                });
#pragma warning restore 612, 618
        }
    }
}
