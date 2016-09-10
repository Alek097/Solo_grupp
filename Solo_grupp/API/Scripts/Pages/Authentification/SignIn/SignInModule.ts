import {SignInController} from './SignInController.ts'
import {SignInService} from './SignInService.ts'

angular.module('signIn', [])
    .controller('signInController', SignInController)
    .service('signInService', SignInService);