import {CreateNews} from '../../../Common/Models/CreateNews.ts'
import {ControllerResult} from '../../../Common/Models/ControllerResult.ts'

export class OnlyNewsService {

    public static $inject: string[] = [
        '$http'
    ];

    constructor(
        private http: ng.IHttpService
    ) {

    }

    public GetNews(id: string): ng.IHttpPromise<ControllerResult<CreateNews>> {
        return this.http.get('/api/news/getnews?id=' + id);
    }
}