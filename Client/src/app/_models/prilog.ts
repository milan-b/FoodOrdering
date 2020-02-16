export class Prilog {
  prilogId: number;
  naziv: string;
  varijanta: number;

  public constructor(init?: Partial<Prilog>) {
    Object.assign(this, init);
  }
}
