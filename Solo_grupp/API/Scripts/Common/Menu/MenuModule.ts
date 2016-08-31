import {MenuDirective} from './MenuDirective.ts';

angular.module('menu',
    [

    ])
    .directive('sologruppnavbar', () => new MenuDirective());