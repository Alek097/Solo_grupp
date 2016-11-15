import {ShowCommentsDirective} from './ShowCommentsDirective.ts'
import {ShowCommentsController} from './ShowCommentsController.ts'

angular.module('showComments', [])
    .controller('showCommentsController', ShowCommentsController)
    .directive('showcomments', () => new ShowCommentsDirective());