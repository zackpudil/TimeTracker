var TimesheetApp = (function ($) {

    var Timesheet = function (serverModel, projects) {
        var self = ko.mapping.fromJS(serverModel);
        self.savedModel = serverModel;
        self.avaliableProjects = ko.observableArray(projects);

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
                self.Project.Id() != self.savedModel.Project.Id
            );
        });

        self.isValid = ko.computed(function () {
            if (self.Project.Id() != undefined && self.Project.Id() != 0)
                return self.Total() != 0;
            return true;
        });

        return self;
    };

    var TimesheetViewModel = function (timesheets, projects) {
        var self = this;
        self.savedSheets = timesheets;
        self.projects = ko.observableArray(projects);
        self.timesheets = ko.observableArray(timesheets.slice(0));

        self.addNewTimesheet = function () {
            $.get('Timesheet/New', function (data) { self.timesheets.push(new Timesheet(data)); }, "json");
        };

        self.removeTimesheet = function (timesheet) {
            self.timesheets.remove(timesheet);
            $('timesheet-form').validate();
        };

        self.saveTimesheets = function () {
            ko.utils.postJson(location.href, { savedTimesheets: ko.mapping.toJS(self.timesheets) });
        };

        //determin if there are unsaved changes if a timesheet has changed or the lengths are different
        self.hasUnsavedChanges = ko.computed(function () {
            return (
                ko.utils.arrayFilter(self.timesheets(), function (item) { return item.hasUnsavedChanges(); }).length != 0 ||
                self.timesheets().length != self.savedSheets.length
            );

        });

        self.grandTotal = ko.computed(function () {
            var total = 0;
            $.each(self.timesheets(), function (i, item) {
                total += item.Total();
            });

            return total;
        });

        self.grandTotalIsValid = ko.computed(function () {
            return self.grandTotal() >= 40;
        });

        self.hasTotalErrors = ko.computed(function () {
            return ko.utils.arrayFilter(self.timesheets(), function (item) { return !item.isValid(); }).length != 0;
        });

        self.isValid = ko.computed(function () {
            return (
                ko.utils.arrayFilter(self.timesheets(), function (item) { return !item.isValid(); }).length == 0 &&
                self.grandTotalIsValid()
            );
        });

        //set up subscriptions to give the timesheets choosen project the proper name.
        ko.computed(function () {
            ko.utils.arrayForEach(self.timesheets(), function (timesheet) {
                var selectedProject = ko.utils.arrayFirst(self.projects(), function (project) {
                    return project.Id() == timesheet.Project.Id();
                });

                timesheet.Project.Name(selectedProject ? selectedProject.Name() : "");
            });
        });

        //subscription to filter the list of avliable projects.  Do not allow user to create timesheets with duplicate projects.
        ko.computed(function () {
            var selectedProjects = ko.utils.arrayFilter(
                ko.utils.arrayMap(self.timesheets(), function (timesheet) {
                    return timesheet.Project.Id();
                }),
                function (id) { return id != 0 }
            );

            ko.utils.arrayForEach(self.timesheets(), function (timesheet) {
                var avaliableProjects = ko.utils.arrayFilter(self.projects(), function (project) {
                    return $.inArray(project.Id(), selectedProjects) == -1
                });

                if (timesheet.Project.Id() != undefined && timesheet.Project.Id() != 0)
                    avaliableProjects.push(timesheet.Project);

                timesheet.avaliableProjects(avaliableProjects);
            });
        });
    };

    //the view model is serialized in the view.
    this.start = function (initialData) {
        var projects = ko.utils.arrayMap(initialData.AvaliableProjects, function (item) {
            return ko.mapping.fromJS(item);
        });

        var timesheets = ko.utils.arrayMap(initialData.Timesheets, function (item) {
            return new Timesheet(item, projects);
        });
        var viewModel = new TimesheetViewModel(timesheets, projects);

        ko.applyBindings(viewModel);
    };

    return this;

})(jQuery);