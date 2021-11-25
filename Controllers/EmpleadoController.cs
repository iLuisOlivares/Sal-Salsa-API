using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Restaurante_sal_salsa.Controllers
{
    // api/empleado endpoint
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        // Serialize
        private IConfiguration _configuration; // get data => pass to .net data
        private IWebHostEnvironment _env; // which resource i do petition

        public EmpleadoController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        // Get Empleados
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                        SELECT * FROM empleado
            ";

            DataTable table = new DataTable();
            // Bring the pool connetion from appsetting.json
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open(); // open connection
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close(); // close connection
                }
            }

            return new JsonResult(table); //return the json
        }

        [HttpPost]
        public JsonResult Post(Models.Empleado empleadoData)
        {
            string query = @"
                        INSERT INTO empleado (restaurante_id, nombre, descripcion, imagen)
                        VALUES (@Erestaurante_id, @Enombre, @Edescripcion, @Eimagen);         
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@Erestaurante_id", empleadoData.restaurante_id);
                    myCommand.Parameters.AddWithValue("@Enombre", empleadoData.nombre);
                    myCommand.Parameters.AddWithValue("@Edescripcion", empleadoData.descripcion);
                    myCommand.Parameters.AddWithValue("@Eimagen", empleadoData.imagen);
            

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        // Delete Empleado
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                        DELETE FROM empleado 
                        WHERE id=@EmpleadoId;
            ";

            DataTable table = new DataTable();
            // Bring the pool connetion from appsetting.json
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open(); // open connection
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    // change @EmpleadoId in the query by id from the body.params
                    myCommand.Parameters.AddWithValue("@EmpleadoId", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close(); // close connection
                }
            }

            return new JsonResult("Deleted Successfully"); //return the json
        }


    }
}
