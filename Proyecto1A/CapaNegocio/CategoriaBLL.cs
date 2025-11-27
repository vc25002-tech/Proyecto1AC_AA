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
    public class CategoriaBLL
    {

        CategoriaDAL dal = new CategoriaDAL();

        public DataTable Listar()
        {
            return dal.Listar();
            // La BLL solo pasa la solicitud
        }
        private void ValidarCategoria(Categoria c, bool esEdicion = false)
        {
            // 1. Campo obligatorio
            if (string.IsNullOrWhiteSpace(c.Nombre))
                throw new Exception("El nombre de la categoría es obligatorio.");

            // 2. Longitud
            if (c.Nombre.Length > 50)
                throw new Exception("El nombre no debe superar los 50 caracteres.");

            if (!string.IsNullOrWhiteSpace(c.Descripcion) && c.Descripcion.Length > 250)
                throw new Exception("La descripción no debe superar los 250 caracteres.");

            // 3. Validación de duplicados
            if (!esEdicion) // Caso: INSERTAR
            {

                if (dal.ExisteNombre(c.Nombre))
                    throw new Exception("Ya existe una categoría con ese nombre.");
            }
            else // Caso: EDITAR
            {
                if (dal.ExisteNombreEnOtraCategoria(c.Nombre, c.Id))
                    throw new Exception("Ya existe otra categoría con ese nombre.");
            }
        }

        // GUARDAR (Inserta o Actualiza depende si lleva ID o no)

        public int Guardar(Categoria c)
        {
            // Si el ID es 0 → INSERTAR
            if (c.Id == 0)
            {
                ValidarCategoria(c, esEdicion: false);
                return dal.Insertar(c);
            }
            else
            {
                ValidarCategoria(c, esEdicion: true);

                bool actualizado = dal.Actualizar(c);

                if (!actualizado)
                    throw new Exception("No se pudo actualizar la categoría.");

                return c.Id; // Devuelve el mismo ID
            }

        }

        public bool Eliminar(int id)
        {
            // Validación de FK ANTES de intentar eliminar
            if (dal.TieneProductosAsociados(id))
                throw new Exception("No se puede eliminar esta categoría porque tiene productos asociados.");

            // Si no tiene dependencias, eliminar
            return dal.Eliminar(id);

        }

        // BUSCAR

        public DataTable Buscar(string filtro)
        {
            return dal.Buscar(filtro);
        }
    }


}

