import './Authorize/AuthorizeModule.ts'
import './Menu/MenuModule.ts'

angular.module('common',
    [
        'menu',
        'authorize',
    ]);