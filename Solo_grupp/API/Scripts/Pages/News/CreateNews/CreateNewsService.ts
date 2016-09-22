export class CreateNewsService {
    public static $inject: string[] =
    [
        '$http'
    ];

    constructor(
        private http: ng.IHttpService
    ) {

    }

    public uploadImage(data: FormData): ng.IHttpPromise<string[]> {
        return this.http.post('/api/CreateNews/UploadImage', data);
    }
}