import {CreateNews} from '../../../Common/Models/CreateNews.ts'
import {MoveTo} from '../../../Common/Models/MoveTo.ts'

export class CreateNewsService {
    public static $inject: string[] =
    [
        '$http'
    ];

    constructor(
        private http: ng.IHttpService
    ) {

    }
    public createNews(data: CreateNews): ng.IHttpPromise<MoveTo> {
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