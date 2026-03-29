namespace DISANVANHOA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thongke2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.tb_ThongKes", newName: "ThongKes");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ThongKes", newName: "tb_ThongKes");
        }
    }
}
