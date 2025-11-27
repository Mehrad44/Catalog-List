export class UserToken {
    constructor(
        public tokenType: string,
        public accessToken: string,
        public refreshToken: string,
        public expiresIn: number) { }
}
