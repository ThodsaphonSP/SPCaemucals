
export class User {
    id: string = '';
    userName: string = '';
    normalizedUserName: string = '';
    email: string = '';
    normalizedEmail: string = '';
    emailConfirmed: boolean = false;
    securityStamp: string = '';
    concurrencyStamp: string = '';
    phoneNumber: string = '';
    phoneNumberConfirmed: boolean = false;
    twoFactorEnabled: boolean = false;
    lockoutEnd: string = '';
    lockoutEnabled: boolean = false;
    accessFailedCount: number = 0;
    firstName: string = '';
    lastName: string = '';
    companyId: string = '';
    company: Company = new Company();
}

export class Company {
    companyId: number = 0;
    companyName: string = '';
}


export interface AuthState {
    user: User | null;
}
