import {Registration} from '../../Common/Models/Registration.ts'

export class SignUpService {
    public static $inject: string[] =
    [
        '$http'
    ];

    constructor(
        private http: ng.IHttpService
    ) {

    }

    public Registration(model: Registration): ng.IHttpPromise<any> {
        return this.http.post('/api/user/signup', model);
    }
}