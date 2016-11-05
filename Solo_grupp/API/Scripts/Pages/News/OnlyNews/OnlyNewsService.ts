import {News} from '../../../Common/Models/News.ts'
import {Comment} from '../../../Common/Models/Comment.ts'
import {ControllerResult} from '../../../Common/Models/ControllerResult.ts'

export class OnlyNewsService {

    public static $inject: string[] = [
        '$http'
    ];

    constructor(
        private http: ng.IHttpService
    ) {

    }

    public GetNews(id: string): ng.IHttpPromise<ControllerResult<News>> {
        return this.http.get('/api/news/getnews?id=' + id);
    }

    public GetComments(id: string): ng.IHttpPromise<ControllerResult<Comment[]>>{
        return this.http.get('/api/news/getcomments?id=' + id);
    }
}