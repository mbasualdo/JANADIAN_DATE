using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AerolineaFrba
{
    public class CompraDB
    {
        int pnr;
        Double precio;
        DateTime fecha;
        int viaje;
        String formaPago;
        int cliente;

        public CompraDB(int pnr, Double precio, DateTime fecha, int viaje, string formaPago, int cliente)
        {
            // TODO: Complete member initialization
            this.pnr = pnr;
            this.precio = precio;
            this.fecha = fecha;
            this.viaje = viaje;
            this.formaPago = formaPago;
            this.cliente = cliente;

        }

        public override string ToString()
        {
            return this.getPNR.ToString();
        }
        public Double getPrecio
        {
            get { return precio; }
        }
        public int getPNR
        {
            get { return pnr; }
        }

        internal void setPrecio(Double p)
        {
            this.precio = p;
        }

        internal void setFecha(DateTime p)
        {
            this.fecha = p;
        }
        public DateTime getFecha
        {
            get { return fecha; }
        }

        internal void setViaje(int p)
        {
            this.viaje = p;
        }
        public int getViaje
        {
            get { return viaje; }
        }

        internal void setFormaPago(String p)
        {
            this.formaPago = p;
        }
        public String getFormaPago
        {
            get { return formaPago; }
        }

        internal void setCliente(int p)
        {
            this.cliente = p;
        }
        public int getCliente
        {
            get { return cliente; }
        }

    }
}
