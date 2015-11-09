using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AerolineaFrba
{
    class Rol
    {
        int id;
        string nombre;
        List<String> funcionalidades;
        Boolean habilitado;

        public Rol(int id, string nombre,Boolean habilitado) {
            this.id = id;
            this.nombre = nombre;
            this.habilitado = habilitado;
        }

    }
}
