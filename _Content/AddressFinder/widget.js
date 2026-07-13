namespace("ExamplePlugin");

// This is the provider configuration WIDGET for the Example Address Finder.
//
// The 'Address Lookup' global feature embeds this widget inline (via
// $ui.widgets.loadWidget) when you select this provider as the address lookup
// provider. It is NOT opened as a stack panel, so there is no modal header or
// ribbon here - the hosting blade owns that chrome and its own Save/Close buttons.
//
// The host blade drives the widget lifecycle:
//   - it constructs this view model,
//   - calls load(config) to populate the UI from the saved configuration,
//   - reads validationErrorCount() to enable/disable its Save button,
//   - calls save() to get the config object to persist.
//
// The 'id' in addressFinder.widget.json must match both this constructor's full
// namespace path and the 'ConfigWidget' value returned by
// AddressFinderSearchProvider.cs ("ExamplePlugin.AddressFinder").
ExamplePlugin.AddressFinder = function () {
    var self = this;

    self.resultPrefix = ko.observable();

    self.validation = {
        resultPrefix: Validator.required(self, self.resultPrefix, "Result Prefix is required")
    };

    self.validationErrorCount = ko.pureComputed(function () {
        var count = 0;
        if (self.validation.resultPrefix()) count++;

        return count;
    });
};

// Called by the host blade to populate the widget from the saved configuration.
ExamplePlugin.AddressFinder.prototype.load = function (config) {
    var self = this;
    if (!config) return;

    self.resultPrefix(config.resultPrefix);
};

// Called by the host blade when the feature is saved. The object returned here is
// serialised to JSON and provided to your provider's methods as the 'config'
// argument (see AddressFinderSearchProvider.SearchAddresses). Notice this model
// matches 'AddressFinderSearchProviderConfig.cs', which is used to deserialise
// this JSON back into a C# object - so the property names must line up.
ExamplePlugin.AddressFinder.prototype.save = function () {
    var self = this;

    return {
        resultPrefix: self.resultPrefix()
    };
};
