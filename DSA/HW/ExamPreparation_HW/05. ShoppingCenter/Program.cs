using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wintellect.PowerCollections;

class Program
{
    static ShoppingCenter center = new ShoppingCenter();
    static StringBuilder answer = new StringBuilder();

    static void Main()
    {
        int commandsCount = int.Parse(Console.ReadLine());
        for (int i = 0; i < commandsCount; i++)
        {
            string command = Console.ReadLine();
            answer.AppendLine(center.ProcessCommand(command));
        }
        Console.Write(answer);
    }
}

public class Product : IComparable<Product>
{
    public string Name { get; set; }

    public decimal Price { get; set; }

    public string Producer { get; set; }

    public Product(string name, string price, string producer)
    {
        this.Name = name;
        this.Price = decimal.Parse(price);
        this.Producer = producer;
    }

    public string GetProductAndProducer()
    {
        return this.Name + ";" + this.Producer;
    }

    public int CompareTo(Product other)
    {
        if (this.Name.CompareTo(other.Name) == 0)
        {
            if (this.Producer.CompareTo(other.Producer) == 0)
            {
                return this.Price.CompareTo(other.Price);
            }
            else
            {
                return this.Producer.CompareTo(other.Producer);
            }
        }
        else
        {
            return this.Name.CompareTo(other.Name);
        }
    }

    public override string ToString()
    {
        return "{" + this.Name + ";" + this.Producer + ";" + string.Format("{0:0.00}", this.Price) + "}";
    }
}

public class ShoppingCenter
{
    private const string PRODUCT_ADDED = "Product added";
    private const string X_PRODUCTS_DELETED = " products deleted";
    private const string NO_PRODUCTS_FOUND = "No products found";
    private const string INCORRECT_COMMAND = "Incorrect command";

    private readonly MultiDictionary<string, Product> productsByName; // Use hash_multimap in C++ STL
    private readonly MultiDictionary<string, Product> nameAndProducer;
    private readonly OrderedMultiDictionary<decimal, Product> productsByPrice; // Use multimap in C++ STL
    private readonly MultiDictionary<string, Product> productsByProducer;

    public ShoppingCenter()
    {
        productsByName = new MultiDictionary<string, Product>(true);
        nameAndProducer = new MultiDictionary<string, Product>(true);
        productsByPrice = new OrderedMultiDictionary<decimal, Product>(true);
        productsByProducer = new MultiDictionary<string, Product>(true);
    }

    private string AddProduct(string name, string price, string producer)
    {
        Product product = new Product(name, price, producer);
        productsByName.Add(name, product);
        string nameAndProducerKey = product.GetProductAndProducer();
        nameAndProducer.Add(nameAndProducerKey, product);
        productsByPrice.Add(product.Price, product);
        productsByProducer.Add(producer, product);
        return PRODUCT_ADDED;
    }

    private string DeleteProducts(string name, string producer)
    {
        string nameAndProducerKey = name + ";" + producer;
        if (!nameAndProducer.ContainsKey(nameAndProducerKey))
        {
            return NO_PRODUCTS_FOUND;
        }
        else
        {
            var productsToBeRemoved = nameAndProducer[nameAndProducerKey];
            foreach (var product in productsToBeRemoved)
            {
                productsByName.Remove(name, product);
                productsByProducer.Remove(producer, product);
                productsByPrice.Remove(product.Price, product);
            }
            int countOfRemovedProducts = nameAndProducer[nameAndProducerKey].Count;
            nameAndProducer.Remove(nameAndProducerKey);
            string countMessage = countOfRemovedProducts + X_PRODUCTS_DELETED;
            return countMessage;
        }
    }

    private string DeleteProducts(string producer)
    {
        if (!productsByProducer.ContainsKey(producer))
        {
            return NO_PRODUCTS_FOUND;
        }
        else
        {
            var productsToBeRemoved = productsByProducer[producer];
            foreach (var product in productsToBeRemoved)
            {
                productsByName.Remove(product.Name, product);
                string nameAndProducerKey = product.GetProductAndProducer();
                nameAndProducer.Remove(nameAndProducerKey, product);
                productsByPrice.Remove(product.Price, product);
            }
            int countOfRemovedProducts = productsByProducer[producer].Count;
            productsByProducer.Remove(producer);
            string countMessage = countOfRemovedProducts + X_PRODUCTS_DELETED;
            return countMessage;
        }
    }

    private string FindProductsByName(string name)
    {
        if (!productsByName.ContainsKey(name))
        {
            return NO_PRODUCTS_FOUND;
        }
        else
        {
            var productsFound = productsByName[name];
            string formattedProducts = FormatProductForPrint(productsFound);
            return formattedProducts;
        }
    }

    private string FindProductsByPriceRange(string from, string to)
    {
        decimal rangeStart = decimal.Parse(from);
        decimal rangeEnd = decimal.Parse(to);

        // In C++ STL use multimap::lower_bound(key) and multimap::upper_bound(key)
        // See also http://www.cplusplus.com/reference/stl/multimap/lower_bound/
        var productsFound = productsByPrice.Range(rangeStart, true, rangeEnd, true).Values;
        if (productsFound.Count == 0)
        {
            return NO_PRODUCTS_FOUND;
        }
        else
        {
            string formattedProducts = FormatProductForPrint(productsFound);
            return formattedProducts;
        }
    }

    private string FindProductsByProducer(string producer)
    {
        if (!productsByProducer.ContainsKey(producer))
        {
            return NO_PRODUCTS_FOUND;
        }
        else
        {
            var productsFound = productsByProducer[producer];
            string formattedProducts = FormatProductForPrint(productsFound);
            return formattedProducts;
        }
    }

    private string FormatProductForPrint(ICollection<Product> products)
    {
        List<Product> sortedProducts = new List<Product>(products);
        sortedProducts.Sort();
        StringBuilder builder = new StringBuilder();
        foreach (var product in sortedProducts)
        {
            builder.AppendLine(product.ToString());
        }
        string formattedProducts = builder.ToString().TrimEnd();
        return formattedProducts;
    }

    public string ProcessCommand(string command)
    {
        int indexOfFirstSpace = command.IndexOf(' ');
        string method = command.Substring(0, indexOfFirstSpace);
        string parameterValues = command.Substring(indexOfFirstSpace + 1);
        string[] parameters = parameterValues.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        string commandResult;
        switch (method)
        {
            case "AddProduct":
                {
                    commandResult = AddProduct(parameters[0], parameters[1], parameters[2]);
                    break;
                }
            case "DeleteProducts":
                {
                    if (parameters.Length == 1)
                    {
                        commandResult = DeleteProducts(parameters[0]);
                    }
                    else
                    {
                        commandResult = DeleteProducts(parameters[0], parameters[1]);
                    }
                    break;
                }
            case "FindProductsByName":
                {
                    commandResult = FindProductsByName(parameters[0]);
                    break;
                }

            case "FindProductsByPriceRange":
                {
                    commandResult = FindProductsByPriceRange(parameters[0], parameters[1]);
                    break;
                }
            case "FindProductsByProducer":
                {
                    commandResult = FindProductsByProducer(parameters[0]);
                    break;
                }
            default:
                {
                    commandResult = INCORRECT_COMMAND;
                    break;
                }
        }
        return commandResult;
    }
}