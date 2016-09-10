import {Registration} from '../../../Common/Models/Registration.ts'
import {MoveTo} from '../../../Common/Models/MoveTo.ts'
import {SignUpService} from './SignUpService.ts'
import {AuthorizeService} from '../../../Common/Menu/AuthorizeService.ts'
import {User} from '../../../Common/Models/User.ts'
import {Validate} from '../Validate.ts'

export class SignUpController extends Validate {

    public static $inject: string[] =
    [
        'signUpService',
        '$routeParams',
        'authorizeService'
    ];

    constructor(
        private service: SignUpService,
        params: ng.route.IRouteParamsService,
        authorizeService: AuthorizeService
    ) {
        super(new Registration());
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

    public lastNameValidate(): boolean {
        let lastName: string = angular.element('#lastName').val();
        this.model.LastName = lastName;

        this.elem = angular.element('#error-lastName');

        if (lastName == undefined || lastName.length === 0) {
            this.writeError('Введите Фамилию');
            return false;
        }
        else {
            this.clearError();
            return true;
        }
    }
    public firstNameValidate(): boolean {
        let firstName: string = angular.element('#firstName').val();

        this.model.FirstName = firstName;

        this.elem = angular.element('#error-firstName');

        if (firstName == undefined || firstName.length === 0) {
            this.writeError('Введите Имя');
            return false;
        }
        else {
            this.clearError();
            return true;
        }
    }
    public patronymicValidate(): boolean {
        let patronymic: string = angular.element('#patronymic').val();

        this.model.Patronymic = patronymic;

        this.elem = angular.element('#error-patronymic');

        if (patronymic == undefined || patronymic.length === 0) {
            this.writeError('Введите Отчество');
            return false;
        }
        else {
            this.clearError();
            return true;
        }
    }

    public adressValidate(): boolean {
        let adress: string = angular.element('#adress').val();

        if (adress === undefined)
            return;

        this.elem = angular.element('#error-adress');


        if (adress == undefined) {

            this.writeError('Укажите адрес');

            return false;
        }

        this.model.Adress = adress;

        if (adress.length === 0) {
            this.writeError('Укажите адрес');
            return false;
        }
        else {
            let regex: RegExp = new RegExp('с\.[A-Za-zА-Яа-яЁё]+ г\.[A-Za-zА-Яа-яЁё]+');

            if (regex.test(adress)) {
                this.clearError();
                return true;
            }
            else {
                this.writeError('Введите адрес в указанном формате');
                return false;
            }
        }

    }

    public phoneNumberValidate(): boolean {
        let phoneNumber: string = angular.element('#phoneNumber').val();

        if (phoneNumber === undefined)
            return;

        this.elem = angular.element('#error-phoneNumber');

        if (phoneNumber == undefined) {

            this.writeError('Введите номер телефона');

            return false;
        }

        this.model.PhoneNumber = phoneNumber;

        if (phoneNumber.length == 0) {
            this.writeError('Введите адрес');
            return false;
        }
        else {
            this.clearError();
            return true;
        }
    }

    public submit(): void {
        let valid: boolean = this.adressValidate();

        valid = valid && this.emailValidate();

        valid = valid && this.firstNameValidate();

        valid = valid && this.lastNameValidate();

        valid = valid && this.passwordValidate();

        valid = valid && this.patronymicValidate();

        valid = valid && this.phoneNumberValidate();

        valid = valid && this.repeatedPasswordValidate();

        if (valid) {
            this.service.Registration(this.model)
                .success((data: MoveTo) => {
                    if (data.IsMoving) {
                        window.location.href = data.Location;
                    }
                });
        }
    }
}