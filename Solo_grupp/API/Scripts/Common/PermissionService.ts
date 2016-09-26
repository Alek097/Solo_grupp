export class PermissionService {
    public static $inject: string[] = [
        '$http'
    ];

    constructor(
        private http: ng.IHttpService
    ) {

    }

    public IsResolution(resolution: string): ng.IHttpPromise<any> {
        return this.http.get('/api/Permission/is' + resolution);
    }
}