export class KeyValuePair{
    id: number;
    name: string;
}

export class Contact{
    name: string;
    phone: string;
    email: string;
}

export class Vehicle {
    id: number;
    model: KeyValuePair;
    make: KeyValuePair;
    isRegistered: boolean;
    contact: Contact;
    features: KeyValuePair[];
    lastUpdate: string;
}

export class SaveVehicle{
    id: number;
    modelId: number;
    makeId: number;
    isRegistered: boolean;
    contact: Contact;
    features: number[];
}