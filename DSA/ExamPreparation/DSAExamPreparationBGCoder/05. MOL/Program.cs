using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Wintellect.PowerCollections;
using System.IO;
class Program
{
    static OrderedMultiDictionary<string, Product> productsByName = new OrderedMultiDictionary<string, Product>(true);
    static OrderedMultiDictionary<string, Product> productsByProducer = new OrderedMultiDictionary<string, Product>(true);
    static OrderedMultiDictionary<double, Product> productsPrice = new OrderedMultiDictionary<double, Product>(true);
    
    static void Main()
    {
        int commandsCount = int.Parse(Console.ReadLine());
        var consoleOut = new StringWriter();
        Console.SetOut(consoleOut);
        
       
        
        for (int i = 0; i < commandsCount; i++)
        {
            string command = Console.ReadLine();
            if (command[0] == 'A')
            {
                AddProduct(command);
            }
            else if (command.StartsWith("FindProductsByName"))
            {
                FindProductsByName(command);
            }
            else if (command.StartsWith("FindProductsByProducer")) 
            {
                FindProductsByProducer(command);
            }
            else if (command.StartsWith("FindProductsByPriceRange"))
            {
                FindProductsByPriceRange(command);
            }
            else if (command.StartsWith("DeleteProducts"))
            {
                if (command.Contains(";"))
                {
                    DeleteProductsByNameProducer(command);
                }
                else
                {
                    DeleteProducts(command);
                }
            }
        }
        File.WriteAllText("../../out.txt", consoleOut.ToString());
    }

    static void AddProduct(string command) 
    {
        string pattern = @"(\w+\S)(.+);(\d+.?\d+);(\w+)";
        var item = Regex.Match(command, pattern);
        string name = item.Groups[2].Value.Trim();
        double price = double.Parse(item.Groups[3].Value.Trim());
        string producer = item.Groups[4].Value.Trim();
        Product product = new Product(name, price, producer);

        productsByName.Add(name, product);
        productsByProducer.Add(producer, product);
        productsPrice.Add(product.Price, product);

        Console.WriteLine("Product added");
    }

    static void FindProductsByName(string command) 
    {
        string pattern = @"(\w+) (.+)";
        var item = Regex.Match(command, pattern);
        string nameOfProduct = item.Groups[2].Value;
        if (productsByName.ContainsKey(nameOfProduct))
        {
            var findedProduct = productsByName[nameOfProduct];
            foreach (Product product in findedProduct)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("{");
                sb.Append(product.Name + ";");
                sb.Append(product.Producer + ";");
                sb.Append(string.Format("{0:0.00}", product.Price));
                sb.Append("}");
                Console.WriteLine(sb);
            }
        }
        else
            Console.WriteLine("No products found");
    }

    static void FindProductsByProducer(string command) 
    {
        string pattern = @"(\w+) (.+)";
       
        var item = Regex.Match(command, pattern);
        string nameOfProducer = item.Groups[2].Value;
        if (productsByProducer.ContainsKey(nameOfProducer))
        {
            var findedProducts = productsByProducer[nameOfProducer];
            foreach (var product in findedProducts)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("{");
                sb.Append(product.Name + ";");
                sb.Append(product.Producer + ";");
                sb.Append(string.Format("{0:0.00}", product.Price));
                sb.Append("}");
                Console.WriteLine(sb);
            }
        }
        else
            Console.WriteLine("No products found");
    }
    
    static void FindProductsByPriceRange(string command)
    {
        string pattern = @"([0-9]+);([0-9]+)";
        var item = Regex.Match(command, pattern);
        double from = double.Parse(item.Groups[1].Value);
        double to = double.Parse(item.Groups[2].Value);
        
        var findedProducts = productsPrice.Range(from, true, to, true);
        if (findedProducts.Count == 0)
        {
            Console.WriteLine("No products found");
        }
        else
        {
            foreach (Product product in findedProducts.Values)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("{");
                sb.Append(product.Name + ";");
                sb.Append(product.Producer + ";");
                sb.Append(string.Format("{0:0.00}", product.Price));
                sb.Append("}");
                Console.WriteLine(sb);
            }
        }
    }

    static void DeleteProducts(string command)
    {
        string pattern = @"(\w+) (.+)";
        var item = Regex.Match(command, pattern);
        string nameOfProducer = item.Groups[2].Value;

        if (productsByProducer.ContainsKey(nameOfProducer))
        {
            var findedProducts = productsByProducer[nameOfProducer];
            int result = findedProducts.Count;
            foreach (var product in findedProducts)
            {
                string name = product.Name;
                double price = product.Price;
                productsByName.Remove(name);
                productsPrice.Remove(price);
            }
            productsByProducer.Remove(nameOfProducer);
            Console.WriteLine("{0} products deleted", result);
        }
        else
            Console.WriteLine("No products found");
    }

    static void DeleteProductsByNameProducer(string command) 
    {
        string pattern = @"(\w+) (.+\S);(\w+)";
        var item = Regex.Match(command, pattern);
        string productName = item.Groups[2].Value;
        string producer = item.Groups[3].Value;
       
        int result = 0;
        if (productsByProducer.ContainsKey(producer))
        {
            var findedProducts = productsByProducer[producer];
            
            foreach (var product in findedProducts)
            {
                if (product.Name == productName)
                {
                    result++;
                    productsByName.RemoveAll(x => x.Value == product);
                    productsByProducer.RemoveAll(x => x.Value == product);
                    productsPrice.RemoveAll(x => x.Value == product);
                }
            }
           
            if (result == 0)
            {
                Console.WriteLine("No products found");
            }
            else
                Console.WriteLine("{0} products deleted", result);
        }
        else
            Console.WriteLine("No products found");
    }
}

public class Product : IComparable
{
    public string Name { get; set; }

    public double Price { get; set; }

    public string Producer { get; set; }

    public Product(string name, double price, string producer) 
    {
        this.Name = name;
        this.Price = price;
        this.Producer = producer;
    }

    public int CompareTo(object obj)
    {
        if (obj == null)
            return 1;

        Product otherProduct = obj as Product;
        if (otherProduct != null)
            return this.Name.CompareTo(otherProduct.Name);
        else
            throw new ArgumentException("Object is not an Article");
    }
}

