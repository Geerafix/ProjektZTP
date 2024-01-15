using EscapeRoom.ZapisGry;

public class StanGry {
    private long Czas;
    private int Poziom;

    public StanGry() {
        this.Czas = 0;
        this.Poziom = 1;
    }

    public void ZapiszGre(IKomenda komenda) {
        komenda.Wykonaj(this);
    }

    public void WczytajGre(IKomenda komenda) {
        komenda.Wykonaj(this);
    }

    public void ResetujGre(IKomenda komenda) {
        komenda.Wykonaj(this);
    }

    public long GetCzas() {
        return this.Czas;
    }

    public int GetPoziom() {
        return this.Poziom;
    }

    public long SetCzas(long Czas) {
        return this.Czas = Czas;
    }

    public int SetPoziom(int Poziom) {
        return this.Poziom = Poziom;
    }
}

