
export class User {
    id: string;
    userName: string;
    normalizedUserName: string;
    email: string;
    normalizedEmail: string;
    emailConfirmed: boolean;
    securityStamp: string;
    concurrencyStamp: string;
    phoneNumber: string | null;
    phoneNumberConfirmed: boolean;
    twoFactorEnabled: boolean;
    lockoutEnd: string | null;
    lockoutEnabled: boolean;
    accessFailedCount: number;

    constructor() {
        this.id = "";
        this.userName = "";
        this.normalizedUserName = "";
        this.email = "";
        this.normalizedEmail = "";
        this.emailConfirmed = false;
        this.securityStamp = "";
        this.concurrencyStamp = "";
        this.phoneNumber = null;
        this.phoneNumberConfirmed = false;
        this.twoFactorEnabled = false;
        this.lockoutEnd = null;
        this.lockoutEnabled = true;
        this.accessFailedCount = 0;
    }
}

export interface AuthState {
    user: User | null;
}
