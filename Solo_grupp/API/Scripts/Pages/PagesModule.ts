import './Error/ErrorModule.ts'
import './Message/MessageModule.ts'
import './Authentification/AuthentificationModule.ts'
import './News/NewsModule.ts'

angular.module('pages',
    [
        'authentification',
        'error',
        'message',
        'mainNews'
    ]);