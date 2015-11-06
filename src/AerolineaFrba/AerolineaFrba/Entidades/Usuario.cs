using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AerolineaFrba
{
    public class Usuario
    {
        int id;
        string nombre;
        int   intentos;
        string  nombreRol;

        public Usuario(int id, string nombre, int intentos,string rol)
        {
            // TODO: Complete member initialization
            this.id = id;
            this.nombre = nombre;
            this.intentos = intentos;
            this.nombreRol = rol;
        }
    }
}
