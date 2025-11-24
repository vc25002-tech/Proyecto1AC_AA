using Proyecto1A.CapaDatos;
using Proyecto1A.CapaEdentidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1A.CapaNegocio
{
    public class ClienteBLL
    {
        ClienteDAL dal = new ClienteDAL();
        //

        public DataTable Listar()
        { return dal.Listar(); 
        
        
        
        } 
    

    public int Guardar(Clientes2 c)
        {
            if (string.IsNullOrWhiteSpace(c.Nombre))
                throw new Exception("El nombre del cliente es obligatorio.");

            if (c.Id == 0)
            {
                return dal.Insertar(c);
            }
            else
            {
                dal.Actualizar(c);
                return c.Id;
            }
        }

        public void Eliminar(int id)

        {

            dal.Eliminar(id);

        }

        public DataTable Buscar(string nombre)
        {
           return dal.Buscar(nombre);
        }

    }

}




