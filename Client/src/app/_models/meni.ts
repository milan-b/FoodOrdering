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
        let hrana = new Hrana(hranaItem.hrana);
        hrana.izabrana = (this.narudzba.hrana == hranaItem.hrana.hranaId);
        this.hrana.push(hrana)
      });
    }
    else {
      return null;
    }
  }


}
