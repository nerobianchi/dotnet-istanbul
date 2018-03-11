using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace OrderLib.IntegrationTests
{
    public class OrderRepositoryTests : IDisposable
    {
        private readonly OrderRepository sut;

        public OrderRepositoryTests()
        {
            OrderRepositoryTestHelper.InitializeDb();
            this.sut = new OrderRepository();
        }

        [Fact]
        public void given_an_order_when_listig_then_return_one_order()
        {
            OrderRepositoryTestHelper.InsertOneOrder();
            List<Order> actuaList = sut.List();
            Assert.True(actuaList.Count > 0);
        }

        [Fact]
        public void given_an_order_when_listing_then_first_order_id_should_1()
        {
            OrderRepositoryTestHelper.InsertOneOrder();
            List<Order> actuaList = sut.List();

            Assert.Equal(1, actuaList[0].Id);
        }

        public void Dispose()
        {
            OrderRepositoryTestHelper.CleanupOrder();
        }
    }

    public class OrderRepositoryTestHelper
    {
        public static void InitializeDb()
        {
            SqlConnection connection = new SqlConnection("Server=127.0.0.1;database=testdb; user id=sa;password=Pass123!!");
            using (connection)
            {
                var sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = @"
if object_id('dbo.[Order]') is not null 
  drop table dbo.[Order]; 

create table dbo.[Order]
(
    id int identity not null primary key
)
";
                sqlCommand.CommandType = CommandType.Text;

                using (sqlCommand)
                {
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        public static void InsertOneOrder()
        {
            SqlConnection connection = new SqlConnection("Server=127.0.0.1;database=testdb; user id=sa;password=Pass123!!");
            using (connection)
            {
                var sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = @"
set identity_insert dbo.[Order] on

insert into dbo.[Order] (id) values (1)

set identity_insert dbo.[Order] off
";
                sqlCommand.CommandType = CommandType.Text;

                using (sqlCommand)
                {
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        public static void CleanupOrder()
        {
            SqlConnection connection = new SqlConnection("Server=127.0.0.1;database=testdb; user id=sa;password=Pass123!!");
            using (connection)
            {
                var sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = @"
if object_id('dbo.[Order]') is not null 
  drop table dbo.[Order]; 
";
                sqlCommand.CommandType = CommandType.Text;

                using (sqlCommand)
                {
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
    }
}