export class MessageController {
    public static $inject: string[] = [
        '$routeParams'
    ];

    public message: string;

    constructor(
        params: ng.route.IRouteParamsService
    ) {
        this.message = params['message'];
    }
}