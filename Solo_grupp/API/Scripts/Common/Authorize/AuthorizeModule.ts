import {AuthorizeController} from './AuthorizeController.ts'
import {AuthorizeService} from './AuthorizeService.ts'

angular.module('authorize',
    [

    ])
    .controller('authorizeController', AuthorizeController)
    .service('authorizeService', AuthorizeService);