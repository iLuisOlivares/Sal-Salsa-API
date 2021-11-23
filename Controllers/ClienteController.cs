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
using Restaurante_sal_salsa.Models;

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

        [HttpPost]
        public JsonResult Post(Models.Cliente clienteData)
        {
            string query = @"
                        INSERT INTO cliente (nombre_usuario, contrasena, nombre_completo, correo)
                        VALUES (@Enombre_usuario, @Econtrasena, @Enombre_completo, @Ecorreo) ;         
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@Enombre_usuario", clienteData.nombre_usuario);
                    myCommand.Parameters.AddWithValue("@Econtrasena", clienteData.contrasena);
                    myCommand.Parameters.AddWithValue("@Enombre_completo", clienteData.nombre_completo);
                    myCommand.Parameters.AddWithValue("@Ecorreo", clienteData.correo);


                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Added Successfully");
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

        [HttpPut]
        public JsonResult Put(Models.Cliente clienteData)
        {
            string query = @"
                        UPDATE cliente SET 
                        nombre_usuario =@ClienteNombreUsuario,
                        contrasena =@ClienteContrasena,
                        nombre_completo =@ClienteNombre_completo,
                        correo= @ClienteCorreo,
                        tipo_usuario =@ClienteTipo
                        WHERE id =@ClienteId;
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@ClienteId", clienteData.id);
                    myCommand.Parameters.AddWithValue("@ClienteNombreUsuario", clienteData.nombre_usuario);
                    myCommand.Parameters.AddWithValue("@ClienteContrasena", clienteData.contrasena);
                    myCommand.Parameters.AddWithValue("@ClienteNombre_completo", clienteData.nombre_completo);
                    myCommand.Parameters.AddWithValue("@ClienteCorreo", clienteData.correo);
                    myCommand.Parameters.AddWithValue("@ClienteTipo", clienteData.tipo_usuario);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

    }
}
