import {ControllerResult} from '../../../Common/Models/ControllerResult.ts'
import {Replace} from '../../../Common/Models/Replace.ts'

export class ReplaceService {
    public static $inject: string[] =
    [
        '$http'
    ];

    constructor(
        private http: ng.IHttpService
    ) {

    }

    public Replace(email: string): ng.IHttpPromise<ControllerResult<any>> {
        return this.http.post('/api/user/replace?email=' + email, null);
    }
    public ReplacePassword(model: Replace): ng.IHttpPromise<ControllerResult<any>> {
        return this.http.post('/api/user/replace', model);
    }
}