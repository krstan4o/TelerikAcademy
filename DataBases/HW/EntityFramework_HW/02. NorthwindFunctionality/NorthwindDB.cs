using System;
using Northwind.Data;
using System.Linq;
using System.Collections.Generic;
public static class NorthwindDB
{
   
    public static void InsertCustomer(string customerId, string companyName, string contactName, string contactTitle, string address, 
        string city, string region, string postalCode, string country, string phone, string fax) 
    {
        NorthwindEntities db = new NorthwindEntities();
        using (db)
        {
            Customer customer = CreateCustomer(customerId, companyName, contactName,
                contactTitle, address, city, region, postalCode, country, phone, fax);
            db.Customers.Add(customer);
            db.SaveChanges();
        }
    }

    public static void ModifyCustomer(string oldCostumerId, string companyName,
        string contactName, string contactTitle, string address,
        string city, string region, string postalCode, string country, string phone, string fax) 
    {

        NorthwindEntities db = new NorthwindEntities();
        using (db)
        {

            var customer = db.Customers.Where(x => x.CustomerID == oldCostumerId).FirstOrDefault();
            customer.CompanyName = companyName;
            customer.ContactName = contactName;
            customer.ContactTitle = contactTitle;
            customer.Address = address;
            customer.City = city;
            customer.Region = region;
            customer.PostalCode = postalCode;
            customer.Country = country;
            customer.Phone = phone;
            customer.Fax = fax;
           db.SaveChanges();
        }
    }

    public static void DeleteCustomer(string customerId) 
    {
          NorthwindEntities db = new NorthwindEntities();
          using (db)
          {
              var customerToRemove = db.Customers.FirstOrDefault(x => x.CustomerID == customerId);
              db.Customers.Remove(customerToRemove);
              db.SaveChanges();
          }
    }

    private static Customer CreateCustomer(string customerId, string companyName, string contactName, string contactTitle, string address, string city, string region, string postalCode, string country, string phone, string fax)
    {
        Customer customer = new Customer
        {  
            CustomerID = customerId,
            CompanyName = companyName,
            ContactName = contactName,
            ContactTitle = contactTitle,
            Address = address,
            City = city,
            Region = region,
            PostalCode = postalCode,
            Country = country,
            Phone = phone,
            Fax = fax
        };
        return customer;
    }
}

