import {Registration} from '../../Common/Models/Registration.ts'

export class SignUpController {

    public model: Registration;
    public isValid: boolean = false;

    public static $inject: string[] =
    [

    ];

    public onInput(a: any): void {
        console.log(a);
    }
}