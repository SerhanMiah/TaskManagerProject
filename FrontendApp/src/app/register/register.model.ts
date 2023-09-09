export interface RegisterRequest {
    Email: string;
    Password: string;
    FirstName: string;
    LastName: string;
    Address: string;
    City: string;
    State: string;
    PostalCode: string;
    Country: string;
}

export interface RegisterResponse {
    message?: string;
}
