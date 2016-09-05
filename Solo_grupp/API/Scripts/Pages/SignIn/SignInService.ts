import {SignIn} from '../../Common/Models/SignIn.ts'
import {MoveTo} from '../../Common/Models/MoveTo.ts'

export class SignInService {
    public static $inject: string[] =
    [
        '$http'
    ];

    constructor(
        private http: ng.IHttpService
    ) {

    }

    public SignIn(model: SignIn): ng.IHttpPromise<MoveTo> {
        return this.http.post('/api/user/signin', model);
    }
}