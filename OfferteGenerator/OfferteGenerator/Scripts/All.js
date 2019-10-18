var OfferteGeneratorModelView = new function () {
    var self = this;
    self.DBLocation = ko.observable("Werk");

    self.DBLocations = ["Werk", "Thuis"];
}
ko.applyBindings(OfferteGeneratorModelView);





