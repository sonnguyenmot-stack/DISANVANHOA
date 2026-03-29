namespace DISANVANHOA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class data2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostMedias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        FilePath = c.String(nullable: false),
                        FileType = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostMedias", "PostId", "dbo.Posts");
            DropIndex("dbo.PostMedias", new[] { "PostId" });
            DropTable("dbo.PostMedias");
        }
    }
}
