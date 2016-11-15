import {Comment} from '../Models/Comment'
import {ShowCommentsController} from './ShowCommentsController.ts'

export class ShowCommentsDirective implements ng.IDirective {
    public restrict: string = 'E';
    public controller: string = 'showCommentsController';
    public controllerAs: string = 'ctrl';
    public transclude: boolean = true;
    public templateUrl: string = '../../../Scripts/Common/ShowComments/ShowCommentsView.html';

    public link: Function = (scope: ng.IScope, element: JQuery, attrs: ng.IAttributes): void => {
        let comments: Comment[] = JSON.parse(attrs['comments']);

        let innerLevel: number = 0;

        if (attrs['innerlevel'] != undefined && attrs['innerlevel'] !== '') {
            innerLevel = parseFloat(attrs['innerlevel']);
        }

        let elseVar: any = scope;

        let ctrl: ShowCommentsController = elseVar.ctrl;


        ctrl.Comments = comments;
        ctrl.innerLevel = innerLevel;
        ctrl.isRender = true;
    };
}