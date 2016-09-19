import {CreateNewsController} from './CreateNewsController.ts'

angular.module('createModule',
    [
        'textAngular'
    ])
    .controller('createNewsController', CreateNewsController);