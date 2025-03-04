namespace Vintagefur.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lab66 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "PaymentMethod");
            DropColumn("dbo.Orders", "IsPaid");
            DropColumn("dbo.Orders", "PaymentDate");
            DropColumn("dbo.Orders", "TransactionId");
            DropTable("dbo.ContactMessages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ContactMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                        Phone = c.String(maxLength: 20),
                        Subject = c.String(nullable: false, maxLength: 100),
                        Message = c.String(nullable: false, maxLength: 4000),
                        DateSent = c.DateTime(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Orders", "TransactionId", c => c.String(maxLength: 200));
            AddColumn("dbo.Orders", "PaymentDate", c => c.DateTime());
            AddColumn("dbo.Orders", "IsPaid", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "PaymentMethod", c => c.String());
        }
    }
}
