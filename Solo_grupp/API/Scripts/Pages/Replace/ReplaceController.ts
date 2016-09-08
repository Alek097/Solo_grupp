import {ReplaceService} from './ReplaceService.ts'
import {AuthorizeService} from '../../Common/Authorize/AuthorizeService.ts'
import {User} from '../../Common/Models/User.ts'

export class ReplaceController {

    public error: string;

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
}