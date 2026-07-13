using System.Collections.Generic;
using System.Threading.Tasks;
using Sharedo.Core;
using Sharedo.Core.Case.Modules.AddressLookup;
using Sharedo.Core.Case.Modules.AddressLookup.Models;
using Sharedo.Core.Net.Http;

namespace ExamplePlugin.Samples.AddressFinder;

public class AddressFinderSearchProvider : IAddressLookupProvider
{
    public string SystemName => "example-address-finder";
    public string DisplayName => "Example Address Finder";
    public string ConfigWidget => "ExamplePlugin.AddressFinder";

    private readonly ICoreHttpRetryClient _http;

    public AddressFinderSearchProvider(ICoreHttpRetryClient http)
    {
        // This HTTP client contains built-in logic for retries and logging.
        // Use it when making HTTP calls
        _http = http;
    }

    /// <summary>
    /// Return the list of address suggestions matching the user's search term.
    /// </summary>
    /// <param name="term">The user's search query</param>
    /// <param name="config">
    /// A JSON string containing your global feature config
    /// (see ./_Content/AddressFinder/panel.js)
    /// </param>
    public async Task<List<AddressSummary>> SearchAddresses(string term, string config)
    {
        var configModel = Json.Deserialise<AddressFinderSearchProviderConfig>(config);

        // Perform your real search here, and map the results to AddressSummary.
        // In a real provider, this would likely be an API call to a third-party
        // e.g. 'var response = await _http.GetAsync(url).ConfigureAwait(false);'
        return new List<AddressSummary>
        {
            new AddressSummary
            {
                Udprn = "org-1",
                StreetAddress = $"{configModel.ResultPrefix} 1 Example Street",
                Place = $"{configModel.ResultPrefix} London"
            }
        };
    }

    /// <summary>
    /// Expand a summary result (selected by the user) into the full address detail
    /// used to populate the ODS location.
    /// </summary>
    /// <param name="id">The identifier of the summary result (see <see cref="AddressSummary.Udprn"/>)</param>
    /// <param name="config">A JSON string containing your global feature config</param>
    public async Task<AddressDetail> GetAddressById(string id, string config)
    {
        var configModel = Json.Deserialise<AddressFinderSearchProviderConfig>(config);

        // Look up the full address for 'id' from the third-party system here.
        return new AddressDetail
        {
            Udprn = id,
            CompanyName = $"{configModel.ResultPrefix} Example Company Ltd",
            BuildingName = $"{configModel.ResultPrefix} Example House",
            BuildingNumber = $"{configModel.ResultPrefix} 1",
            AddressLine1 = $"{configModel.ResultPrefix} 1 Example Street",
            AddressLine2 = $"{configModel.ResultPrefix} Example District",
            AddressLine3 = $"{configModel.ResultPrefix} Example Area",
            AddressLine4 = $"{configModel.ResultPrefix} Example Region",
            County = $"{configModel.ResultPrefix} Greater London",
            PostTown = $"{configModel.ResultPrefix} London",
            Postcode = $"{configModel.ResultPrefix} EC1A 1AA",
            CountrySystemName = "gbr",
            Latitude = "51.5074",
            Longitude = "-0.1278"
        };
    }

    /// <summary>
    /// Resolve a free-text location (optionally scoped to a country) to a set of coordinates.
    /// </summary>
    /// <param name="location">The free-text location to geocode</param>
    /// <param name="country">The country system name to scope the search to (may be null)</param>
    /// <param name="config">A JSON string containing your global feature config</param>
    public async Task<GeocodeDetail> Geocode(string location, string country, string config)
    {
        var configModel = Json.Deserialise<AddressFinderSearchProviderConfig>(config);

        // Call your third-party geocoding service here.
        return new GeocodeDetail
        {
            Name = $"{configModel.ResultPrefix} {location}",
            Latitude = "51.5074",
            Longitude = "-0.1278"
        };
    }
}
