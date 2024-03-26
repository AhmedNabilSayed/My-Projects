namespace WebAp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.comments",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        commenttext = c.String(),
                        Post_id = c.Int(),
                        user_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.posts", t => t.Post_id)
                .ForeignKey("dbo.users", t => t.user_id)
                .Index(t => t.Post_id)
                .Index(t => t.user_id);
            
            CreateTable(
                "dbo.posts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        posttext = c.String(),
                        privet = c.Int(nullable: false),
                        user_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.users", t => t.user_id)
                .Index(t => t.user_id);
            
            CreateTable(
                "dbo.reacts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        idpost = c.Int(nullable: false),
                        iduser = c.Int(nullable: false),
                        rea = c.Int(nullable: false),
                        post_id = c.Int(),
                        user_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.posts", t => t.post_id)
                .ForeignKey("dbo.users", t => t.user_id)
                .Index(t => t.post_id)
                .Index(t => t.user_id);
            
            CreateTable(
                "dbo.users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        fname = c.String(),
                        photo = c.String(),
                        city = c.String(),
                        countey = c.String(),
                        email = c.String(),
                        phono = c.String(),
                        lname = c.String(),
                        username = c.String(),
                        password = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.fr_re",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        sendid = c.Int(nullable: false),
                        resid = c.Int(nullable: false),
                        resver_id = c.Int(),
                        sender_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.users", t => t.resver_id)
                .ForeignKey("dbo.users", t => t.sender_id)
                .Index(t => t.resver_id)
                .Index(t => t.sender_id);
            
            CreateTable(
                "dbo.friends",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        fe1 = c.Int(nullable: false),
                        fr2 = c.Int(nullable: false),
                        friendone_id = c.Int(),
                        friendtow_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.users", t => t.friendone_id)
                .ForeignKey("dbo.users", t => t.friendtow_id)
                .Index(t => t.friendone_id)
                .Index(t => t.friendtow_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.friends", "friendtow_id", "dbo.users");
            DropForeignKey("dbo.friends", "friendone_id", "dbo.users");
            DropForeignKey("dbo.fr_re", "sender_id", "dbo.users");
            DropForeignKey("dbo.fr_re", "resver_id", "dbo.users");
            DropForeignKey("dbo.reacts", "user_id", "dbo.users");
            DropForeignKey("dbo.posts", "user_id", "dbo.users");
            DropForeignKey("dbo.comments", "user_id", "dbo.users");
            DropForeignKey("dbo.reacts", "post_id", "dbo.posts");
            DropForeignKey("dbo.comments", "Post_id", "dbo.posts");
            DropIndex("dbo.friends", new[] { "friendtow_id" });
            DropIndex("dbo.friends", new[] { "friendone_id" });
            DropIndex("dbo.fr_re", new[] { "sender_id" });
            DropIndex("dbo.fr_re", new[] { "resver_id" });
            DropIndex("dbo.reacts", new[] { "user_id" });
            DropIndex("dbo.reacts", new[] { "post_id" });
            DropIndex("dbo.posts", new[] { "user_id" });
            DropIndex("dbo.comments", new[] { "user_id" });
            DropIndex("dbo.comments", new[] { "Post_id" });
            DropTable("dbo.friends");
            DropTable("dbo.fr_re");
            DropTable("dbo.users");
            DropTable("dbo.reacts");
            DropTable("dbo.posts");
            DropTable("dbo.comments");
        }
    }
}
