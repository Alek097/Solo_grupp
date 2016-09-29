import {CreateNews} from '../../../Common/Models/CreateNews.ts'
import {ControllerResult} from '../../../Common/Models/ControllerResult.ts'

export class CreateNewsService {
    public static $inject: string[] =
    [
        '$http'
    ];

    constructor(
        private http: ng.IHttpService
    ) {

    }
    public createNews(data: CreateNews): ng.IHttpPromise<ControllerResult> {
        return this.http.post('/api/CreateNews/Create', data);
    }

    public uploadImage(data: FormData): JQueryXHR {

        return $.ajax({
            type: "POST",
            url: '/api/CreateNews/UploadImage',
            contentType: false,
            processData: false,
            data: data
        });
    }
}