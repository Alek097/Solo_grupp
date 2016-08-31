import './Pages/PagesModule.ts'
import './Common/CommonModule.ts'

angular.module('main',
    [
        'pages',
        'common',
        'ngRoute',
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
    });