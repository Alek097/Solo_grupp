import {Registration} from '../../../Common/Models/Registration.ts'
import {MoveTo} from '../../../Common/Models/MoveTo.ts'
export class SignUpService {
    public static $inject: string[] =
    [
        '$http'
    ];

    constructor(
        private http: ng.IHttpService
    ) {

    }

    public Registration(model: Registration): ng.IHttpPromise<MoveTo> {
        return this.http.post('/api/user/signup', model);
    }
}