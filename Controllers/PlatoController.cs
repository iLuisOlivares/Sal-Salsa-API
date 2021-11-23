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
    public class PlatoController : ControllerBase
    {
        private IConfiguration _configuration;
        private IWebHostEnvironment _env;

        public PlatoController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                        SELECT * FROM plato
            ";

            DataTable table = new DataTable();
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

        [HttpGet("{id}")]
        public JsonResult GetOne(int id)
        {
            string query = @"
                        SELECT * FROM plato                        
                        WHERE id=@PlatoId;
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@PlatoId", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult(table);

        }

        [HttpPost]
        public JsonResult Post(Models.Plato platoData)
        {
            string query = @"
                        INSERT INTO plato (restaurante_id, nombre, descripcion, imagen, precio)
                        VALUES (@Erestaurante_id, @Enombre, @Edescripcion, @Eimagen, @Eprecio) ;         
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@Erestaurante_id", platoData.restaurante_id);
                    myCommand.Parameters.AddWithValue("@Enombre", platoData.nombre);
                    myCommand.Parameters.AddWithValue("@Edescripcion", platoData.descripcion);
                    myCommand.Parameters.AddWithValue("@Eimagen", platoData.imagen);
                    myCommand.Parameters.AddWithValue("@Eprecio", platoData.precio);

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
                        DELETE FROM plato 
                        WHERE id=@PlatoId;
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@PlatoId", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close(); 
                }
            }

            return new JsonResult("Deleted Successfully");
        }

        [HttpPut]
        public JsonResult Put(Models.Plato platoData)
        {
            string query = @"
                        UPDATE plato SET 
                        nombre =@Enombre,
                        descripcion =@Edescripcion,
                        imagen =@Eimagen,
                        precio= @Eprecio
                        WHERE id =@PlatoId;   
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@PlatoId", platoData.id);
                    myCommand.Parameters.AddWithValue("@Enombre", platoData.nombre);
                    myCommand.Parameters.AddWithValue("@Edescripcion", platoData.descripcion);
                    myCommand.Parameters.AddWithValue("@Eimagen", platoData.imagen);
                    myCommand.Parameters.AddWithValue("@Eprecio", platoData.precio);
                    
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
