using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace SalesManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpPost]
        public void Post(Order order)
        {
           
            string stringDeConexao = "Server=localhost;Database=rlsalesdb;Uid=root;Pwd=123456;";
            MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(stringDeConexao);

            MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySql.Data.MySqlClient.MySqlCommand("PR_TB_Order_Insert", connection);

            mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();
            order.Created = DateTime.Now;
            mySqlCommand.Parameters.AddWithValue("varId", order.Id);
            mySqlCommand.Parameters.AddWithValue("varCustomerId", order.CustomerId);
            mySqlCommand.Parameters.AddWithValue("varStatus", order.Status);
            mySqlCommand.Parameters.AddWithValue("varTotal", order.Total);
            mySqlCommand.Parameters.AddWithValue("varCreated", order.Created);
            mySqlCommand.Parameters.AddWithValue("varUpdated", DateTime.Now);
            mySqlCommand.Parameters.AddWithValue("varPaymentForm", order.PaymentForm);

            mySqlCommand.ExecuteNonQuery();

            connection.Close();

        }

        [HttpGet]
        [Route("de/{dataInicial}/ate/{dataFinal}")]
        public List<Order> Get(string dataInicial, string dataFinal)
        {
            var dtInicial = DateTime.Parse(dataInicial);
            var dtFinal = DateTime.Parse(dataFinal);


            string stringDeConexao = "Server=localhost;Database=rlsalesdb;Uid=root;Pwd=123456;";
            MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(stringDeConexao);

            MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySql.Data.MySqlClient.MySqlCommand("PR_TB_Order_Select", connection);
            mySqlCommand.Parameters.Add("@inicialDate", MySqlDbType.Datetime).Value = dtInicial;
            mySqlCommand.Parameters.Add("@finalDate", MySqlDbType.DateTime).Value = dtFinal;

            mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            connection.Open();

            MySqlDataReader tabela = mySqlCommand.ExecuteReader();

            bool existeDados = tabela.Read();
            List<Order> order = new List<Order>();

            while (existeDados)
            {
                Order or = new Order();
                or.Id = tabela.GetString("Id");
                or.CustomerId = tabela.GetString("CustomerId");
                or.Status = tabela.GetInt32("Status");
                or.Total = tabela.GetDouble("Total");
                or.Created = tabela.GetDateTime("Created");
                or.Updated = tabela.GetDateTime("Updated");
                or.PaymentForm = tabela.GetInt32("PaymentForm");

                order.Add(or);
                existeDados = tabela.Read();
            }

            connection.Close();

            return order;
        }
        
        [HttpGet]
        [Route("items/de/{dataInicial}/ate/{dataFinal}")]
        public List<OrderItem> GetItems(string dataInicial, string dataFinal)
        {
            var dtInicial = DateTime.Parse(dataInicial);
            var dtFinal = DateTime.Parse(dataFinal);

            string stringDeConexao = "Server=localhost;Database=rlsalesdb;Uid=root;Pwd=123456;";
            MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(stringDeConexao);

            MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySql.Data.MySqlClient.MySqlCommand("PR_TB_OrderItem_Select", connection);
            mySqlCommand.Parameters.Add("@inicialDate", MySqlDbType.Datetime).Value = dtInicial;
            mySqlCommand.Parameters.Add("@finalDate", MySqlDbType.DateTime).Value = dtFinal;
            mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            connection.Open();

            MySqlDataReader tabela = mySqlCommand.ExecuteReader();

            bool existeDados = tabela.Read();

            List<OrderItem> orderItem = new List<OrderItem>();

            while (existeDados)
            {
                OrderItem OI = new OrderItem();
                OI.OrderId = tabela.GetString("OrderId");
                OI.ProductId = tabela.GetString("ProductId");
                OI.UnitValue = tabela.GetDouble("UnitValue");
                OI.Total = tabela.GetDouble("Total");
                OI.ProductName = tabela.GetString("ProductName");

                orderItem.Add(OI);
                existeDados = tabela.Read();
            }
            connection.Close();
            return orderItem;
            
        }
        
        [HttpPost]
        [Route("{id}/items")]
        public void InsertItems(string id, OrderItem orderItem)
        {
            string stringDeConexao = "Server=localhost;Database=rlsalesdb;Uid=root;Pwd=123456;";
            MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(stringDeConexao);

            MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySql.Data.MySqlClient.MySqlCommand("PR_TB_OrderItem_Insert", connection);
          
            mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();
            mySqlCommand.Parameters.AddWithValue("varOrderId", orderItem.OrderId);
            mySqlCommand.Parameters.AddWithValue("varProductId", orderItem.ProductId);
            mySqlCommand.Parameters.AddWithValue("varCount", orderItem.Count);
            mySqlCommand.Parameters.AddWithValue("varUnitValue", orderItem.UnitValue);
            mySqlCommand.Parameters.AddWithValue("varTotal", orderItem.Total);
            mySqlCommand.Parameters.AddWithValue("varProductName", orderItem.ProductName);

            mySqlCommand.ExecuteNonQuery();
            connection.Close();

        }
        
        public class Order
        {
            public string Id { get; set; }
            public string CustomerId { get; set; }
            public int Status { get; set; }
            public double Total { get; set; }
            public DateTime Created { get; set; }
            public DateTime Updated { get; set; }
            public int PaymentForm { get; set; }
            public List<OrderItem> Items { get; set; }
        }

        public class OrderItem
        {
            public string OrderId { get; set; }
            public string ProductId { get; set; }
            public int Count { get; set; }
            public double UnitValue { get; set; }
            public double Total { get; set; }
            public string ProductName { get; set; }
        }
    }
}
