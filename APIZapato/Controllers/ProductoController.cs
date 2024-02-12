using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIZapato.Models;
using System;
using Microsoft.AspNetCore.Cors;

namespace APIZapato.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {

        public readonly bdSuperZapatoContext _dbcontext;
        public ProductoController(bdSuperZapatoContext _context)
        {
            _dbcontext = _context;

        }

        /// <summary>
        /// Metodo Get  Devuelve un lista de los productos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista() {
            List<Producto> Lista = new List<Producto>();
            try
            {
                Lista = _dbcontext.Productos.Include(c => c.oCategoria).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = Lista });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = Lista });
            }

        }



        [HttpGet]
        [Route("ObtenerProdcuto/{idProducto:int}")]
        public IActionResult ObtenerProdcuto(int idProducto)
        {
            //Buscamos el producto por el Id que se envia
            Producto oProducto = _dbcontext.Productos.Find(idProducto);

            //Si no encontramos el producto Retornamos Un mensaje 

            if (oProducto == null)
            {
                return BadRequest("El producto no se encuentra");
            }

            //Si se encuentra un valor retornamos el objeto 
            try
            {

                oProducto = _dbcontext.Productos.Include(c => c.oCategoria).Where(p => p.Idproducto == idProducto).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oProducto });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = oProducto });
            }

        }

        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Producto objeto)
        {
            try
            {

                _dbcontext.Productos.Add(objeto);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }


        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Producto objeto)
        {


            //Buscamos el producto por el Id que se envia
            Producto oProducto = _dbcontext.Productos.Find(objeto.Idproducto);

            //Si no encontramos el producto Retornamos Un mensaje 

            if (oProducto == null)
            {
                return BadRequest("El producto no se encuentra");
            }

            try
            {

                oProducto.CodifoBarra = objeto.CodifoBarra is null ? oProducto.CodifoBarra : objeto.CodifoBarra;
                oProducto.Descripcion = objeto.Descripcion is null ? oProducto.Descripcion : objeto.Descripcion;
                oProducto.Marca = objeto.Marca is null ? oProducto.Marca : objeto.Marca;
                oProducto.IdCategoria = objeto.IdCategoria is null ? oProducto.IdCategoria : objeto.IdCategoria;
                oProducto.Precio= objeto.Precio is null ? oProducto.Precio : objeto.Precio;


                _dbcontext.Productos.Update(oProducto);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }


        [HttpDelete]
        [Route("Eliminar/{idProducto:int}")]
        public IActionResult Eliminar(int idProducto)
        {

            //Buscamos el producto por el Id que se envia
            Producto oProducto = _dbcontext.Productos.Find(idProducto);

            //Si no encontramos el producto Retornamos Un mensaje 

            if (oProducto == null)
            {
                return BadRequest("El producto no se encuentra");
            }

            try
            {         

                _dbcontext.Productos.Remove(oProducto);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }

        }


    }
}
