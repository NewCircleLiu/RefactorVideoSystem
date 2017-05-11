namespace GoodVideoSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ActionLogs", "user_userId", "dbo.Users");
            DropForeignKey("dbo.Suggests", "userId", "dbo.Users");
            DropIndex("dbo.ActionLogs", new[] { "user_userId" });
            DropIndex("dbo.Suggests", new[] { "userId" });
            CreateTable(
                "dbo.Codes",
                c => new
                    {
                        CodeID = c.Int(nullable: false, identity: true),
                        CodeValue = c.String(nullable: false),
                        CodeStatus = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        VideoID = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        ModifyTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CodeID)
                .ForeignKey("dbo.Videos", t => t.VideoID, cascadeDelete: true)
                .Index(t => t.VideoID);
            
            AlterColumn("dbo.Videos", "CreateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Videos", "ModifyTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "UserId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Users", "Phone", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "UserUniqueCode", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "CreateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "ModifyTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ActionLogs", "CreateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ActionLogs", "ModifyTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ActionLogs", "user_UserId", c => c.Int());
            AlterColumn("dbo.Suggests", "SuggestId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Suggests", "Text", c => c.String(nullable: false));
            AlterColumn("dbo.Suggests", "UserPhone", c => c.String(nullable: false));
            AlterColumn("dbo.Suggests", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Suggests", "CreateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Suggests", "ModifyTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Managers", "ManagerId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Managers", "Account", c => c.String(nullable: false));
            AlterColumn("dbo.Managers", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Managers", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Managers", "Phone", c => c.String(nullable: false));
            AlterColumn("dbo.Managers", "CreateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Managers", "ModifyTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Products", "ProductId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Products", "Price", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Img", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Url", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "CreateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Products", "ModifyTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ExceptionLogs", "ExceptionLogId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.ExceptionLogs", "Context", c => c.String(nullable: false));
            AlterColumn("dbo.ExceptionLogs", "CreateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ExceptionLogs", "ModifyTime", c => c.DateTime(nullable: false));
            DropPrimaryKey("dbo.Users", new[] { "userId" });
            AddPrimaryKey("dbo.Users", "UserId");
            DropPrimaryKey("dbo.Suggests", new[] { "suggestId" });
            AddPrimaryKey("dbo.Suggests", "SuggestId");
            DropPrimaryKey("dbo.Managers", new[] { "managerId" });
            AddPrimaryKey("dbo.Managers", "ManagerId");
            DropPrimaryKey("dbo.Products", new[] { "productId" });
            AddPrimaryKey("dbo.Products", "ProductId");
            DropPrimaryKey("dbo.ExceptionLogs", new[] { "exceptionLogId" });
            AddPrimaryKey("dbo.ExceptionLogs", "ExceptionLogId");
            AddForeignKey("dbo.ActionLogs", "user_UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Suggests", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            CreateIndex("dbo.ActionLogs", "user_UserId");
            CreateIndex("dbo.Suggests", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Suggests", new[] { "UserId" });
            DropIndex("dbo.ActionLogs", new[] { "user_UserId" });
            DropIndex("dbo.Codes", new[] { "VideoID" });
            DropForeignKey("dbo.Suggests", "UserId", "dbo.Users");
            DropForeignKey("dbo.ActionLogs", "user_UserId", "dbo.Users");
            DropForeignKey("dbo.Codes", "VideoID", "dbo.Videos");
            DropPrimaryKey("dbo.ExceptionLogs", new[] { "ExceptionLogId" });
            AddPrimaryKey("dbo.ExceptionLogs", "exceptionLogId");
            DropPrimaryKey("dbo.Products", new[] { "ProductId" });
            AddPrimaryKey("dbo.Products", "productId");
            DropPrimaryKey("dbo.Managers", new[] { "ManagerId" });
            AddPrimaryKey("dbo.Managers", "managerId");
            DropPrimaryKey("dbo.Suggests", new[] { "SuggestId" });
            AddPrimaryKey("dbo.Suggests", "suggestId");
            DropPrimaryKey("dbo.Users", new[] { "UserId" });
            AddPrimaryKey("dbo.Users", "userId");
            AlterColumn("dbo.ExceptionLogs", "modifyTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ExceptionLogs", "createTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ExceptionLogs", "context", c => c.String(nullable: false));
            AlterColumn("dbo.ExceptionLogs", "exceptionLogId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Products", "modifyTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Products", "createTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Products", "url", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "img", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "name", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "price", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "productId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Managers", "modifyTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Managers", "createTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Managers", "phone", c => c.String(nullable: false));
            AlterColumn("dbo.Managers", "email", c => c.String(nullable: false));
            AlterColumn("dbo.Managers", "password", c => c.String(nullable: false));
            AlterColumn("dbo.Managers", "account", c => c.String(nullable: false));
            AlterColumn("dbo.Managers", "managerId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Suggests", "modifyTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Suggests", "createTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Suggests", "userId", c => c.Int(nullable: false));
            AlterColumn("dbo.Suggests", "userPhone", c => c.String(nullable: false));
            AlterColumn("dbo.Suggests", "text", c => c.String(nullable: false));
            AlterColumn("dbo.Suggests", "suggestId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.ActionLogs", "user_userId", c => c.Int());
            AlterColumn("dbo.ActionLogs", "modifyTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ActionLogs", "createTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "modifyTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "createTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "userUniqueCode", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "phone", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "userId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Videos", "modifyTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Videos", "createTime", c => c.DateTime(nullable: false));
            DropTable("dbo.Codes");
            CreateIndex("dbo.Suggests", "userId");
            CreateIndex("dbo.ActionLogs", "user_userId");
            AddForeignKey("dbo.Suggests", "userId", "dbo.Users", "userId", cascadeDelete: true);
            AddForeignKey("dbo.ActionLogs", "user_userId", "dbo.Users", "userId");
        }
    }
}
