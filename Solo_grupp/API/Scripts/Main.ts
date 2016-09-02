import './Pages/PagesModule.ts'
import './Common/CommonModule.ts'

angular.module('main',
    [
        'ngRoute',
        'ngAnimate',
        'pages',
        'common',
    ])
    .config(($routeProvider: ng.route.IRouteProvider) =>
    {
        $routeProvider.when('/Home',
            <ng.route.IRoute>
            {
                templateUrl: '../Scripts/Pages/Home/HomeView.html',
            });
        $routeProvider.when('/',
            <ng.route.IRoute>
            {
                templateUrl: '../Scripts/Pages/Home/HomeView.html',
            });
        $routeProvider.when('/SignUp',
            <ng.route.IRoute>
            {
                templateUrl: '../Scripts/Pages/SignUp/SignUpView.html',
            });
        $routeProvider.when('/Error?httpCode=:httpCode&message=:message',
            <ng.route.IRoute>
            {
                templateUrl: '../Scripts/Common/Error/ErrorView.ts',
                controller: 'errorController',
                controllerAs: 'error'
            });
    });