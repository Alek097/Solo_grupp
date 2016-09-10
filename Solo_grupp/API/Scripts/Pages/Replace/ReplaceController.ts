import {ReplaceService} from './ReplaceService.ts'
import {AuthorizeService} from '../../Common/Authorize/AuthorizeService.ts'
import {User} from '../../Common/Models/User.ts'
import {MoveTo} from '../../Common/Models/MoveTo.ts'
import {RepositoryResult} from '../../Common/Models/RepositoryResult.ts'
import {Replace} from '../../Common/Models/Replace.ts'

export class ReplaceController {

    public error: string;

    private email: string;
    private elem: JQuery;
    private model: Replace = new Replace();

    public static $inject: string[] =
    [
        'replaceService',
        'authorizeService',
        '$routeParams'
    ];

    constructor(
        private service: ReplaceService,
        authorizeService: AuthorizeService,
        params: ng.route.IRouteParamsService
    ) {
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

    public emailValidate(): boolean {
        let email: string = angular.element('#email').val();

        if (email === undefined)
            return;

        this.elem = angular.element('#error-email');

        if (email == undefined) {
            this.writeError('Введите Email');
            return false;
        }

        this.email = email;

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

    public repeatedPasswordValidate(): boolean {
        let repeatedPassword: string = angular.element('#repeatedPassword').val();;

        this.model.RepeatedPassword = repeatedPassword;

        this.elem = angular.element('#error-repeatedPassword');

        if (repeatedPassword == undefined) {
            this.writeError('Повторите пароль');
        }

        if (repeatedPassword !== this.model.Password) {
            this.writeError('Пароли не совпадают');
            return false;
        }
        else {
            this.clearError();
            return true;
        }
    }

    public replaceCodeValidate(): boolean {
        let replaceCode: string = angular.element('#replaceCode').val();

        this.model.ReplaceCode = replaceCode;

        this.elem = angular.element('#error-replaceCode');

        if (replaceCode == undefined || replaceCode.length == 0) {
            this.writeError('Введите код');
            return false;
        }
        else {
            this.clearError();
        }

        let regex: RegExp = new RegExp('[a-z0-9]{8}-[a-z0-9]{4}-[a-z0-9]{4}-[a-z0-9]{4}-[a-z0-9]{12}');

        if (regex.test(replaceCode)) {
            this.clearError();
            return true;
        }
        else {
            this.writeError('Неправильный код');
            return false;
        }
    }

    public submit(): void {
        let valid: boolean = this.emailValidate();

        if (valid) {
            this.service.Replace(this.email)
                .success((data) => {
                    if (data.Responce.IsMoving) {
                        window.location.href = data.Responce.Location;
                    }
                    else {
                        angular.element('#display-firstForm').css('display', 'none');
                        angular.element('#display-secondForm').css('display', 'block');
                    }
                });
        }
    }

    public submitPass(): void {
        let valid: boolean = this.replaceCodeValidate();
        valid = valid && this.passwordValidate();
        valid = valid && this.repeatedPasswordValidate();

        if (valid) {
            this.service.ReplacePassword(this.model)
                .success((data) => {
                    if (data.Responce.IsMoving) {
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