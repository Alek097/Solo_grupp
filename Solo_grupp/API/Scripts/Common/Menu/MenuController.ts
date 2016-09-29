import {User} from '../Models/User.ts'
import {AuthorizeService} from './AuthorizeService.ts'

export class MenuController {
    public static $inject: string[] =
    [
        'authorizeService'
    ];

    public isAuth: boolean;
    public fullName: string = null;
    constructor(
        private service: AuthorizeService
    ) {
        this.service.Authentification()
            .success((data: User) => {
                if (data == null) {
                    this.isAuth = false;
                }
                else {
                    this.fullName = data.FullName;
                    this.isAuth = true;
                }
            })
            .error(() => {
                this.isAuth = false;
            });
    }
    public SignOut(): void {
        this.service.SignOut()
            .success(() => {
                this.isAuth = false;
                this.fullName = '';
                location.reload();
            });
    }
}