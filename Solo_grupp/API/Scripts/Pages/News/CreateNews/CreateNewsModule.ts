import {CreateNewsController} from './CreateNewsController.ts'
import {CreateNewsService} from './CreateNewsService.ts'

angular.module('createModule',
    [
        'textAngular'
    ])
    .controller('createNewsController', CreateNewsController)
    .service('createNewsService', CreateNewsService);