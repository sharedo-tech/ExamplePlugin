namespace("ExamplePlugin");

ExamplePlugin.OdsExternalSearchProvider = function (element, configuration, stackModel) {
    var self = this;

    var defaults = {
        apiKey: ko.observable(),
        locationTypes: ko.observableArray(),
    };

    self.configuration = $.extend(defaults, configuration);

    self.configuration.resultPrefix = ko.observable(ko.unwrap(self.configuration.resultPrefix));    

    self.locationTypes = ko.observableArray();
    
    self.validationErrorCount = ko.computed(function() {
        let errors = 0;
        if (!self.configuration.resultPrefix()) errors++;

        return errors;
    });

    self.ribbon = self.createRibbonBar();
};


ExamplePlugin.OdsExternalSearchProvider.prototype.loadAndBind = function() {
    var self = this;
    
}

ExamplePlugin.OdsExternalSearchProvider.prototype.createRibbonBar = function () {
    var self = this;

    return new Components.Core.RibbonBar.Ribbon(
        {
            alignment: Components.Core.RibbonBar.RibbonAlignment.Right,
            sectionTitles: false
        },
        [
            new Components.Core.RibbonBar.Section({ title: "Actions", useLargeButtons: true, useFlatButtons: true },
                [
                    new Components.Core.RibbonBar.Button({
                        title: "Save & Close",
                        css: "btn-success",
                        icon: "fa-save",
                        callback: self.save.bind(self),
                        disabled: self.validationErrorCount
                    }),
                    new Components.Core.RibbonBar.Button({
                        title: "Close",
                        css: "btn-primary",
                        icon: "fa-times",
                        callback: self.discard.bind(self)
                    })
                ])
        ]
    );
};

ExamplePlugin.OdsExternalSearchProvider.prototype.save = function () {
    var self = this;
    
    // The configuration you pass to this call will be serialised, and provided in your
    // search provider's 'Search()' method when the user makes a search.
    // Notice that this model matches 'ExampleOdsSearchProviderConfig.cs', which is used to 
    // deserialise this JSON back into a C# object.
    $ui.stacks.close(self, ko.toJS(self.configuration));
};

ExamplePlugin.OdsExternalSearchProvider.prototype.discard = function () {
    var self = this;
    $ui.stacks.cancel(self);
};