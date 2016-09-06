import './Pages/PagesModule.ts'
import './Common/CommonModule.ts'
import {Authentification} from './Common/Models/Authentification.ts'
import {User} from './Common/Models/User.ts'

$.get('/api/uses/authentification',
    (data: User) => {

        if (data == undefined) {
            Authentification.isAuthentification = false;
        }
        else {
            Authentification.isAuthentification = true;
            Authentification.user = data;
        }

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
                        templateUrl: '../Scripts/Pages/SignUp/SignUpView.html',
                        controller: 'signUpController',
                        controllerAs: 'signUpCtrl'
                    });
                $routeProvider.when('/SignUp/:message',
                    <ng.route.IRoute>
                    {
                        templateUrl: '../Scripts/Pages/SignUp/SignUpView.html',
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
                        templateUrl: '../Scripts/Pages/SignIn/SignInView.html',
                        controller: 'signInController',
                        controllerAs: 'signInCtrl'
                    });
                $routeProvider.when('/SignIn/:message',
                    <ng.route.IRoute>
                    {
                        templateUrl: '../Scripts/Pages/SignIn/SignInView.html',
                        controller: 'signInController',
                        controllerAs: 'signInCtrl'
                    });
            });
    });