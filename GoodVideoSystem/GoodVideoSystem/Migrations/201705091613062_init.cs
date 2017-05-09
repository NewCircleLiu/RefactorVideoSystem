namespace GoodVideoSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Videos",
                c => new
                    {
                        VideoID = c.Int(nullable: false, identity: true),
                        ls_video_id = c.Int(nullable: false),
                        ls_video_uuid = c.String(nullable: false),
                        VideoName = c.String(nullable: false),
                        CodeCounts = c.Int(nullable: false),
                        CodeUsed = c.Int(nullable: false),
                        CodeNotUsed = c.Int(nullable: false),
                        VideoImageLocal = c.String(nullable: false),
                        createTime = c.DateTime(nullable: false),
                        modifyTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.VideoID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        userId = c.Int(nullable: false, identity: true),
                        phone = c.String(nullable: false),
                        userUniqueCode = c.String(nullable: false),
                        createTime = c.DateTime(nullable: false),
                        modifyTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.userId);
            
            CreateTable(
                "dbo.ActionLogs",
                c => new
                    {
                        actionLogId = c.Int(nullable: false, identity: true),
                        action = c.String(nullable: false),
                        userId = c.String(nullable: false),
                        createTime = c.DateTime(nullable: false),
                        modifyTime = c.DateTime(nullable: false),
                        user_userId = c.Int(),
                    })
                .PrimaryKey(t => t.actionLogId)
                .ForeignKey("dbo.Users", t => t.user_userId)
                .Index(t => t.user_userId);
            
            CreateTable(
                "dbo.Suggests",
                c => new
                    {
                        suggestId = c.Int(nullable: false, identity: true),
                        text = c.String(nullable: false),
                        userPhone = c.String(nullable: false),
                        userId = c.Int(nullable: false),
                        createTime = c.DateTime(nullable: false),
                        modifyTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.suggestId)
                .ForeignKey("dbo.Users", t => t.userId, cascadeDelete: true)
                .Index(t => t.userId);
            
            CreateTable(
                "dbo.Managers",
                c => new
                    {
                        managerId = c.Int(nullable: false, identity: true),
                        account = c.String(nullable: false),
                        password = c.String(nullable: false),
                        email = c.String(nullable: false),
                        phone = c.String(nullable: false),
                        createTime = c.DateTime(nullable: false),
                        modifyTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.managerId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        productId = c.Int(nullable: false, identity: true),
                        price = c.Int(nullable: false),
                        name = c.String(nullable: false),
                        img = c.String(nullable: false),
                        url = c.String(nullable: false),
                        createTime = c.DateTime(nullable: false),
                        modifyTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.productId);
            
            CreateTable(
                "dbo.ExceptionLogs",
                c => new
                    {
                        exceptionLogId = c.Int(nullable: false, identity: true),
                        context = c.String(nullable: false),
                        createTime = c.DateTime(nullable: false),
                        modifyTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.exceptionLogId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Suggests", new[] { "userId" });
            DropIndex("dbo.ActionLogs", new[] { "user_userId" });
            DropForeignKey("dbo.Suggests", "userId", "dbo.Users");
            DropForeignKey("dbo.ActionLogs", "user_userId", "dbo.Users");
            DropTable("dbo.ExceptionLogs");
            DropTable("dbo.Products");
            DropTable("dbo.Managers");
            DropTable("dbo.Suggests");
            DropTable("dbo.ActionLogs");
            DropTable("dbo.Users");
            DropTable("dbo.Videos");
        }
    }
}
