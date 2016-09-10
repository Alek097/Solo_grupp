import './SignIn/SignInModule.ts'
import './Replace/ReplaceModule.ts'
import './SignUp/SignUpModule.ts'

angular.module('authentification',
    [
        'signUp',
        'signIn',
        'replace'
    ]);