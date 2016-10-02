import './News/NewsModule.ts'
import './CreateNews/CreateNewsModule.ts'
import './OnlyNews/OnlyNewsmodule.ts'

angular.module('mainNews',
    [
        'news',
        'createModule',
        'onlyNews'
    ]);