import {ReplaceService} from './ReplaceService.ts'
import {AuthorizeService} from '../../Common/Authorize/AuthorizeService.ts'
import {User} from '../../Common/Models/User.ts'
import {MoveTo} from '../../Common/Models/MoveTo.ts'
import {RepositoryResult} from '../../Common/Models/RepositoryResult.ts'

export class ReplaceController {

    public error: string;

    private email: string;
    private elem: JQuery;

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

    public submit(): void {
        let valid: boolean = this.emailValidate();

        if (valid) {
            this.service.Replace(this.email)
                .success((data) => {
                    if (data.Responce.IsMoving) {
                        window.location.href = data.Responce.Location;
                    }
                    else {
                        //NextSlide
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