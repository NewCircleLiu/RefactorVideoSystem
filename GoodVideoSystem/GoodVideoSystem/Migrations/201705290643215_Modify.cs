namespace GoodVideoSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Codes", "BindedDeviceCount", c => c.Int(nullable: false));
            AddColumn("dbo.Codes", "DeviceUniqueCode", c => c.String());
            AddColumn("dbo.Users", "Username", c => c.String());
            AddColumn("dbo.Users", "InviteCodes", c => c.String(nullable: false));
            DropColumn("dbo.Users", "UserUniqueCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "UserUniqueCode", c => c.String(nullable: false));
            DropColumn("dbo.Users", "InviteCodes");
            DropColumn("dbo.Users", "Username");
            DropColumn("dbo.Codes", "DeviceUniqueCode");
            DropColumn("dbo.Codes", "BindedDeviceCount");
        }
    }
}
