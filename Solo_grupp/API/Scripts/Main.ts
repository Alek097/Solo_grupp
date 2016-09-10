import './Pages/PagesModule.ts'
import './Common/CommonModule.ts'
import {User} from './Common/Models/User.ts'

angular.module('main',
    [
        'ngRoute',
        'ngAnimate',
        'pages',
        'common',
    ])
    .config(($routeProvider: ng.route.IRouteProvider) => {
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
                templateUrl: '../Scripts/Pages/Authentification/SignUp/SignUpView.html',
                controller: 'signUpController',
                controllerAs: 'signUpCtrl'
            });
        $routeProvider.when('/SignUp/:message',
            <ng.route.IRoute>
            {
                templateUrl: '../Scripts/Pages/Authentification/SignUp/SignUpView.html',
                controller: 'signUpController',
                controllerAs: 'signUpCtrl'
            });
        $routeProvider.when('/Error/:httpCode/:message',
            <ng.route.IRoute>
            {
                templateUrl: '../Scripts/Pages/Error/ErrorView.html',
                controller: 'errorController',
                controllerAs: 'error'
            });
        $routeProvider.when('/Message/:message',
            <ng.route.IRoute>
            {
                templateUrl: '../Scripts/Pages/Message/MessageView.html',
                controller: 'messageController',
                controllerAs: 'messageCtrl'
            });
        $routeProvider.when('/SignIn',
            <ng.route.IRoute>
            {
                templateUrl: '../Scripts/Pages/Authentification/SignIn/SignInView.html',
                controller: 'signInController',
                controllerAs: 'signInCtrl'
            });
        $routeProvider.when('/SignIn/:message',
            <ng.route.IRoute>
            {
                templateUrl: '../Scripts/Pages/Authentification/SignIn/SignInView.html',
                controller: 'signInController',
                controllerAs: 'signInCtrl'
            });
        $routeProvider.when('/Replace',
            <ng.route.IRoute>
            {
                templateUrl: '../Scripts/Pages/Authentification/Replace/ReplaceView.html',
                controller: 'replaceController',
                controllerAs: 'replace'
            });
        $routeProvider.when('/Replace/:message',
            <ng.route.IRoute>
            {
                templateUrl: '../Scripts/Pages/Authentification/Replace/ReplaceView.html',
                controller: 'replaceController',
                controllerAs: 'replace'
            });
    });