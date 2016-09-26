import './Menu/MenuModule.ts'
import './ModalMessage/ModalMessageModule.ts'

import {PermissionService} from './PermissionService.ts'

angular.module('common',
    [
        'menu',
        'modalMessage'
    ])
    .service('permissionService', PermissionService);

import './Other/OtherStart.ts'