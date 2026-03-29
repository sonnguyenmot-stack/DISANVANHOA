namespace DISANVANHOA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thongke : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_ThongKes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ThoiGian = c.DateTime(nullable: false),
                        SoTruyCap = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tb_ThongKes");
        }
    }
}
