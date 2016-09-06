import {User} from '../Models/User.ts'
import {Authentification} from '../Models/Authentification.ts'

export class AuthorizeController {
    public isAuth: boolean;
    public fullName: string = null;
    constructor() {
        this.isAuth = Authentification.isAuthentification;
        this.fullName = Authentification.user.FullName;
    }
}