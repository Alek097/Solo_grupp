import {Registration} from '../../../Common/Models/Registration.ts'
import {ControllerResult} from '../../../Common/Models/ControllerResult.ts'
export class SignUpService {
    public static $inject: string[] =
    [
        '$http'
    ];

    constructor(
        private http: ng.IHttpService
    ) {

    }

    public Registration(model: Registration): ng.IHttpPromise<ControllerResult<any>> {
        return this.http.post('/api/user/signup', model);
    }
}