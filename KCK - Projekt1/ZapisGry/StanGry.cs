using EscapeRoom.ZapisGry;

public class StanGry {
    private long Czas;
    private int Poziom;

    public StanGry(long Czas, int Poziom) {
        this.Czas = Czas;
        this.Poziom = Poziom;
    }

    public void Zapisz(IKomenda komenda) {
        komenda.Wykonaj(this);
    }

    public void Wczytaj(IKomenda komenda) {
        komenda.Wykonaj(this);
    }

    public void Resetuj(IKomenda komenda) {
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

