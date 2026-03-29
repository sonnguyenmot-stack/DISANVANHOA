namespace DISANVANHOA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class database : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Alias = c.String(),
                        link = c.String(),
                        Description = c.String(),
                        SeoTitle = c.String(maxLength: 150),
                        SeoDescription = c.String(maxLength: 250),
                        SeoKeywords = c.String(maxLength: 150),
                        IsActive = c.Boolean(nullable: false),
                        Position = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        Modifiedby = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tb_Certificate",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Alias = c.String(),
                        Position = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        Modifiedby = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tb_Document",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Alias = c.String(),
                        Img = c.String(),
                        FilePath = c.String(),
                        Description = c.String(),
                        Detail = c.String(),
                        link = c.String(),
                        IsHome = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CertificateId = c.Int(nullable: false),
                        DocumentCategoryId = c.Int(nullable: false),
                        DocTypeId = c.Int(nullable: false),
                        ViewCount = c.Int(nullable: false),
                        SummaryAI = c.String(),
                        WorksheetAI = c.String(),
                        QuestionsAI = c.String(),
                        TestAI = c.String(),
                        ActivitySuggestionsAI = c.String(),
                        IsAIProcessed = c.Boolean(nullable: false),
                        VideoPath = c.String(),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        Modifiedby = c.String(),
                        General_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tb_Type", t => t.DocTypeId, cascadeDelete: true)
                .ForeignKey("dbo.tb_DocumentCategory", t => t.DocumentCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.tb_General", t => t.General_Id)
                .ForeignKey("dbo.tb_Certificate", t => t.CertificateId, cascadeDelete: true)
                .Index(t => t.CertificateId)
                .Index(t => t.DocumentCategoryId)
                .Index(t => t.DocTypeId)
                .Index(t => t.General_Id);
            
            CreateTable(
                "dbo.tb_Type",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Alias = c.String(),
                        Description = c.String(),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        Modifiedby = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tb_DocumentCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Alias = c.String(),
                        Description = c.String(),
                        Icon = c.String(),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        Modifiedby = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tb_DocumentImage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DocumentId = c.Int(nullable: false),
                        Image = c.String(),
                        IsDefault = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        Modifiedby = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tb_Document", t => t.DocumentId, cascadeDelete: true)
                .Index(t => t.DocumentId);
            
            CreateTable(
                "dbo.tb_General",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Alias = c.String(),
                        Description = c.String(),
                        Detail = c.String(),
                        Icon = c.String(),
                        FilePath = c.String(),
                        ViewCount = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        Modifiedby = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tb_ChatFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User = c.String(),
                        FileName = c.String(),
                        FilePath = c.String(),
                        UploadTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Author = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        Likes = c.Int(nullable: false),
                        Dislikes = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Author = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        MediaPath = c.String(),
                        Likes = c.Int(nullable: false),
                        Dislikes = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tb_Contact",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Email = c.String(maxLength: 150),
                        Website = c.String(),
                        Message = c.String(maxLength: 4000),
                        IsRead = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        Modifiedby = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tb_Exam",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Alias = c.String(),
                        Image = c.String(),
                        Description = c.String(),
                        Detail = c.String(),
                        Level = c.String(),
                        SubjectId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        DocumentCategoryId = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        Modifiedby = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tb_DocumentCategory", t => t.DocumentCategoryId, cascadeDelete: true)
                .Index(t => t.DocumentCategoryId);
            
            CreateTable(
                "dbo.tb_Historicalrelics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150),
                        Alias = c.String(),
                        Description = c.String(),
                        Detail = c.String(),
                        Image = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        ViewCount = c.Int(nullable: false),
                        FilePath = c.String(),
                        VideoPath = c.String(),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        Modifiedby = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tb_News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150),
                        Alias = c.String(),
                        Description = c.String(),
                        Detail = c.String(),
                        Image = c.String(),
                        CategoryId = c.Int(nullable: false),
                        SeoTitle = c.String(),
                        SeoDescription = c.String(),
                        SeoKeywords = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsHome = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        Modifiedby = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tb_QuestionDiSan",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Img1 = c.String(),
                        Img2 = c.String(),
                        Img3 = c.String(),
                        Img4 = c.String(),
                        result = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tb_Question",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        OptionA = c.String(),
                        OptionB = c.String(),
                        OptionC = c.String(),
                        OptionD = c.String(),
                        CorrectAnswer = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tb_Review",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        UserName = c.String(),
                        FullName = c.String(),
                        Email = c.String(),
                        Content = c.String(),
                        Rate = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        Avatar = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tb_Document", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(),
                        Phone = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.tb_Review", "ProductId", "dbo.tb_Document");
            DropForeignKey("dbo.tb_Exam", "DocumentCategoryId", "dbo.tb_DocumentCategory");
            DropForeignKey("dbo.Comments", "PostId", "dbo.Posts");
            DropForeignKey("dbo.tb_Document", "CertificateId", "dbo.tb_Certificate");
            DropForeignKey("dbo.tb_Document", "General_Id", "dbo.tb_General");
            DropForeignKey("dbo.tb_DocumentImage", "DocumentId", "dbo.tb_Document");
            DropForeignKey("dbo.tb_Document", "DocumentCategoryId", "dbo.tb_DocumentCategory");
            DropForeignKey("dbo.tb_Document", "DocTypeId", "dbo.tb_Type");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.tb_Review", new[] { "ProductId" });
            DropIndex("dbo.tb_Exam", new[] { "DocumentCategoryId" });
            DropIndex("dbo.Comments", new[] { "PostId" });
            DropIndex("dbo.tb_DocumentImage", new[] { "DocumentId" });
            DropIndex("dbo.tb_Document", new[] { "General_Id" });
            DropIndex("dbo.tb_Document", new[] { "DocTypeId" });
            DropIndex("dbo.tb_Document", new[] { "DocumentCategoryId" });
            DropIndex("dbo.tb_Document", new[] { "CertificateId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.tb_Review");
            DropTable("dbo.tb_Question");
            DropTable("dbo.tb_QuestionDiSan");
            DropTable("dbo.tb_News");
            DropTable("dbo.tb_Historicalrelics");
            DropTable("dbo.tb_Exam");
            DropTable("dbo.tb_Contact");
            DropTable("dbo.Posts");
            DropTable("dbo.Comments");
            DropTable("dbo.tb_ChatFiles");
            DropTable("dbo.tb_General");
            DropTable("dbo.tb_DocumentImage");
            DropTable("dbo.tb_DocumentCategory");
            DropTable("dbo.tb_Type");
            DropTable("dbo.tb_Document");
            DropTable("dbo.tb_Certificate");
            DropTable("dbo.tb_Category");
        }
    }
}
