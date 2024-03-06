using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.GreenHouse.Indexes;
using YesSql.Sql;

namespace OrchardCore.GreenHouse.Migrations
{
    public class ReviewPartMigrations : DataMigration
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public ReviewPartMigrations(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        public async Task<int> CreateAsync()
        {
            // Create database schema for the ReviewPartIndex
            await SchemaBuilder.CreateMapIndexTableAsync<ReviewPartIndex>(table => table
                .Column<string>("Name", column => column.WithLength(50))
                .Column<string>("Email", column => column.WithLength(50))
                .Column<string>("Review", column => column.Unlimited())
                .Column<int>("Rating")
                .Column<string>("ContentItemId", column => column.WithLength(26))
            );

            // content part add on.
            await _contentDefinitionManager.AlterPartDefinitionAsync("ReviewPart", part => part
                .WithDisplayName("Leave a Review")
                .WithDescription("Allows users to leave a review and rating."));

            return 1;
        }
    }
}
