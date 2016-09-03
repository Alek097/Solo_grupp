import './Error/ErrorModule.ts'
import './SignUp/SignUpModule.ts'
import './Message/MessageModule.ts'

angular.module('pages',
    [
        'error',
        'signUp',
        'message'
    ]);