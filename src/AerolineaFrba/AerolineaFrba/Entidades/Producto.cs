using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AerolineaFrba
{
    public class Producto
    {
        int id;
        string nombre; 
        int stock;
        int millasNecesarias;

        public Producto(int id, string nombre, int stock, int millasNecesarias)
        {
            // TODO: Complete member initialization
            this.id = id;
            this.nombre=nombre;
            this.stock = stock;
            this.millasNecesarias = millasNecesarias;
        }
        public int getStock
        {
            get { return stock; }
        }
        internal void setStock(int p)
        {
            this.stock = p;
        }
        public int getId
        {
            get { return id; }
        }
        internal void setMillasNecesarias(int p)
        {
            this.millasNecesarias = p;
        }
        public int getMillasNecesarias
        {
            get { return millasNecesarias; }
        }
        public string getNombre
        {
            get { return nombre; }
        }
        public override string ToString()
        {
            return this.getNombre.ToString() + " Stock: " + this.getStock.ToString() + " Millas:" + this.getMillasNecesarias.ToString();
        }
    }
}
