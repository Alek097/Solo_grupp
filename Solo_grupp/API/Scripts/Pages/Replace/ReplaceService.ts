import {MoveTo} from '../../Common/Models/MoveTo.ts'
import {RepositoryResult} from '../../Common/Models/RepositoryResult.ts'

export class ReplaceService {
    public static $inject: string[] =
    [
        '$http'
    ];

    constructor(
        private http: ng.IHttpService
    ) {

    }

    public Replace(email: string): ng.IHttpPromise<RepositoryResult<MoveTo>> {
        return this.http.post('/api/user/replace?email=' + email, null);
    }
}