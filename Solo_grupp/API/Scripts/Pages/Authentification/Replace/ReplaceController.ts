import {ReplaceService} from './ReplaceService.ts'
import {AuthorizeService} from '../../../Common/Menu/AuthorizeService.ts'
import {User} from '../../../Common/Models/User.ts'
import {ControllerResult} from '../../../Common/Models/ControllerResult.ts'
import {RepositoryResult} from '../../../Common/Models/RepositoryResult.ts'
import {Replace} from '../../../Common/Models/Replace.ts'
import {Validate} from '../Validate.ts'

export class ReplaceController extends Validate {

    public email: string;

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
        super(
            new Replace(),
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
                    if (data.Responce.IsSucces) {
                        window.location.href = data.Responce.Message;
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
                    if (data.Responce.IsSucces) {
                        window.location.href = data.Responce.Message;
                    }
                });
        }
    }
}