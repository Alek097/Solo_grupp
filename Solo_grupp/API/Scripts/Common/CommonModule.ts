import './Menu/MenuModule.ts'
import './ModalMessage/ModalMessageModule.ts'
import './ShowComments/ShowCommentsModule.ts'

import {PermissionService} from './PermissionService.ts'

angular.module('common',
    [
        'menu',
        'modalMessage',
        'showComments'
    ])
    .service('permissionService', PermissionService);

import './Other/OtherStart.ts'