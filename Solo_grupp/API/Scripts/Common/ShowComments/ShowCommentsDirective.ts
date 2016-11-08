import {Comment} from '../Models/Comment'

export class ShowCommentsDirective implements ng.IDirective {
    public restrict: string = 'E';

    public link: Function = (scope: ng.IScope, element: JQuery, attrs: ng.IAttributes): void => {
        element.append(
            this.render(JSON.parse(attrs['comments']), 0)
        );

    };

    private render(comments: Comment[], innerLevel: number): string {
        let html: string = '';

        for (let i = 0; i < comments.length; i++) {
            let comment: string =
                '<div class="comment col-xs-12" style="margin-left:' + innerLevel * 15 + 'px">' +
                '<a href="/#/User/' + comments[i].Author.Id + '">' +
                '<div class="col-xs-3 col-md-2 comment-usrinf">' +
                '<h5>' + comments[i].Author.FullName + '</h5>' +
                '<h6>' + comments[i].CreateDate + '</h6>' +
                '</div>' +
                '</a>' +
                '<div class="col-xs-9 col-md-10 comment-body">' +
                '<p>' + comments[i].Text + '</p>' +
                '</div>' +
                '</div>';

            if (comments[i].Comments != undefined && comments[i].Comments.length > 0) {
                comment += '<div>' + this.render(comments[i].Comments, innerLevel + 1) + '</div>';
            }

            html += comment;

        }

        return html;
    }
}