import './Pages/PagesModule.ts'
import './Common/CommonModule.ts'

angular.module('main',
    [
        'common',
        'pages'
    ]);
    //.config(($routeProvider: ng.route.IRouteProvider) =>
    //{
    //    $routeProvider.when('registration/',
    //        <ng.route.IRoute>
    //        {
    //            templateUrl: './Pages/Registration/RegistrationView.html',
    //            controller: 'RegistrationController',
    //            controllerAs: 'registr'
    //        });
    //});