export class ReplaceService {
    public static $inject: string[] =
    [
        '$http'
    ];

    constructor(
        private http: ng.IHttpService
    ) {

    }

    public Replace(email: string): ng.IHttpPromise<any> {
        return this.http.post('/api/user/replace', email);
    }
}