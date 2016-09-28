import {SignIn} from '../../../Common/Models/SignIn.ts'
import {MoveTo} from '../../../Common/Models/MoveTo.ts'
import {User} from '../../../Common/Models/User.ts'
import {RepositoryResult} from '../../../Common/Models/RepositoryResult.ts'
import {SignInService} from './SignInService.ts'
import {AuthorizeService} from '../../../Common/Menu/AuthorizeService.ts'
import {Validate} from '../Validate.ts'

export class SignInController extends Validate {

    public static $inject: string[] =
    [
        'signInService',
        '$routeParams',
        'authorizeService'
    ];

    constructor(
        private service: SignInService,
        params: ng.route.IRouteParamsService,
        authorizeService: AuthorizeService
    ) {
        super(
            new SignIn,
            null);
        authorizeService.Authentification()
            .success((data: User) => {
                if (data == undefined) {
                    return;
                }
                else {
                    window.location.href = '/#/Home';
                }
            })
            .error(() => {
                window.location.href = '/#/Home';
            });

        this.error = params['message'];

        if (this.error == undefined)
            this.error = '';
    }

    public submit(): void {
        let valid: boolean = this.emailValidate();
        valid = valid && this.passwordValidate();

        if (valid) {
            this.service.SignIn(this.model)
                .success((data: RepositoryResult<MoveTo>) => {
                    if (data.Responce.IsMoving) {
                        window.location.href = data.Responce.Location;
                        window.location.reload();
                    }
                });
        }
    }
}