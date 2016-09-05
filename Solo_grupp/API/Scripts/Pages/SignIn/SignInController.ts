import {SignIn} from '../../Common/Models/SignIn.ts'
import {MoveTo} from '../../Common/Models/MoveTo.ts'
import {User} from '../../Common/Models/User.ts'
import {RepositoryResultValue} from '../../Common/Models/RepositoryResult.ts'
import {SignInService} from './SignInService.ts'

export class SignInController {
    public model: SignIn = new SignIn();
    public error: string;

    private elem: JQuery;

    public static $inject: string[] =
    [
        'signInService',
        '$routeParams'
    ];

    constructor(
        private service: SignInService,
        params: ng.route.IRouteParamsService
    ) {
        this.error = params['message'];

        if (this.error == undefined)
            this.error = '';
    }

    public emailValidate(): boolean {
        let email: string = angular.element('#email').val();

        if (email === undefined)
            return;

        this.elem = angular.element('#error-email');

        if (email == undefined) {
            this.writeError('Введите Email');
            return false;
        }

        this.model.Email = email;

        if (email.length === 0) {
            this.writeError('Введите Email');
            return false;
        }
        else {
            this.clearError();
        }

        let Regex: RegExp = new RegExp('^[-._a-z0-9]+@(?:[a-z0-9][-a-z0-9]+\.)+[a-z]{2,6}$');

        if (Regex.test(email)) {
            this.clearError();
            return true;
        }
        else {
            this.writeError('Введите Email в правильном формате');
            return false;
        }
    }

    public passwordValidate(): boolean {
        let password: string = angular.element('#password').val();

        this.model.Password = password;

        this.elem = angular.element('#error-password');

        if (password == undefined || password.length == 0) {
            this.writeError('Введите пароль')
            return false;
        }
        else {
            this.clearError();
        }

        if (password.length < 5) {
            this.writeError('Пароль меньше 5 символов');
            return false;
        }
        else {
            this.clearError();
            return true;
        }
    }

    public submit(): void {
        let valid: boolean = this.emailValidate();
        valid = valid && this.passwordValidate();

        if (valid) {
            this.service.SignIn(this.model)
                .success((data: RepositoryResultValue<User, MoveTo>) => {
                    if (data.Responce.IsMoving) {

                        if (data.Value != undefined) {
                            window.localStorage.setItem('user', JSON.stringify(data.Value));
                        }


                        window.location.href = data.Responce.Location;
                    }
                });
        }
    }

    private writeError(text: string): void {
        this.elem.text(text + '.');
    }
    private clearError(): void {
        this.elem.text('');
    }
}