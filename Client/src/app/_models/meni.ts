import { Hrana } from './hrana';
import { Prilog } from './prilog';

export class Meni {
  narudzba: {
    lokacija: number;
    vrijeme: number;
    hrana: number;
    prilozi: number[];
    datum: Date;
  };

  hrana: Hrana[];

  constructor(meniResponse: any) {
    //TODO remove init narudzba when API return narudzba
    this.narudzba = { lokacija: 1, vrijeme: 1, hrana: 1, prilozi: [], datum: new Date() };
    this.hrana = [];
    if (meniResponse && meniResponse.body) {
      meniResponse.body.hrana.forEach((hranaItem) => {
        this.hrana.push(new Hrana({
          hranaId: hranaItem.hrana.hranaId,
          naziv: hranaItem.hrana.naziv,
          stalna: hranaItem.hrana.stalna,
          narucena: false,
          izabrana: this.narudzba.hrana == hranaItem.hrana.hranaId,
          prilozi: this.getPrilozi(hranaItem.hrana.prilozi)
        })
        );

      });
    }
    else {
      return null;
    }
  }

  private getPrilozi(priloziItem: any): Array<Prilog> {
    let prilozi: Prilog[] = [];
    priloziItem.forEach((prilogItem) => {
      prilozi.push(
        new Prilog({
          prilogId: prilogItem.prilog.prilogId,
          naziv: prilogItem.prilog.naziv,
          varijanta: prilogItem.varijanta,
          izabran: false,
          omogucen: true
        })
      );
    });
    return prilozi;
  }
}
