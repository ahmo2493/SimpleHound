// <auto-generated />
using MenuProgect1.SQLDatabase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MenuProgect1.Migrations
{
    [DbContext(typeof(MenuDBContext))]
    partial class MenuDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MenuProgect1.SQLDatabase.MenuCustomerOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Category");

                    b.Property<string>("Customer");

                    b.Property<string>("EmployeeName");

                    b.Property<string>("FoodItem");

                    b.Property<string>("Note");

                    b.Property<string>("Password");

                    b.Property<decimal>("Price");

                    b.Property<int>("Quantity");

                    b.Property<int>("TableNum");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("MenuCustomerOrder");
                });

            modelBuilder.Entity("MenuProgect1.SQLDatabase.MenuEmployees", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("Position");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("MenuEmployees");
                });

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
