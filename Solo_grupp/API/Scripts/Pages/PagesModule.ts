import './Error/ErrorModule.ts'
import './SignUp/SignUpModule.ts'
import './Message/MessageModule.ts'
import './SignIn/SignInModule.ts'
import './Replace/ReplaceModule.ts'

angular.module('pages',
    [
        'error',
        'signUp',
        'message',
        'signIn',
        'replace'
    ]);