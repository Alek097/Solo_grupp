import {ModalMessageController} from './ModalMessageController.ts'
import {ModalMessageService} from './ModalMessageService.ts'

angular.module('modalMessage',
    [
        'ui.bootstrap'
    ])
    .controller('modalMessageController', ModalMessageController)
    .service('modalMessageService', ModalMessageService);