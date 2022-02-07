using Microsoft.EntityFrameworkCore.Migrations;

namespace PostFeedback.DAL.EF.Migrations
{
    public partial class TriggerCorrected : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"create trigger UserDeleted
                                    on Users
                                    instead of delete
                                    as
                                    begin
                                    set nocount on
                                    delete UserFeedbacks from UserFeedbacks
                                    join deleted
                                    on UserFeedbacks.ItemId = deleted.Id;
                                    delete Posts from Posts
                                    join deleted
                                    on Posts.OwnerId = deleted.Id;
                                    delete Users from Users
                                    join deleted 
                                    on Users.Id = deleted.Id;
                                    end");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"drop trigger UserDeleted");
        }
    }
}
