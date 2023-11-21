import kontaktModel from "./kontaktModel";
import lokacijaModel from "./lokacijaModel";
import tipPostupkaModel from "./tipPostupkaModel";
import userModel from "./userModel";

export default interface parnicaModel {
    parnicaId?: number;
    datumOdrzavanja?: Date;
    lokacijaId?: number;
    lokacija?: lokacijaModel;
    sudijaId?: number;
    sudija?: kontaktModel;
    tipUstanove?: string;
    identifikatorPostupka?: string;
    brojSudnice?: number;
    tuzilacId?: number;
    tuzilac?: kontaktModel;
    tuzenikId?: number;
    tuzenik?: kontaktModel;
    zaduzeniAdvokati?: userModel[];
    napomena?: string;
    tipPostupkaId?: number;
    tipPostupka?: tipPostupkaModel;
}