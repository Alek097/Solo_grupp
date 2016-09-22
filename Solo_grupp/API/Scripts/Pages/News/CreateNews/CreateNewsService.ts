export class CreateNewsService {
    public static $inject: string[] =
    [
        '$http'
    ];

    constructor(
        private http: ng.IHttpService
    ) {

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