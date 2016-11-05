export class ShowCommentsDirective implements ng.IDirective {
    public restrict: string = 'E';
    public templateUrl: string = '../../../Scripts/Common/ShowComments/ShowCommentsView.html';
    public controller: Function = (): void => { };
    public controllerAs: string = 'ctr';

    public link: Function = (scope: ng.IScope, element, attrs: ng.IAttributes): void => {
        this.controller.prototype.comments = attrs['comments'];
        this.controller.prototype.innerLevel = attrs['innerlevel'];
    };
}