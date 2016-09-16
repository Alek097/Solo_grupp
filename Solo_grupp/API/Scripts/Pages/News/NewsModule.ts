import './News/NewsModule.ts'
import './CreateNews/CreateNewsModule.ts'

angular.module('mainNews',
    [
        'news',
        'createModule'
    ]);