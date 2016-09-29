import {Registration} from '../../../Common/Models/Registration.ts'
import {ControllerResult} from '../../../Common/Models/ControllerResult.ts'
import {SignUpService} from './SignUpService.ts'
import {AuthorizeService} from '../../../Common/Menu/AuthorizeService.ts'
import {User} from '../../../Common/Models/User.ts'
import {Validate} from '../Validate.ts'
import {ModalMessageService} from '../../../Common/ModalMessage/ModalMessageService.ts'

export class SignUpController extends Validate {

    public static $inject: string[] =
    [
        'signUpService',
        '$routeParams',
        'authorizeService',
        'modalMessageService'
    ];

    private countries: Object[];
    public countriesName: string[] = [];
    public selectCountry: string;

    public citiesName: string[] = [];
    public selectCity: string;

    constructor(
        private service: SignUpService,
        params: ng.route.IRouteParamsService,
        authorizeService: AuthorizeService,
        private modalMessageService: ModalMessageService
    ) {
        super(
            new Registration(),
            'solo_grupp_signUp');

        //this.scope.$on('$viewContentLoaded', () => {
        //    let model: Registration = this.getModel();

        //    if (model != undefined) {
        //        for (let propName in model) {
        //            if (model[propName] != undefined) {
        //                let selector: string = '#' + propName.charAt(0).toLowerCase() + propName.substr(1);
        //                angular.element(selector).val(model[propName]);
        //            }
        //            else {
        //                continue;
        //            }
        //        }
        //    }
        //});

        $.getJSON('Bundles/lib/country-city/data.json', (data: any) => {
            this.countries = data.countries;

            for (let name in this.countries) {
                this.countriesName[this.countriesName.length] = name;
            }
        })

        let model: Registration = this.getModel();

        if (model == undefined) {
            this.model = model;
        }

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
            this.saveModel();
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
            this.saveModel();
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
            this.saveModel();
            return true;
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
            this.saveModel();
            return true;
        }
    }

    public submit(): void {
        let valid: boolean = this.emailValidate();

        valid = this.firstNameValidate() && valid;

        valid = this.lastNameValidate() && valid;

        valid = this.passwordValidate() && valid;

        valid = this.patronymicValidate() && valid;

        valid = this.phoneNumberValidate() && valid;

        valid = this.repeatedPasswordValidate() && valid;

        valid = this.changeCity() && valid;

        valid = this.changeCountry() && valid;

        if (valid) {
            this.service.Registration(this.model)
                .success((data: ControllerResult) => {
                    if (data.IsSucces) {
                        location.href = '/#/Home';
                        this.modalMessageService.open(
                            data.Message,
                            'Регистрация!');
                    }
                    else {
                        this.modalMessageService.open(
                            data.Message,
                            'Упс!');
                    }
                });
        }
    }

    public changeCountry(): boolean {
        this.elem = angular.element('#error-country');


        if (this.selectCountry == undefined || this.selectCountry === '') {
            this.writeError('Выберите страну');
            return false;
        }
        else {
            this.clearError();
        }

        this.citiesName = this.countries[this.selectCountry];
        this.model.Country = this.selectCountry;
        this.saveModel();
        return true;

    }

    public changeCity(): boolean {
        this.elem = angular.element('#error-city');


        if (this.selectCity == undefined || this.selectCity === '') {
            this.writeError('Выберите город');
            return false;
        }
        else {
            this.clearError();
        }

        this.model.City = this.selectCity;
        this.saveModel();
        return true;
    }
}