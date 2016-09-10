import './Error/ErrorModule.ts'
import './Message/MessageModule.ts'
import './Authentification/AuthentificationModule.ts'

angular.module('pages',
    [
        'authentification',
        'error',
        'message',
    ]);