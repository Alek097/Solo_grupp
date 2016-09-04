import {SignUpController} from './SignUpController.ts'
import {SignUpService} from './SignUpService.ts'

angular.module('signUp', [])
    .controller('signUpController', SignUpController)
    .service('signUpService', SignUpService);