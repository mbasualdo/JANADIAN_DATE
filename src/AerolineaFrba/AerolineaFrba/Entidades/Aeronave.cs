using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AerolineaFrba
{
    public class Aeronave
    {
        int id;
        String matricula;
        String modelo;
        Decimal KG_Disponibles;
        String fabricante;
        String tipoServicio;
        int cantButacasVentanilla;
        int cantButacasPasillo;

        Boolean habilitado;

        public Aeronave(int id, string matricula, string modelo, decimal kdDispo, string fabricante, int butacasVent, int butacasPas,string tipoServicio)
        {
            // TODO: Complete member initialization
            this.id = id;
            this.matricula=matricula;
            this.modelo = modelo;
            this.KG_Disponibles = kdDispo;
            this.fabricante = fabricante;
            this.cantButacasPasillo = butacasPas;
            this.cantButacasVentanilla = butacasVent;
            this.tipoServicio = tipoServicio;

        }

        public override string ToString()
        {
            return this.getMatricula.ToString() + " " + this.getModelo.ToString() + " " + this.getFabricante.ToString() + " " + this.getTipoServicio.ToString();
        }
        public Boolean getHabilitado
        {
            get { return habilitado; }
        }
        public int getId
        {
            get { return id; }
        }
        public string getMatricula
        {
            get { return matricula; }
        }
        public string getModelo
        {
            get { return modelo; }
        }
        public string getFabricante
        {
            get { return fabricante; }
        }
        public string getTipoServicio
        {
            get { return tipoServicio; }
        }
        public Decimal getKGDisponibles
        {
            get { return KG_Disponibles; }
        }
        public int getCantidadButacasVentanilla
        {
            get { return cantButacasVentanilla; }
        }
        public int getCantidadButacasPasillo
        {
            get { return cantButacasPasillo; }
        }
        internal void setMatricula(string p)
        {
            this.matricula = p;
        }
        internal void setButacasPasillo(int p)
        {
            this.cantButacasPasillo = p;
        }
        internal void setButacasVentanilla(int p)
        {
            this.cantButacasVentanilla = p;
        }
        
        internal void setModelo(string p)
        {
            this.modelo = p;
        }
        internal void setKG_Disponibles( Decimal p)
        {
            this.KG_Disponibles = p;
        }

        internal void setHabilitado(Boolean p)
        {
            this.habilitado = p;
        }
        internal void setTipoServicio(string p)
        {
            this.tipoServicio = p;
        }
        internal void setFabricante(string p)
        {
            this.fabricante = p;
        }
    }
}
