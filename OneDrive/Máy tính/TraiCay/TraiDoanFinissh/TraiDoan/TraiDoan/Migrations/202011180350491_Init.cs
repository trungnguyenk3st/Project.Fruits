namespace TraiDoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.blogs",
                c => new
                    {
                        IDblog = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        sortcontent = c.String(nullable: false),
                        content = c.String(),
                        comment = c.String(),
                        author = c.String(),
                        like = c.String(),
                        dateblog = c.DateTime(nullable: false),
                        img1 = c.String(),
                        img2 = c.String(),
                        img3 = c.String(),
                        IDblogcate = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IDblog)
                .ForeignKey("dbo.blog_category", t => t.IDblogcate, cascadeDelete: true)
                .Index(t => t.IDblogcate);
            
            CreateTable(
                "dbo.blog_category",
                c => new
                    {
                        IDblogcate = c.Int(nullable: false, identity: true),
                        Nameblogcate = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IDblogcate);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        IDCategory = c.Int(nullable: false, identity: true),
                        NameCategory = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IDCategory);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        IDProduct = c.Int(nullable: false, identity: true),
                        NameProduct = c.String(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        Image = c.String(),
                        IDCategory = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IDProduct)
                .ForeignKey("dbo.Categories", t => t.IDCategory, cascadeDelete: true)
                .Index(t => t.IDCategory);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        IDOrderDetail = c.Int(nullable: false, identity: true),
                        QuantitySale = c.Int(nullable: false),
                        UnitPriceSale = c.Double(nullable: false),
                        IDOrder = c.Int(nullable: false),
                        IDProduct = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IDOrderDetail)
                .ForeignKey("dbo.Orders", t => t.IDOrder, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.IDProduct, cascadeDelete: true)
                .Index(t => t.IDOrder)
                .Index(t => t.IDProduct);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        IDorder = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        Namecus = c.String(nullable: false),
                        Adresscus = c.String(),
                        Phonecus = c.String(),
                        AdressDelivery = c.String(),
                        User_IDUser = c.Int(),
                    })
                .PrimaryKey(t => t.IDorder)
                .ForeignKey("dbo.Users", t => t.User_IDUser)
                .Index(t => t.User_IDUser);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        IDcontact = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Name = c.String(),
                        Datacontact = c.String(),
                    })
                .PrimaryKey(t => t.IDcontact);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        IDUser = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        ConfirmPassword = c.String(nullable: false),
                        LoginErrorMessage = c.String(),
                    })
                .PrimaryKey(t => t.IDUser);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "User_IDUser", "dbo.Users");
            DropForeignKey("dbo.OrderDetails", "IDProduct", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "IDOrder", "dbo.Orders");
            DropForeignKey("dbo.Products", "IDCategory", "dbo.Categories");
            DropForeignKey("dbo.blogs", "IDblogcate", "dbo.blog_category");
            DropIndex("dbo.Orders", new[] { "User_IDUser" });
            DropIndex("dbo.OrderDetails", new[] { "IDProduct" });
            DropIndex("dbo.OrderDetails", new[] { "IDOrder" });
            DropIndex("dbo.Products", new[] { "IDCategory" });
            DropIndex("dbo.blogs", new[] { "IDblogcate" });
            DropTable("dbo.Users");
            DropTable("dbo.Contacts");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
            DropTable("dbo.blog_category");
            DropTable("dbo.blogs");
        }
    }
}
