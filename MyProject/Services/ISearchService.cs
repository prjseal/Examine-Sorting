using Examine;

namespace MyProject.Services
{
    public interface ISearchService
    {
        ISearchResults? Search(string searchTerm);
    }
}
