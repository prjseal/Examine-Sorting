using Examine;
using Examine.Search;
using Lucene.Net.Analysis.Core;
using Umbraco.Cms.Core;
using Umbraco.Cms.Infrastructure.Examine;

namespace MyProject.Services
{
    public class SearchService : ISearchService
    {
        private readonly IExamineManager _examineManager;

        public SearchService(IExamineManager examineManager)
        {
            _examineManager = examineManager ?? throw new ArgumentNullException(nameof(examineManager));

        }

        public ISearchResults? Search(string searchTerm)
        {
            if (!_examineManager.TryGetIndex(Constants.UmbracoIndexes.ExternalIndexName, out IIndex? index))
            {
                return null;
            }

            IBooleanOperation? query = index.Searcher.CreateQuery(IndexTypes.Content)
                .GroupedNot(new[] { "umbracoNaviHide" }, new[] { "1" });

            string[]? terms = !string.IsNullOrWhiteSpace(searchTerm)
                ? searchTerm.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Where(x => !StopAnalyzer.ENGLISH_STOP_WORDS_SET.Contains(x.ToLower()) && x.Length > 2).ToArray() : null;

            if (terms != null && terms.Length > 0)
            {
                query!.And().Group(q => q
                    .GroupedOr(new[] { "nodeName" }, terms), BooleanOperation.Or);
            }

            return query.OrderBy(new SortableField("nodeName", SortType.String)).Execute();
        }
    }
}
