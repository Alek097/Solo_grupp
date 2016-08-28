import {User} from '../Models/User.ts'
import {Authentification} from '../Models/Authentification.ts'

export class AuthorizeController {
    public isAuth: boolean;
    public fullName: string = null;
    constructor()
    {
        let user: User = window.localStorage.getItem('user');

        if (user === null || user === undefined) {
            this.isAuth = false;
            Authentification.isAuthentification = false;
        }
        else {
            this.isAuth = true;
            Authentification.isAuthentification = true;
            Authentification.user = user;
            this.fullName = user.FullName;
        }
    }
}