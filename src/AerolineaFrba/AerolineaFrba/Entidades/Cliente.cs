using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AerolineaFrba
{
    public class Cliente
    {
        int id;
        decimal dni;
        string nombre; 
        string apellido;
        string dir;
        decimal telefono;
        string mail;
        DateTime fechaNacimiento;

        public Cliente(int id, decimal dni,string nombre, string apellido,string dir,decimal telefono,string mail,DateTime fechaNacimiento)
        {
            // TODO: Complete member initialization
            this.id = id;
            this.dni = dni;
            this.nombre=nombre;
            this.apellido = apellido;
            this.dir = dir;
            this.telefono = telefono;
            this.mail = mail;
            this.fechaNacimiento = fechaNacimiento;
        }
        public decimal getDni
        {
            get { return dni; }
        }
        internal void setDni(decimal p)
        {
            this.dni = p;
        }
        public int getId
        {
            get { return id; }
        }
    }
}
