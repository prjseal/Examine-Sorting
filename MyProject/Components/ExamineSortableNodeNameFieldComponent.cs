using Examine.Lucene;
using Examine;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Infrastructure.Examine;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core;

namespace MyProject.Components
{
    public class ExamineSortableNodeNameFieldComponent : IComponent
    {
        private readonly IOptionsMonitor<LuceneDirectoryIndexOptions> _indexOptions;

        public ExamineSortableNodeNameFieldComponent(
            IOptionsMonitor<LuceneDirectoryIndexOptions> indexOptions)
        {
            _indexOptions = indexOptions;
        }

        public void Initialize()
        {
            var internalIndexOptions = _indexOptions.Get(Constants.UmbracoIndexes.InternalIndexName);

            if (internalIndexOptions == null)
                throw new InvalidOperationException($"Unable to obtain options for {Constants.UmbracoIndexes.InternalIndexName}.");

            EnsureSortableNodeNameField(internalIndexOptions);
        }

        private void EnsureSortableNodeNameField(LuceneDirectoryIndexOptions internalIndexOptions)
        {
            var nodeNameSortableFieldDefinition = new FieldDefinition(
                UmbracoExamineFieldNames.NodeNameFieldName, // nodeName
                FieldDefinitionTypes.FullTextSortable);

            internalIndexOptions.FieldDefinitions.AddOrUpdate(nodeNameSortableFieldDefinition);
        }

        public void Terminate() { }
    }
}
