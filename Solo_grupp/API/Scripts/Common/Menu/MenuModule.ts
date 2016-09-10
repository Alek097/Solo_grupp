import {MenuDirective} from './MenuDirective.ts'
import {MenuController} from './MenuController.ts'
import {AuthorizeService} from './AuthorizeService.ts'


angular.module('menu',
    [

    ])
    .controller('menuController', MenuController)
    .service('authorizeService', AuthorizeService)
    .directive('sologruppnavbar', () => new MenuDirective());