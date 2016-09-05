import {SignIn} from '../../Common/Models/SignIn.ts'
import {MoveTo} from '../../Common/Models/MoveTo.ts'
import {User} from '../../Common/Models/User.ts'
import {RepositoryResultValue} from '../../Common/Models/RepositoryResult.ts'

export class SignInService {
    public static $inject: string[] =
    [
        '$http'
    ];

    constructor(
        private http: ng.IHttpService
    ) {

    }

    public SignIn(model: SignIn): ng.IHttpPromise<RepositoryResultValue<User,MoveTo>> {
        return this.http.post('/api/user/signin', model);
    }
}