import {ReplaceController} from './ReplaceController.ts'
import {ReplaceService} from './ReplaceService.ts'

angular.module('replace', [])
    .controller('replaceController', ReplaceController)
    .service('replaceService', ReplaceService);