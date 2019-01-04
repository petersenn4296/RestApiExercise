using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using RestApiExercise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiExercise.Services
{
    public class CustomerService
    {
        private readonly IMongoCollection<Customer> _customers;

        public CustomerService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("RestApiExerciseDB"));
            var database = client.GetDatabase("RestApiExerciseDB");
            _customers = database.GetCollection<Customer>("Customers");
        }

        public List<Customer> Get()
        {
            return _customers.Find(customer => true).ToList();
        }

        public Customer Get(string id)
        {
            var docId = new ObjectId(id);

            return _customers.Find<Customer>(customer => customer.Id.Equals(docId)).FirstOrDefault();
        }

        public Customer Create(Customer customer)
        {
            customer.Id = ObjectId.GenerateNewId().ToString();
            _customers.InsertOne(customer);

            return customer;
        }

        public void Update(string id, Customer updatedCustomer)
        {
            var docId = new ObjectId(id);

            _customers.ReplaceOne(customer => customer.Id.Equals(docId), updatedCustomer);
        }

        public void Remove(ObjectId id)
        {
            _customers.DeleteOne(customer => customer.Id.Equals(id));
        }
    }
}
