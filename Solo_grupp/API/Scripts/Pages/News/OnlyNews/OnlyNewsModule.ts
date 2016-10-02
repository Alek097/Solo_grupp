import {OnlyNewsService} from './OnlyNewsService.ts'
import {OnlyNewsController} from './OnlyNewsController.ts'

angular.module('onlyNews', [])
    .controller('onlyNewsController', OnlyNewsController)
    .service('onlyNewsService', OnlyNewsService);