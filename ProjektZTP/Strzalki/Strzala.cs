using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRoom {
    internal class Strzala {
        public int X { get; set; }
        public int Y { get; set; }

        public int Xpostaci { get; set; }
        public int Ypostaci { get; set; }

        public Strzala() {

        }

        public int GetStrzalaX() {
            return X;
        }

        public int GetStrzalaY() {
            return Y;
        }

        public void SetStrzalaX(int x) {
            X = x;
        }
        public void SetStrzalaY(int y) {
            Y = y;
        }

        public void SetXpostaci(int x) {
            Xpostaci = x;
        }

        public void SetYpostaci(int y) {
            Ypostaci = y;
        }

        public int GetXpostaci() {
            return Xpostaci;
        }

        public int GetYpostaci() {
            return Ypostaci;
        }

    }
}
