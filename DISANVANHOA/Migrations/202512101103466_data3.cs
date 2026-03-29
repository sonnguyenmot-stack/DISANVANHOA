namespace DISANVANHOA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class data3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Document", "Author", c => c.String());
            AddColumn("dbo.tb_Historicalrelics", "Author", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Historicalrelics", "Author");
            DropColumn("dbo.tb_Document", "Author");
        }
    }
}
