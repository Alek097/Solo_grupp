import {Registration} from '../../Common/Models/Registration.ts'
import {SignUpService} from './SignUpService.ts'

export class SignUpController {

    public model: Registration = new Registration();
    private elem: JQuery;

    public static $inject: string[] =
    [
        'signUpService'
    ];

    constructor(
        private service: SignUpService
    ) {

    }

    public emailValidate(email: string): boolean {
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
            return true;
        }
    }

    public passwordValidate(password: string): boolean {
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

    public repeatedPasswordValidate(repeatedPassword: string): boolean {
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

    public lastNameValidate(lastName: string): boolean {
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
    public firstNameValidate(firstName: string): boolean {
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
    public patronymicValidate(patronymic: string): boolean {
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

    public adressValidate(adress: string): boolean {
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
            let regex: RegExp = new RegExp('с\.[A-Za-zА-Яа-яЁё]+ г\.[A-Za-zА-Яа-яЁё]');

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

    public phoneNumberValidate(phoneNumber: string): boolean {
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
        let valid: boolean = this.adressValidate(this.model.Adress);

        valid = valid && this.emailValidate(this.model.Email);

        valid = valid && this.firstNameValidate(this.model.FirstName);

        valid = valid && this.lastNameValidate(this.model.LastName);

        valid = valid && this.passwordValidate(this.model.Password);

        valid = valid && this.patronymicValidate(this.model.Patronymic);

        valid = valid && this.phoneNumberValidate(this.model.PhoneNumber);

        valid = valid && this.repeatedPasswordValidate(this.model.RepeatedPassword);

        if (valid) {
            this.service.Registration(this.model);
        }
    }

    private writeError(text: string): void {
        this.elem.text(text + '.');
    }
    private clearError(): void {
        this.elem.text('');
    }
}