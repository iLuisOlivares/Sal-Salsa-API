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

    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private IConfiguration _configuration; // get data => pass to .net data
        private IWebHostEnvironment _env; // which resource i do petition

        public ClienteController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                        SELECT * FROM cliente
            ";

            DataTable table = new DataTable();
            // Bring the pool connetion from appsetting.json
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpGet("{nombre_usuario}")]
        public JsonResult GetOne(String nombre_usuario)
        {
            string query = @"
                        SELECT * FROM cliente                        
                        WHERE nombre_usuario=@ClienteId;
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
                    myCommand.Parameters.AddWithValue("@ClienteId", nombre_usuario);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close(); 
                }
            }
            return new JsonResult(table);

        }


        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                        DELETE FROM cliente 
                        WHERE id=@ClienteId;
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
                    myCommand.Parameters.AddWithValue("@ClienteId", id);

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
