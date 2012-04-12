var TimesheetApp = (function ($) {

    var Project = function (serverModel) {
        var self = ko.mapping.fromJS(serverModel);
        return self;
    };

    var Timesheet = function (serverModel) {
        var self = ko.mapping.fromJS(serverModel);
        self.savedModel = serverModel;

        self.Project = ko.observable(new Project(serverModel.Project));

        self.Total = ko.computed(function () {
            return common.parseNumber(self.Monday()) +
                common.parseNumber(self.Tuesday()) +
                common.parseNumber(self.Wednesday()) +
                common.parseNumber(self.Thursday()) +
                common.parseNumber(self.Friday());
        });

        self.hasUnsavedChanges = ko.computed(function () {
            return (
                self.Total() != self.savedModel.Total ||
                self.Project().Id != self.savedModel.Project.Id
            );
        });

        return self;
    };

    var TimesheetViewModel = function (timesheets, projects) {
        var self = this;
        self.savedSheets = timesheets;
        self.projects = ko.observableArray(ko.utils.arrayMap(projects, function (item) { return new Project(item); }));
        self.timesheets = ko.observableArray(timesheets.slice(0));

        self.addNewTimesheet = function () {
            $.get('Timesheet/New', function (data) { self.timesheets.push(new Timesheet(data)); }, "json");
        };

        self.removeTimesheet = function (timesheet) {
            self.timesheets.remove(timesheet);
            $('timesheet-form').validate();
        };

        self.saveTimesheets = function () {
            alert(ko.mapping.toJSON(self.timesheets()));
        };

        //determin if there are unsaved changes if a timesheet has changed or the lengths are different
        self.hasUnsavedChanges = ko.computed(function () {
            var unSaved = false;
            $.each(self.timesheets(), function (i, item) {
                unSaved = item.hasUnsavedChanges();
                return !unSaved;
            });

            return unSaved || self.timesheets().length != self.savedSheets.length;
        });

        self.grandTotal = ko.computed(function () {
            var total = 0;
            $.each(self.timesheets(), function (i, item) {
                total += item.Total();
            });

            return total;
        });
    };

    //the view model is serialized in the view.
    this.start = function (initialData) {
        var timesheets = ko.utils.arrayMap(initialData.Timesheets, function (item) {
            return new Timesheet(item);
        });
        var viewModel = new TimesheetViewModel(timesheets, initialData.AvaliableProjects);

        ko.applyBindings(viewModel);
    };

    return this;

})(jQuery);