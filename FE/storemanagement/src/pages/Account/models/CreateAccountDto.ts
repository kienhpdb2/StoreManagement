interface CreateAccountDto {
    fullName: string;
    phoneNumber: string;
    email: string;
    password: string;
    address: string;
    gender: number;
    dob: Date;
}

export default CreateAccountDto;