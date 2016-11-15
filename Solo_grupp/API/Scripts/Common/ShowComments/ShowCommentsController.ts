import {Comment} from '../Models/Comment'
import {User} from '../Models/User.ts'
import {AuthorizeService} from '../Menu/AuthorizeService.ts'

export class ShowCommentsController {
    public static $inject: string[] = [
        'authorizeService'
    ];

    public isAuth: boolean = false;
    public isRender: boolean = false;
    public Comments: Comment[] = [];
    public innerLevel: number = 0;

    constructor(
        authorizeService: AuthorizeService
    ) {
        authorizeService.Authentification()
            .success((data: User) => {
                if (data == undefined) {
                    this.isAuth = false;
                }
                else {
                    this.isAuth = true;
                }
            });
    }

    public send(id: string): void {
        
    }

}