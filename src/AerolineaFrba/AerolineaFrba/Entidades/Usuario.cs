using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AerolineaFrba
{
    class Usuario
    {
        int id;
        string nombre;
        int   intentos;
        Rol  rol;

        public Usuario(int id, string nombre, int intentos)
        {
            // TODO: Complete member initialization
            this.id = id;
            this.nombre = nombre;
            this.intentos = intentos;
        }
    }
}
