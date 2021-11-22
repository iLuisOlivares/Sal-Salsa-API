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
    }
}
