﻿using EscapeRoom.ZapisGry;

public class StanGry {
    private long Czas;
    private int Poziom;

    public StanGry() {
        this.Czas = 0;
        this.Poziom = 1;
    }

    public void ZapiszGre(IPolecenie komenda) {
        komenda.Wykonaj(this);
    }

    public void WczytajGre(IPolecenie komenda) {
        komenda.Wykonaj(this);
    }

    public void ResetujGre(IPolecenie komenda) {
        Czas = 0;
        Poziom = 1;
        komenda.Wykonaj(this);
    }

    public long GetCzas() {
        return this.Czas;
    }

    public int GetPoziom() {
        return this.Poziom;
    }

    public void SetCzas(long Czas) {
        this.Czas = Czas;
    }

    public void SetPoziom(int Poziom) {
        this.Poziom = Poziom;
    }
}

