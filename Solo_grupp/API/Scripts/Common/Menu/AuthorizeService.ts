import {User} from '../Models/User.ts'

export class AuthorizeService {
    public static $inject: string[] =
    [
        '$http'
    ];
    constructor(
        private http: ng.IHttpService
    ) {

    }

    public SignOut(): ng.IHttpPromise<any> {
        return this.http.get('/api/user/signout');
    }
    public Authentification(): ng.IHttpPromise<User> {
        return this.http.get('/api/user/Authentification');
    }
}