﻿export class Validate {
    public error: string;

    public elem: JQuery;

    constructor(
        public model: any
    ) {
    }

    public writeError(text: string): void {
        this.elem.text(text + '.');
    }
    public clearError(): void {
        this.elem.text('');
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
}