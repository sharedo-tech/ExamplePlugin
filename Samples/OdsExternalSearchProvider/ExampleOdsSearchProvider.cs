using System.Collections.Generic;
using System.Threading.Tasks;
using Sharedo.Core;
using Sharedo.Core.Case.Modules.ODS.ExternalSearch;
using Sharedo.Core.Case.Modules.ODS.ExternalSearch.Dispatcher;
using Sharedo.Core.Case.Modules.ODS.ExternalSearch.Providers;
using Sharedo.Core.Case.Modules.ODS.ExternalSearch.Providers._ReturnModels;
using Sharedo.Core.Net.Http;

namespace ExamplePlugin.Samples.OdsExternalSearchProvider;

public class ExampleOdsSearchProvider : IOdsExternalSearchProvider
{
    public string SystemName => "example-provider";
    public string DisplayName => "Example Provider";
    public string Description => "An example ODS external search provider that returns hard-coded results.";
    public string IconClass => "fa-search";
    public string ConfigurationPanel => "ExamplePlugin.OdsExternalSearchProvider";
    public string ResultsTemplate { get; }

    public IEnumerable<OdsExternalSearchType> SupportsTypes => new[] { OdsExternalSearchType.Organisations };

    private readonly ICoreHttpRetryClient _http;
    
    public ExampleOdsSearchProvider(ICoreHttpRetryClient http)
    {
        // This HTTP client contains built-in logic for retries and logging.
        // Use it when making HTTP calls
        _http = http;
    }
    
    /// <summary>
    /// Return the initial search result set 
    /// </summary>
    /// <param name="config">
    /// A JSON string containing your global feature config
    /// (see ./_Content/OdsExternalSearchProvider/panel.js)
    /// </param>
    /// <param name="types">The ODS type to search for (if your provider only supports one ODS type in SupportsTypes, you can ignore this)</param>
    /// <param name="query">The user's search query</param>
    /// <param name="rowsPerPage">The number of rows to return on this page</param>
    /// <param name="pageNumber">The page number to return (the first page is '0')</param>
    public async Task<OdsExternalSearchResult> Search(string config, IEnumerable<OdsExternalSearchType> types, string query, int rowsPerPage, int pageNumber)
    {
        var configModel = Json.Deserialise<ExampleOdsSearchProviderConfig>(config);

        // Perform your real search here, and map the results to either
        // OdsExternalSearchResultOrganisation or OdsExternalSearchResultPerson
        // In a real provider, this would likely be an API call to a third-party
        // e.g. 'var response = await _http.GetAsync(url).ConfigureAwait(false);'
        var resultSet = new List<IOdsExternalSearchResult>
        {
            new OdsExternalSearchResultOrganisation
            {
                Name = $"{configModel.ResultPrefix} Organisation 1",
                CompanyNumber = "12345678",
                ProvidersReference = "org-1",
                
                // Set this to 'true' if this is a summary result that we need to 
                // enrich before creating the real ODS entity (see 'Expand()' below)
                RequiresExpansion = false
            }
        };
        
        return new OdsExternalSearchResult
        {
            Success = true,
            TotalResults = resultSet.Count,
            RowsPerPage = resultSet.Count,
            CurrentPage = pageNumber,
            Results = resultSet
        };
    }

    /// <summary>
    /// This is called when a result is selected in the ODS external search panel where 'RequiresExpansion' is true.
    /// Some providers only provide summary information in the initial search results, in which case
    /// this call can be used to expand/enrich those results before they are used to create a real ODS
    /// entity.
    /// If you can return a complete entity in the `Search()` method, you can return null from this method.
    /// </summary>
    public Task<OdsExternalSearchExpandedResult> Expand(string reference, IEnumerable<OdsExternalSearchType> type)
    {
        return Task.FromResult(default(OdsExternalSearchExpandedResult));
    }
}