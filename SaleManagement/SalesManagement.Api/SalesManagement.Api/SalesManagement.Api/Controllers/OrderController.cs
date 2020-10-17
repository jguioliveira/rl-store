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
        public void InsertOrder(Order order)
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
        public List<Order> GetListOrder(string dataInicial, string dataFinal)
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
        [Route("id/{id}")]
        public Order GetId(string id)
        {
            string stringDeConexao = "Server=localhost;Database=rlsalesdb;Uid=root;Pwd=123456;";
            MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(stringDeConexao);

            MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySql.Data.MySqlClient.MySqlCommand("PR_TB_Order_Id_Select", connection);
            mySqlCommand.Parameters.Add("@varId", MySqlDbType.String).Value = id;
           
            mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            connection.Open();

            MySqlDataReader tabela = mySqlCommand.ExecuteReader();

            bool existeDados = tabela.Read();

            Order order = new Order();

            if (existeDados)
            {
                order.Id = tabela.GetString("Id");
                order.CustomerId = tabela.GetString("CustomerId");
                order.Status = tabela.GetInt32("Status");
                order.Total = tabela.GetDouble("Total");
                order.Created = tabela.GetDateTime("Created");
                order.Updated = tabela.GetDateTime("Updated");
                order.PaymentForm = tabela.GetInt32("PaymentForm");
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
                OI.Count = tabela.GetInt16("Count");
                OI.UnitValue = tabela.GetDouble("UnitValue");
                OI.Total = tabela.GetDouble("Total");
                OI.ProductName = tabela.GetString("ProductName");

                orderItem.Add(OI);
                existeDados = tabela.Read();
            }
            connection.Close();
            return orderItem;
            
        }

        [HttpGet]
        [Route("{id}/items")]
        public OrderItem GetItemsId(string id)
        {
          string stringDeConexao = "Server=localhost;Database=rlsalesdb;Uid=root;Pwd=123456;";
           MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(stringDeConexao);

          MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySql.Data.MySqlClient.MySqlCommand("PR_TB_OrderItem_Id_Select", connection);
          mySqlCommand.Parameters.Add("@varId", MySqlDbType.String).Value = id;
  
          mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

           connection.Open();

           MySqlDataReader tabela = mySqlCommand.ExecuteReader();

           bool existeDados = tabela.Read();

           OrderItem orderItem = new OrderItem();

           if(existeDados)
           {
                
               orderItem.OrderId = tabela.GetString("OrderId");
               orderItem.ProductId = tabela.GetString("ProductId");
               orderItem.Count = tabela.GetInt16("Count");
               orderItem.UnitValue = tabela.GetDouble("UnitValue");
               orderItem.Total = tabela.GetDouble("Total");
               orderItem.ProductName = tabela.GetString("ProductName");

               
            }
            connection.Close();
            return orderItem;
        }

        [HttpPost]
        [Route("{id}/insert/items")]
        public void InsertItems(string id, [FromBody] List<OrderItem> orderItem)
        {
            string stringDeConexao = "Server=localhost;Database=rlsalesdb;Uid=root;Pwd=123456;";
            MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(stringDeConexao);
            
            connection.Open();

            foreach (var item in orderItem)
            {
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySql.Data.MySqlClient.MySqlCommand("PR_TB_OrderItem_Insert", connection);
                mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                mySqlCommand.Parameters.AddWithValue("varOrderId", item.OrderId);
                mySqlCommand.Parameters.AddWithValue("varProductId", item.ProductId);
                mySqlCommand.Parameters.AddWithValue("varCount", item.Count);
                mySqlCommand.Parameters.AddWithValue("varUnitValue", item.UnitValue);
                mySqlCommand.Parameters.AddWithValue("varTotal", item.Total);
                mySqlCommand.Parameters.AddWithValue("varProductName", item.ProductName);

                mySqlCommand.ExecuteNonQuery();
            }           
            connection.Close();

        }

        [HttpGet]
        [Route("{id}/order/items")]
        public Order GetOrder_OrderItem(string id)
        {

            string stringDeConexao = "Server=localhost;Database=rlsalesdb;Uid=root;Pwd=123456;";
            MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(stringDeConexao);

            MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySql.Data.MySqlClient.MySqlCommand("PR_TB_Order_Id_Select", connection);
            mySqlCommand.Parameters.Add("@varId", MySqlDbType.String).Value = id;

            mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            connection.Open();

            MySqlDataReader tabela = mySqlCommand.ExecuteReader();

            bool existeDados = tabela.Read();

            Order order = new Order();

            if (existeDados)
            {
                order.Id = tabela.GetString("Id");
                order.CustomerId = tabela.GetString("CustomerId");
                order.Status = tabela.GetInt32("Status");
                order.Total = tabela.GetDouble("Total");
                order.Created = tabela.GetDateTime("Created");
                order.Updated = tabela.GetDateTime("Updated");
                order.PaymentForm = tabela.GetInt32("PaymentForm");
            }
            connection.Close();
            MySql.Data.MySqlClient.MySqlCommand mySqlCommando = new MySql.Data.MySqlClient.MySqlCommand("PR_TB_OrderItem_Id_Select", connection);
            mySqlCommando.Parameters.Add("@varId", MySqlDbType.String).Value = id;

            mySqlCommando.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();
            MySqlDataReader tabela1 = mySqlCommando.ExecuteReader();

           
            order.Items = new List<OrderItem>();
            bool existeDado = tabela1.Read();

            while (existeDado)
            {
                OrderItem orderItem = new OrderItem();
                orderItem.OrderId = tabela1.GetString("OrderId");
                orderItem.ProductId = tabela1.GetString("ProductId");
                orderItem.Count = tabela1.GetInt16("Count");
                orderItem.UnitValue = tabela1.GetDouble("UnitValue");
                orderItem.Total = tabela1.GetDouble("Total");
                orderItem.ProductName = tabela1.GetString("ProductName");

                existeDado = tabela1.Read();
                order.Items.Add(orderItem);
            }
            connection.Close();

            return order;
        }
        
        [HttpPost]
        [Route("fullOrder")]
        public void InsertOrder_OrderItem(Order order)
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
         
            foreach (var item in order.Items)
            {
                MySql.Data.MySqlClient.MySqlCommand mySqlCommando = new MySql.Data.MySqlClient.MySqlCommand("PR_TB_OrderItem_Insert", connection);
                mySqlCommando.CommandType = System.Data.CommandType.StoredProcedure;

                mySqlCommando.Parameters.AddWithValue("varOrderId", item.OrderId);
                mySqlCommando.Parameters.AddWithValue("varProductId", item.ProductId);
                mySqlCommando.Parameters.AddWithValue("varCount", item.Count);
                mySqlCommando.Parameters.AddWithValue("varUnitValue", item.UnitValue);
                mySqlCommando.Parameters.AddWithValue("varTotal", item.Total);
                mySqlCommando.Parameters.AddWithValue("varProductName", item.ProductName);

                mySqlCommando.ExecuteNonQuery();
            }
            connection.Close();

         
        }

        [HttpPut]
         public void UpdateOrder(OrderUpdateStatusCommand ousc)
         {
            string stringDeConexao = "Server=localhost;Database=rlsalesdb;Uid=root;Pwd=123456;";
            MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(stringDeConexao);

            MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySql.Data.MySqlClient.MySqlCommand("PR_TB_Order_Status_Update", connection);
            mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();

            mySqlCommand.Parameters.AddWithValue("varId", ousc.Id);
            mySqlCommand.Parameters.AddWithValue("varStatus", ousc.Status);
            mySqlCommand.Parameters.AddWithValue("varUpdated", ousc.Updated);

            mySqlCommand.ExecuteNonQuery();
            connection.Close();
        }

        [HttpDelete]
        [Route("{id}/delete")]
        public void DeleteOrder(string Id)
        {
            string stringDeConexao = "Server=localhost;Database=rlsalesdb;Uid=root;Pwd=123456;";
            MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(stringDeConexao);

            MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySql.Data.MySqlClient.MySqlCommand("PR_TB_Order_Delete", connection);
            mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();

            mySqlCommand.Parameters.AddWithValue("varId", Id);
            
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

        public class OrderUpdateStatusCommand
        {
            public string Id { get; set; }
            public int Status { get; set; }
            public DateTime Updated { get; set; }
        }
    }
}
