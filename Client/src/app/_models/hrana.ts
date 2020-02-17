import { Prilog } from './prilog';

export class Hrana {
  hranaId: number;
  naziv: string;
  izabrana: boolean;
  stalna: boolean;
  narucena: boolean;
  prilozi: Prilog[];

  public constructor(init?: Partial<Hrana>) {
    Object.assign(this, init);
  }
}
