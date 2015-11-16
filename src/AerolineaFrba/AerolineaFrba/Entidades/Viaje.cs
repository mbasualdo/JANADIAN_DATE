using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AerolineaFrba
{
    public class Viaje
    {
        int id;
        int ruta;
        int aeronave;
        DateTime fechaSalida;
        DateTime fechaLlegada;
        DateTime fechaLlegadaEstimada;

        public Viaje(int id, int aeronave, int ruta, DateTime fechaSalida, DateTime fechaLlegada, DateTime fechaLlegadaEstimada)
        {
            // TODO: Complete member initialization
            this.id = id;
            this.ruta=ruta;
            this.aeronave = aeronave;

            this.fechaSalida = fechaSalida;
            this.fechaLlegada = fechaLlegada;
            this.fechaLlegadaEstimada = fechaLlegadaEstimada;


        }


        public int getId
        {
            get { return id; }
        }
        public int getRuta
        {
            get { return ruta; }
        }
        public int getAeronave
        {
            get { return aeronave; }
        }
        public DateTime getFechaSalida
        {
            get { return fechaSalida; }
        }
        public DateTime getFechaLlegada
        {
            get { return fechaLlegada; }
        }
        public DateTime getFechaLlegadaEstimada
        {
            get { return fechaLlegadaEstimada; }
        }
        internal void setAeronave(int p)
        {
            this.aeronave = p;
        }
        internal void setRuta(int p)
        {
            this.ruta = p;
        }
        internal void setFechaSalida(DateTime p)
        {
            this.fechaSalida = p;
        }

        internal void setFechaLlegada(DateTime p)
        {
            this.fechaLlegada = p;
        }
        internal void setFechaLlegadaEstimada(DateTime p)
        {
            this.fechaLlegadaEstimada = p;
        }
    }
}
