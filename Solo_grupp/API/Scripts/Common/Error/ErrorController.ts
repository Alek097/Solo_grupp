export class ErrorController {
    public static $inject: string[] = [
        '$routeParams'
    ];

    public httpCode: number;
    public message: string;

    constructor(
        params: ng.route.IRouteParamsService
    ) {
        this.httpCode = params['httpCode'];
        this.message = params['message'];
    }
}