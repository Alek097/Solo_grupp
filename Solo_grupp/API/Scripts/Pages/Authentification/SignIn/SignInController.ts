import {SignIn} from '../../../Common/Models/SignIn.ts'
import {ControllerResult} from '../../../Common/Models/ControllerResult.ts'
import {User} from '../../../Common/Models/User.ts'
import {SignInService} from './SignInService.ts'
import {AuthorizeService} from '../../../Common/Menu/AuthorizeService.ts'
import {Validate} from '../Validate.ts'
import {ModalMessageService} from '../../../Common/ModalMessage/ModalMessageService.ts'

export class SignInController extends Validate {

    public static $inject: string[] =
    [
        'signInService',
        '$routeParams',
        'authorizeService',
        'modalMessageService'
    ];

    constructor(
        private service: SignInService,
        params: ng.route.IRouteParamsService,
        authorizeService: AuthorizeService,
        private modalMessageService: ModalMessageService
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
                .success((data: ControllerResult<any>) => {
                    if (data.IsSucces) {
                        window.location.href = data.Message;
                        window.location.reload();
                    }
                    else {
                        this.modalMessageService.open(
                            data.Message,
                            'Упс!');
                    }
                });
        }
    }
}