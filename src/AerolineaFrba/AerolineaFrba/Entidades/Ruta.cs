using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AerolineaFrba
{
    public class Ruta
    {
         int id;
        Decimal codigo;
        Double precio_BaseKG;
        Double precio_BasePasaje;
        String origen;
        String destino;
        String tipoServicio;

        Boolean habilitado;

        public Boolean getHabilitado
        {
            get { return habilitado; }
        }
        public int getId
        {
            get { return id; }
        }
        public string getOrigen
        {
            get { return origen; }
        }
        public string getTipoServicio
        {
            get { return tipoServicio; }
        }
        public string getDestino
        {
            get { return destino; }
        }
        public Decimal getCodigo
        {
            get { return codigo; }
        }
        public Double getPrecio_BaseKG
        {
            get { return precio_BaseKG; }
        }
        public Double getPrecio_BasePasaje
        {
            get { return precio_BasePasaje; }
        }
        public Ruta(int id, string origen, string destino, Decimal codigo, Double precio_BaseKG, Double precio_BasePasaje, string tipoServicio, bool habilitado)
        {
            // TODO: Complete member initialization
            this.id = id;
            this.origen = origen;
            this.destino = destino;
            this.codigo = codigo;
            this.precio_BaseKG = precio_BaseKG;
            this.precio_BasePasaje = precio_BasePasaje;
            this.tipoServicio = tipoServicio;
            this.habilitado = habilitado;
        }

        public override string ToString()
        {
            return this.getCodigo.ToString() + " " + this.getOrigen.ToString() + " " + this.getDestino.ToString() + " " + this.getTipoServicio.ToString();
        }
        internal void setOrigen(string p)
        {
            this.origen = p;
        }
        internal void setDestino(string p)
        {
            this.destino = p;
        }
        internal void setCodigo( Decimal p)
        {
            this.codigo = p;
        }
        internal void setPrecio_BaseKG(Double p)
        {
            this.precio_BaseKG = p;
        }
            internal void setPrecio_BasePasaje(Double p)
        {
            this.precio_BasePasaje = p;
        }
        internal void setHabilitado(Boolean p)
        {
            this.habilitado = p;
        }
        internal void setTipoServicio(string p)
        {
            this.tipoServicio = p;
        }
    }
}
