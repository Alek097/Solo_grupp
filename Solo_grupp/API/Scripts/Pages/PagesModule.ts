import './Error/ErrorModule.ts'
import './SignUp/SignUpModule.ts'
import './Message/MessageModule.ts'
import './SignIn/SignInModule.ts'

angular.module('pages',
    [
        'error',
        'signUp',
        'message',
        'signIn'
    ]);