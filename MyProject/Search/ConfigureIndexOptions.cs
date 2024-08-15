using Examine;
using Examine.Lucene;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Infrastructure.Examine;

namespace MyProject.Search
{
    public class ConfigureIndexOptions : IConfigureNamedOptions<LuceneDirectoryIndexOptions>
    {
        private readonly ILoggerFactory _loggerFactory;

        public ConfigureIndexOptions(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public void Configure(string name, LuceneDirectoryIndexOptions options)
        {
            var nodeNameSortableFieldDefinition = new FieldDefinition(
                UmbracoExamineFieldNames.NodeNameFieldName, // nodeName
                FieldDefinitionTypes.FullTextSortable);

            options.FieldDefinitions.AddOrUpdate(nodeNameSortableFieldDefinition);
        }

        public void Configure(LuceneDirectoryIndexOptions options) => Configure(string.Empty, options);
    }
}
