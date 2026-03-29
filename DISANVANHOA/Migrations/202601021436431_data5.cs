namespace DISANVANHOA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class data5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
            AddColumn("dbo.AspNetUsers", "GTinh", c => c.String());
            AddColumn("dbo.AspNetUsers", "lop", c => c.String());
            AddColumn("dbo.AspNetUsers", "MHS", c => c.String());
            AddColumn("dbo.AspNetUsers", "KH", c => c.String());
            AddColumn("dbo.AspNetUsers", "NgaySinh", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Image");
            DropColumn("dbo.AspNetUsers", "NgaySinh");
            DropColumn("dbo.AspNetUsers", "KH");
            DropColumn("dbo.AspNetUsers", "MHS");
            DropColumn("dbo.AspNetUsers", "lop");
            DropColumn("dbo.AspNetUsers", "GTinh");
            DropColumn("dbo.AspNetUsers", "Address");
        }
    }
}
