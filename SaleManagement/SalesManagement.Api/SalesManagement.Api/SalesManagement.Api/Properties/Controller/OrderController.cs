using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace SalesManagement.Api.Properties.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public class Order
        {
            public string Id { get; set; }
            public string CustomerId { get; set; }
            public int Status { get; set; }
            public double Total { get; set; }
            public DateTime Created { get; set; }
            public DateTime Updated { get; set; }
            public int PaymentForm { get; set; }
        }

       

        public OrderController(string Id, string CustomerId, int Status, double Total, DateTime Created, DateTime Updated, int PaymentForm)
        {
            string stringDeConexao = "Server=localhost;Database=rlsalesdb;Uid=root;Pwd=123456;";
            MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(stringDeConexao);

            MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySql.Data.MySqlClient.MySqlCommand("PR_TB_Order_Insert", connection);

            mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();
            mySqlCommand.ExecuteNonQuery();

            mySqlCommand.Parameters.AddWithValue("varId", Id);
            mySqlCommand.Parameters.AddWithValue("varCustomerId", CustomerId);
            mySqlCommand.Parameters.AddWithValue("varStatus", Status);
            mySqlCommand.Parameters.AddWithValue("varTotal", Total);
            mySqlCommand.Parameters.AddWithValue("varCretaed", Created);
            mySqlCommand.Parameters.AddWithValue("varUpdated", Updated);
            mySqlCommand.Parameters.AddWithValue("varPaymentForm", PaymentForm);

            connection.Close();

        }
    }
}
