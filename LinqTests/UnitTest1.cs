using System;
using ExpectedObjects;
using LinqTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using NSubstitute.Routing.Handlers;

namespace LinqTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void find_products_that_price_between_200_and_500()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = WithoutLinq.FindProductByPrice(products, 200, 500, "Odd-e");

            var expected = new List<Product>()
            {
                new Product {Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e"},
                new Product {Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e"}
            };

            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        public void MyOwnLinq_Where()
        {
            var products = RepositoryFactory.GetProducts();
            var actual =
                products.MyOwnWhere(prod => (prod.Price <= 500 && prod.Price >= 200 && prod.Supplier == "Odd-e"));
            //var k = products.Where(p => (true));
            Console.WriteLine();
            Console.WriteLine();

            foreach (var aProduct in actual)
            {
                Console.Write(aProduct.Price);
            }

            var expected = new List<Product>()
            {
                new Product {Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e"},
                new Product {Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e"}
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void MyOwnLinq_Where2()
        {
            var employee = RepositoryFactory.GetEmployees();
            //var actual = YourOwnLinq.MyOwnWhere<Employee>(
            //    employee, emp => (emp.Age >= 25 && emp.Age <= 40));
            var actual = employee.MyOwnWhere(emp => (emp.Age >= 25 && emp.Age <= 40));

            var expected = new List<Employee>()
            {
                new Employee{Name="Tom", Role=RoleType.Engineer, MonthSalary=140, Age=33, WorkingYear=2.6} ,
                new Employee{Name="Bas", Role=RoleType.Engineer, MonthSalary=280, Age=36, WorkingYear=2.6} ,
                new Employee{Name="Mary", Role=RoleType.OP, MonthSalary=180, Age=26, WorkingYear=2.6} ,
                new Employee{Name="Joey", Role=RoleType.Engineer, MonthSalary=250, Age=40, WorkingYear=2.6},
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }
    }
}

internal class WithoutLinq
{
    public static List<Product> FindProductByPrice(IEnumerable<Product> products, int lowBoundary, int highBoundary, string supporter)
    {
        var result = new List<Product>();
        foreach (var prod in products)
        {
            if (prod.Price <= 500 && prod.Price >= 200 && prod.Supplier == supporter)
            {
                result.Add(prod);
            }
        }
        return result;
    }
}

public  static class YourOwnLinq
{
    public static IEnumerable<T> MyOwnWhere<T>(this IEnumerable<T> source, Func<T, bool> func)
    {
        foreach (var p in source)
        {
            if (func(p))
            {
                yield return p;
            }
        }
    }
}