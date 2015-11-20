using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AerolineaFrba
{
    public class Butaca
    {
        int id;
        Decimal numero;
        string tipo;
        Decimal piso;
        int aeronave;

        public Decimal getNumero
        {
            get { return numero; }
        }
        public int getId
        {
            get { return id; }
        }
        public int getAeronave
        {
            get { return aeronave; }
        }
        public string getTipo
        {
            get { return tipo; }
        }
        public Decimal getPiso
        {
            get { return piso; }
        }

        public Butaca(int id, Decimal numero,string tipo,Decimal piso, int aeronave)
        {
            this.id = id;
            this.numero = numero;
            this.tipo = tipo;
            this.piso = piso;
            this.aeronave = aeronave;
        }



        internal void setNumero(Decimal p)
        {
            this.numero = p;
        }
        internal void setTipo(string p)
        {
            this.tipo = p;
        }
        internal void setPiso(Decimal p)
        {
            this.piso = p;
        }
        internal void setAeronave(int p)
        {
            this.aeronave = p;
        }
    }
}
