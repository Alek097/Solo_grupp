import './Authorize/AuthorizeModule.ts'
import './Menu/MenuModule.ts'
import './Error/ErrorModule.ts'

angular.module('common',
    [
        'menu',
        'authorize',
        'error'
    ]);