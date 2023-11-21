import kompanijaModel from "./kompanijaModel";

export default interface kontaktModel {
    id?: number;
    ime?: string;
    telefon1?: string;
    telefon2?: string;
    adresa?: string;
    email?: string;
    pravnoFizickoLice?: string;
    zanimanje?: string;
    pripadnostKompaniji?: kompanijaModel;
    kompanijaId?: number;
}