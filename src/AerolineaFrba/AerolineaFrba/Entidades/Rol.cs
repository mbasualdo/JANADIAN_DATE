using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AerolineaFrba
{
    public class Rol
    {
        int id;
        string nombre;
        List<String> funcionalidades;
        Boolean habilitado;

        public Boolean getHabilitado
        {
            get { return habilitado; }
        }
        public int getId
        {
            get { return id; }
        }
        public string getNombre
        {
            get { return nombre; }
        }
        public List<String> getFuncionalidades
        {
            get { return funcionalidades; }
        }

        public Rol(int id, string nombre,Boolean habilitado) {
            this.id = id;
            this.nombre = nombre;
            this.habilitado = habilitado;
        }

        public Rol(int id, string nombre, List<string> funcionalidades, bool habilitado)
        {
            // TODO: Complete member initialization
            this.id = id;
            this.nombre = nombre;
            this.funcionalidades = funcionalidades;
            this.habilitado = habilitado;
        }


        internal void setNombre(string p)
        {
            this.nombre = p;
        }
        internal void setFuncionalidades( List<string> p)
        {
            this.funcionalidades = p;
        }
        internal void setHabilitado(Boolean p)
        {
            this.habilitado = p;
        }
    }
}
