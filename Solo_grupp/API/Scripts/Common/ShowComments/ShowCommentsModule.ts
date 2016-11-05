import {ShowCommentsDirective} from './ShowCommentsDirective.ts'

angular.module('showComments', [])
    .directive('showcomments', () => new ShowCommentsDirective());