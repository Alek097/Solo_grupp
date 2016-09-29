import {SignIn} from '../../../Common/Models/SignIn.ts'
import {ControllerResult} from '../../../Common/Models/ControllerResult.ts'
import {User} from '../../../Common/Models/User.ts'
import {RepositoryResultValue} from '../../../Common/Models/RepositoryResult.ts'

export class SignInService {
    public static $inject: string[] =
    [
        '$http'
    ];

    constructor(
        private http: ng.IHttpService
    ) {

    }

    public SignIn(model: SignIn): ng.IHttpPromise<RepositoryResultValue<User, ControllerResult>> {
        return this.http.post('/api/user/signin', model);
    }
}