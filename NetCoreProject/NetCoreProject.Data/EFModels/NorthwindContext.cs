using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NetCoreProject.Data.EFModels
{
    public partial class NorthwindContext : DbContext
    {
        public NorthwindContext()
        {
        }

        public NorthwindContext(DbContextOptions<NorthwindContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<BuildVersion> BuildVersion { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<CustomerAddress> CustomerAddress { get; set; }
        public virtual DbSet<CustomerCustomerDemo> CustomerCustomerDemo { get; set; }
        public virtual DbSet<CustomerDemographics> CustomerDemographics { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<EmployeeTerritories> EmployeeTerritories { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<ErrorLog> ErrorLog { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<ProductDescription> ProductDescription { get; set; }
        public virtual DbSet<ProductModel> ProductModel { get; set; }
        public virtual DbSet<ProductModelProductDescription> ProductModelProductDescription { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<SalesOrderDetail> SalesOrderDetail { get; set; }
        public virtual DbSet<SalesOrderHeader> SalesOrderHeader { get; set; }
        public virtual DbSet<Shippers> Shippers { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }
        public virtual DbSet<Territories> Territories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=NorthW;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Address", "SalesLT");

                entity.HasIndex(e => e.Rowguid)
                    .HasName("AK_Address_rowguid")
                    .IsUnique();

                entity.HasIndex(e => e.StateProvince);

                entity.HasIndex(e => new { e.AddressLine1, e.AddressLine2, e.City, e.StateProvince, e.PostalCode, e.CountryRegion });

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.AddressLine1)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.AddressLine2).HasMaxLength(60);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.CountryRegion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.StateProvince)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<BuildVersion>(entity =>
            {
                entity.HasKey(e => e.SystemInformationId)
                    .HasName("PK__BuildVer__35E58ECAD625D208");

                entity.Property(e => e.SystemInformationId)
                    .HasColumnName("SystemInformationID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.DatabaseVersion)
                    .IsRequired()
                    .HasColumnName("Database Version")
                    .HasMaxLength(25);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.VersionDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.HasIndex(e => e.CategoryName)
                    .HasName("CategoryName");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.Picture).HasColumnType("image");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer", "SalesLT");

                entity.HasIndex(e => e.EmailAddress);

                entity.HasIndex(e => e.Rowguid)
                    .HasName("AK_Customer_rowguid")
                    .IsUnique();

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.CompanyName).HasMaxLength(128);

                entity.Property(e => e.EmailAddress).HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Phone).HasMaxLength(25);

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.SalesPerson).HasMaxLength(256);

                entity.Property(e => e.Suffix).HasMaxLength(10);

                entity.Property(e => e.Title).HasMaxLength(8);
            });

            modelBuilder.Entity<CustomerAddress>(entity =>
            {
                entity.HasKey(e => new { e.CustomerId, e.AddressId })
                    .HasName("PK_CustomerAddress_CustomerID_AddressID");

                entity.ToTable("CustomerAddress", "SalesLT");

                entity.HasIndex(e => e.Rowguid)
                    .HasName("AK_CustomerAddress_rowguid")
                    .IsUnique();

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.AddressType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.CustomerAddress)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerAddress)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CustomerCustomerDemo>(entity =>
            {
                entity.HasKey(e => new { e.CustomerId, e.CustomerTypeId })
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.CustomerId)
                    .HasColumnName("CustomerID")
                    .HasMaxLength(5);

                entity.Property(e => e.CustomerTypeId)
                    .HasColumnName("CustomerTypeID")
                    .HasMaxLength(10);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerCustomerDemo)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerCustomerDemo_Customers");

                entity.HasOne(d => d.CustomerType)
                    .WithMany(p => p.CustomerCustomerDemo)
                    .HasForeignKey(d => d.CustomerTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerCustomerDemo");
            });

            modelBuilder.Entity<CustomerDemographics>(entity =>
            {
                entity.HasKey(e => e.CustomerTypeId)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.CustomerTypeId)
                    .HasColumnName("CustomerTypeID")
                    .HasMaxLength(10)
                    .ValueGeneratedNever();

                entity.Property(e => e.CustomerDesc).HasColumnType("ntext");
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.HasIndex(e => e.City)
                    .HasName("City");

                entity.HasIndex(e => e.CompanyName)
                    .HasName("CompanyName");

                entity.HasIndex(e => e.PostalCode)
                    .HasName("PostalCode");

                entity.HasIndex(e => e.Region)
                    .HasName("Region");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("CustomerID")
                    .HasMaxLength(5)
                    .ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(60);

                entity.Property(e => e.City).HasMaxLength(15);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.ContactName).HasMaxLength(30);

                entity.Property(e => e.ContactTitle).HasMaxLength(30);

                entity.Property(e => e.Country).HasMaxLength(15);

                entity.Property(e => e.Fax).HasMaxLength(24);

                entity.Property(e => e.Phone).HasMaxLength(24);

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.Property(e => e.Region).HasMaxLength(15);
            });

            modelBuilder.Entity<EmployeeTerritories>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.TerritoryId })
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.TerritoryId)
                    .HasColumnName("TerritoryID")
                    .HasMaxLength(20);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeTerritories)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeTerritories_Employees");

                entity.HasOne(d => d.Territory)
                    .WithMany(p => p.EmployeeTerritories)
                    .HasForeignKey(d => d.TerritoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeTerritories_Territories");
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.HasIndex(e => e.LastName)
                    .HasName("LastName");

                entity.HasIndex(e => e.PostalCode)
                    .HasName("PostalCode");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.Address).HasMaxLength(60);

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.City).HasMaxLength(15);

                entity.Property(e => e.Country).HasMaxLength(15);

                entity.Property(e => e.Extension).HasMaxLength(4);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.HireDate).HasColumnType("datetime");

                entity.Property(e => e.HomePhone).HasMaxLength(24);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Notes).HasColumnType("ntext");

                entity.Property(e => e.Photo).HasColumnType("image");

                entity.Property(e => e.PhotoPath).HasMaxLength(255);

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.Property(e => e.Region).HasMaxLength(15);

                entity.Property(e => e.Title).HasMaxLength(30);

                entity.Property(e => e.TitleOfCourtesy).HasMaxLength(25);

                entity.HasOne(d => d.ReportsToNavigation)
                    .WithMany(p => p.InverseReportsToNavigation)
                    .HasForeignKey(d => d.ReportsTo)
                    .HasConstraintName("FK_Employees_Employees");
            });

            modelBuilder.Entity<ErrorLog>(entity =>
            {
                entity.Property(e => e.ErrorLogId).HasColumnName("ErrorLogID");

                entity.Property(e => e.ErrorMessage)
                    .IsRequired()
                    .HasMaxLength(4000);

                entity.Property(e => e.ErrorProcedure).HasMaxLength(126);

                entity.Property(e => e.ErrorTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId })
                    .HasName("PK_Order_Details");

                entity.ToTable("Order Details");

                entity.HasIndex(e => e.OrderId)
                    .HasName("OrdersOrder_Details");

                entity.HasIndex(e => e.ProductId)
                    .HasName("ProductsOrder_Details");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Details_Orders");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Details_Products");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.HasIndex(e => e.CustomerId)
                    .HasName("CustomersOrders");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("EmployeesOrders");

                entity.HasIndex(e => e.OrderDate)
                    .HasName("OrderDate");

                entity.HasIndex(e => e.ShipPostalCode)
                    .HasName("ShipPostalCode");

                entity.HasIndex(e => e.ShipVia)
                    .HasName("ShippersOrders");

                entity.HasIndex(e => e.ShippedDate)
                    .HasName("ShippedDate");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("CustomerID")
                    .HasMaxLength(5);

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.Freight)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.RequiredDate).HasColumnType("datetime");

                entity.Property(e => e.ShipAddress).HasMaxLength(60);

                entity.Property(e => e.ShipCity).HasMaxLength(15);

                entity.Property(e => e.ShipCountry).HasMaxLength(15);

                entity.Property(e => e.ShipName).HasMaxLength(40);

                entity.Property(e => e.ShipPostalCode).HasMaxLength(10);

                entity.Property(e => e.ShipRegion).HasMaxLength(15);

                entity.Property(e => e.ShippedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Orders_Customers");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Orders_Employees");

                entity.HasOne(d => d.ShipViaNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ShipVia)
                    .HasConstraintName("FK_Orders_Shippers");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product", "SalesLT");

                entity.HasIndex(e => e.Name)
                    .HasName("AK_Product_Name")
                    .IsUnique();

                entity.HasIndex(e => e.ProductNumber)
                    .HasName("AK_Product_ProductNumber")
                    .IsUnique();

                entity.HasIndex(e => e.Rowguid)
                    .HasName("AK_Product_rowguid")
                    .IsUnique();

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Color).HasMaxLength(15);

                entity.Property(e => e.DiscontinuedDate).HasColumnType("datetime");

                entity.Property(e => e.ListPrice).HasColumnType("money");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");

                entity.Property(e => e.ProductModelId).HasColumnName("ProductModelID");

                entity.Property(e => e.ProductNumber)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.SellEndDate).HasColumnType("datetime");

                entity.Property(e => e.SellStartDate).HasColumnType("datetime");

                entity.Property(e => e.Size).HasMaxLength(5);

                entity.Property(e => e.StandardCost).HasColumnType("money");

                entity.Property(e => e.ThumbnailPhotoFileName).HasMaxLength(50);

                entity.Property(e => e.Weight).HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.ProductCategory)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ProductCategoryId);

                entity.HasOne(d => d.ProductModel)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ProductModelId);
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("ProductCategory", "SalesLT");

                entity.HasIndex(e => e.Name)
                    .HasName("AK_ProductCategory_Name")
                    .IsUnique();

                entity.HasIndex(e => e.Rowguid)
                    .HasName("AK_ProductCategory_rowguid")
                    .IsUnique();

                entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ParentProductCategoryId).HasColumnName("ParentProductCategoryID");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.ParentProductCategory)
                    .WithMany(p => p.InverseParentProductCategory)
                    .HasForeignKey(d => d.ParentProductCategoryId)
                    .HasConstraintName("FK_ProductCategory_ProductCategory_ParentProductCategoryID_ProductCategoryID");
            });

            modelBuilder.Entity<ProductDescription>(entity =>
            {
                entity.ToTable("ProductDescription", "SalesLT");

                entity.HasIndex(e => e.Rowguid)
                    .HasName("AK_ProductDescription_rowguid")
                    .IsUnique();

                entity.Property(e => e.ProductDescriptionId).HasColumnName("ProductDescriptionID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<ProductModel>(entity =>
            {
                entity.ToTable("ProductModel", "SalesLT");

                entity.HasIndex(e => e.Name)
                    .HasName("AK_ProductModel_Name")
                    .IsUnique();

                entity.HasIndex(e => e.Rowguid)
                    .HasName("AK_ProductModel_rowguid")
                    .IsUnique();

                entity.Property(e => e.ProductModelId).HasColumnName("ProductModelID");

                entity.Property(e => e.CatalogDescription).HasColumnType("xml");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<ProductModelProductDescription>(entity =>
            {
                entity.HasKey(e => new { e.ProductModelId, e.ProductDescriptionId, e.Culture })
                    .HasName("PK_ProductModelProductDescription_ProductModelID_ProductDescriptionID_Culture");

                entity.ToTable("ProductModelProductDescription", "SalesLT");

                entity.HasIndex(e => e.Rowguid)
                    .HasName("AK_ProductModelProductDescription_rowguid")
                    .IsUnique();

                entity.Property(e => e.ProductModelId).HasColumnName("ProductModelID");

                entity.Property(e => e.ProductDescriptionId).HasColumnName("ProductDescriptionID");

                entity.Property(e => e.Culture).HasMaxLength(6);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.ProductDescription)
                    .WithMany(p => p.ProductModelProductDescription)
                    .HasForeignKey(d => d.ProductDescriptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.ProductModel)
                    .WithMany(p => p.ProductModelProductDescription)
                    .HasForeignKey(d => d.ProductModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.HasIndex(e => e.CategoryId)
                    .HasName("CategoryID");

                entity.HasIndex(e => e.ProductName)
                    .HasName("ProductName");

                entity.HasIndex(e => e.SupplierId)
                    .HasName("SuppliersProducts");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.QuantityPerUnit).HasMaxLength(20);

                entity.Property(e => e.ReorderLevel).HasDefaultValueSql("((0))");

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UnitsInStock).HasDefaultValueSql("((0))");

                entity.Property(e => e.UnitsOnOrder).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Products_Categories");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_Products_Suppliers");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.HasKey(e => e.RegionId)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.RegionId)
                    .HasColumnName("RegionID")
                    .ValueGeneratedNever();

                entity.Property(e => e.RegionDescription)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SalesOrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.SalesOrderId, e.SalesOrderDetailId })
                    .HasName("PK_SalesOrderDetail_SalesOrderID_SalesOrderDetailID");

                entity.ToTable("SalesOrderDetail", "SalesLT");

                entity.HasIndex(e => e.ProductId);

                entity.HasIndex(e => e.Rowguid)
                    .HasName("AK_SalesOrderDetail_rowguid")
                    .IsUnique();

                entity.Property(e => e.SalesOrderId).HasColumnName("SalesOrderID");

                entity.Property(e => e.SalesOrderDetailId)
                    .HasColumnName("SalesOrderDetailID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.LineTotal)
                    .HasColumnType("numeric(38, 6)")
                    .HasComputedColumnSql("(isnull(([UnitPrice]*((1.0)-[UnitPriceDiscount]))*[OrderQty],(0.0)))");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.Property(e => e.UnitPriceDiscount).HasColumnType("money");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.SalesOrderDetail)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.SalesOrder)
                    .WithMany(p => p.SalesOrderDetail)
                    .HasForeignKey(d => d.SalesOrderId);
            });

            modelBuilder.Entity<SalesOrderHeader>(entity =>
            {
                entity.HasKey(e => e.SalesOrderId)
                    .HasName("PK_SalesOrderHeader_SalesOrderID");

                entity.ToTable("SalesOrderHeader", "SalesLT");

                entity.HasIndex(e => e.CustomerId);

                entity.HasIndex(e => e.Rowguid)
                    .HasName("AK_SalesOrderHeader_rowguid")
                    .IsUnique();

                entity.HasIndex(e => e.SalesOrderNumber)
                    .HasName("AK_SalesOrderHeader_SalesOrderNumber")
                    .IsUnique();

                entity.Property(e => e.SalesOrderId)
                    .HasColumnName("SalesOrderID")
                    .HasDefaultValueSql("(NEXT VALUE FOR [SalesLT].[SalesOrderNumber])");

                entity.Property(e => e.AccountNumber).HasMaxLength(15);

                entity.Property(e => e.BillToAddressId).HasColumnName("BillToAddressID");

                entity.Property(e => e.CreditCardApprovalCode)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.Freight)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OnlineOrderFlag)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PurchaseOrderNumber).HasMaxLength(25);

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.SalesOrderNumber)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasComputedColumnSql("(isnull(N'SO'+CONVERT([nvarchar](23),[SalesOrderID],(0)),N'*** ERROR ***'))");

                entity.Property(e => e.ShipDate).HasColumnType("datetime");

                entity.Property(e => e.ShipMethod)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ShipToAddressId).HasColumnName("ShipToAddressID");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.SubTotal)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.TaxAmt)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.TotalDue)
                    .HasColumnType("money")
                    .HasComputedColumnSql("(isnull(([SubTotal]+[TaxAmt])+[Freight],(0)))");

                entity.HasOne(d => d.BillToAddress)
                    .WithMany(p => p.SalesOrderHeaderBillToAddress)
                    .HasForeignKey(d => d.BillToAddressId)
                    .HasConstraintName("FK_SalesOrderHeader_Address_BillTo_AddressID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.SalesOrderHeader)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.ShipToAddress)
                    .WithMany(p => p.SalesOrderHeaderShipToAddress)
                    .HasForeignKey(d => d.ShipToAddressId)
                    .HasConstraintName("FK_SalesOrderHeader_Address_ShipTo_AddressID");
            });

            modelBuilder.Entity<Shippers>(entity =>
            {
                entity.HasKey(e => e.ShipperId);

                entity.Property(e => e.ShipperId).HasColumnName("ShipperID");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.Phone).HasMaxLength(24);
            });

            modelBuilder.Entity<Suppliers>(entity =>
            {
                entity.HasKey(e => e.SupplierId);

                entity.HasIndex(e => e.CompanyName)
                    .HasName("CompanyName");

                entity.HasIndex(e => e.PostalCode)
                    .HasName("PostalCode");

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.Property(e => e.Address).HasMaxLength(60);

                entity.Property(e => e.City).HasMaxLength(15);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.ContactName).HasMaxLength(30);

                entity.Property(e => e.ContactTitle).HasMaxLength(30);

                entity.Property(e => e.Country).HasMaxLength(15);

                entity.Property(e => e.Fax).HasMaxLength(24);

                entity.Property(e => e.HomePage).HasColumnType("ntext");

                entity.Property(e => e.Phone).HasMaxLength(24);

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.Property(e => e.Region).HasMaxLength(15);
            });

            modelBuilder.Entity<Territories>(entity =>
            {
                entity.HasKey(e => e.TerritoryId)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.TerritoryId)
                    .HasColumnName("TerritoryID")
                    .HasMaxLength(20)
                    .ValueGeneratedNever();

                entity.Property(e => e.RegionId).HasColumnName("RegionID");

                entity.Property(e => e.TerritoryDescription)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Territories)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Territories_Region");
            });

            modelBuilder.HasSequence<int>("SalesOrderNumber");
        }
    }
}
