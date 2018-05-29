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
    public class LinqTest
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
                products.MyWhere(prod => (prod.Price <= 500 && prod.Price >= 200 && prod.Supplier == "Odd-e"));
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
            //var actual = YourOwnLinq.MyWhere<Employee>(
            //    employee, emp => (emp.Age >= 25 && emp.Age <= 40));
            var actual = employee.MyWhere(emp => (emp.Age >= 25 && emp.Age <= 40));

            var expected = new List<Employee>()
            {
                new Employee{Name="Tom", Role=RoleType.Engineer, MonthSalary=140, Age=33, WorkingYear=2.6} ,
                new Employee{Name="Bas", Role=RoleType.Engineer, MonthSalary=280, Age=36, WorkingYear=2.6} ,
                new Employee{Name="Mary", Role=RoleType.OP, MonthSalary=180, Age=26, WorkingYear=2.6} ,
                new Employee{Name="Joey", Role=RoleType.Engineer, MonthSalary=250, Age=40, WorkingYear=2.6},
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void ToHttpsUrl()
        {
            var urls = RepositoryFactory.GetUrls();
            var actual = urls.MySelect(url =>
            {
                var s = url.Replace("http", "https");
                return s;
            });
            var expected = new string[]
            {
                "https://tw.yahoo.com:80",
                "https://facebook.com:80",
                "https://twitter.com:80",
                "https://github.com:80"
            };
            expected.ToExpectedObject().ShouldEqual(actual.ToArray());
        }

        //[TestMethod]
        //public void ToHttpsUrl()
        //{
        //    var urls = RepositoryFactory.GetUrls();

        //    var actual = urls.MySelect(url=>
        //    {
        //        var s = url.Replace("http:", "https:");
        //        return s + ":80";
        //    });
        //    var expected = new string[]
        //    {
        //        "https://tw.yahoo.com:80",
        //        "https://facebook.com:80",
        //        "https://twitter.com:80",
        //        "https://github.com:80"
        //    };

        //    expected.ToExpectedObject().ShouldEqual(actual.ToArray());
        //}

        [TestMethod]
        public void GetUrlLength()
        {
            var urls = RepositoryFactory.GetUrls();

            var actual = urls.MySelect(p => p.Length);
            
            var expected = new int[]
            {
                19,20,19,17
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToArray());
        }

        [TestMethod]
        public void GetHttpsUrlTest()
        {
            var urls = RepositoryFactory.GetUrls();
            var actual = urls.MySelect2(s => s.Contains("http:") ? s.Replace("http:", "https:") : s);
            //var actual = WithoutLinq.Tohttps(urls);
            //urls.Select()
            var expected = new List<string>()
            {
                "https://tw.yahoo.com",
                "https://facebook.com",
                "https://twitter.com",
                "https://github.com",
            };
            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void GetHttpsAdd80PortUrlTest()
        {
            var urls = RepositoryFactory.GetUrls();
            var actual = urls.MySelect2(s => s.Contains("http:") ? s.Replace("http:", "https:") : s).MySelect2(s=>s+":80");

            var expected = new List<string>()
            {
                "https://tw.yahoo.com:80",
                "https://facebook.com:80",
                "https://twitter.com:80",
                "https://github.com:80",
            };
            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void Add91PortTest()
        {
            var urls = RepositoryFactory.GetUrls();

            var actual = YourOwnLinq.Append91Port(urls);
            var expected = new string[]
            {
                "http://tw.yahoo.com:91",
                "https://facebook.com",
                "https://twitter.com:91",
                "http://github.com"
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToArray());
        }

        [TestMethod]
        public void GetUrlLength2()
        {
            var urls = RepositoryFactory.GetUrls();
            var actual = urls.MySelect2(u => u.Length);
            var expected = new[]
            {
                19,20,19,17
            };
            expected.ToExpectedObject().ShouldEqual(actual.ToArray());
        }


        [TestMethod]
        public void Add91PortTest2()
        {
            var urls = RepositoryFactory.GetUrls();
            var actual =  urls.MySelect2(s => s.Contains("tw") ? s + ":91" : s);
            var expected = new string[]
            {
                "http://tw.yahoo.com:91",
                "https://facebook.com",
                "https://twitter.com:91",
                "http://github.com"
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToArray());
        }

        [TestMethod]
        public void FilterEmployeeTest2()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.MyWhere(emp => emp.Role == RoleType.Engineer).MySelect2(emp => emp.MonthSalary);
            var expected = new []
            {
               100,140,280,120,250
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToArray());
        }

        [TestMethod]
        public void SelectTopTest()
        {
            var employees = RepositoryFactory.GetEmployees();

            var actual = employees.MyTake(3);
            Assert.AreEqual(actual.Count(), 3);
        }

        [TestMethod]
        public void SelectTop3WithIndexTest()
        {
            var employees = RepositoryFactory.GetEmployees();

            var actual = employees
                .Take(3)
                .MySelect((emp,index)=> index.ToString() + "-" + emp.Name);

            var expected = new[]
            {
                "1-Joe",
                "2-Tom",
                "3-Kevin"
            };
            expected.ToExpectedObject().ShouldEqual(actual.ToArray());
        }

        [TestMethod]
        public void Skip6()
        {
            var employees = RepositoryFactory.GetEmployees();

            var actual = employees.MySkip(6);

            var expected = new[]
            {
                new Employee{Name="Frank", Role=RoleType.Engineer, MonthSalary=120, Age=16, WorkingYear=2.6} ,
                new Employee{Name="Joey", Role=RoleType.Engineer, MonthSalary=250, Age=40, WorkingYear=2.6},
            };
            expected.ToExpectedObject().ShouldEqual(actual.ToArray());
        }

        [TestMethod]
        public void MySelectSkip()
        {
            var products = RepositoryFactory.GetProducts();

            var act = products.mySelectSkip(p => p.Price > 300, 4);
            var expect = new[]
            {
                new Product {Id = 1, Cost = 11, Price = 110, Supplier = "Odd-e"},
                new Product {Id = 2, Cost = 21, Price = 210, Supplier = "Yahoo"},
                new Product {Id = 7, Cost = 71, Price = 710, Supplier = "Yahoo"},
                new Product {Id = 8, Cost = 18, Price = 780, Supplier = "Yahoo"},
            };
            expect.ToExpectedObject().ShouldEqual(act.ToArray());

        }

        [TestMethod]
        public void TakeWhile()
        {
            var products = RepositoryFactory.GetProducts();

            var act = products.MyTakeWhile(p => p.Price > 300, 2);
            var expect = new[]
            {
                new Product{Id=3, Cost=31, Price=310, Supplier="Odd-e" },
                new Product{Id=4, Cost=41, Price=410, Supplier="Odd-e" },
            };
            expect.ToExpectedObject().ShouldEqual(act.ToArray());
        }

        [TestMethod]
        public void RealTakeWhileTest()
        {
            var employees = RepositoryFactory.GetEmployees();

            var act = employees.MyRealTakeWhile(e=>e.Age > 30);
            var expect = new[]
            {
                new Employee{Name="Joe", Role=RoleType.Engineer, MonthSalary=100, Age=44, WorkingYear=2.6 } ,
                new Employee{Name="Tom", Role=RoleType.Engineer, MonthSalary=140, Age=33, WorkingYear=2.6} ,
                new Employee{Name="Kevin", Role=RoleType.Manager, MonthSalary=380, Age=55, WorkingYear=2.6} 
            };
            expect.ToExpectedObject().ShouldEqual(act.ToArray());

        }

        [TestMethod]
        public void MySumTest()
        {
            var products = RepositoryFactory.GetProducts();
            var act = products.MySum(p=>p.Price);
            Assert.AreEqual(3650, act);
        }

        [TestMethod]
        public void MySumGroupTest()
        {
            var products = RepositoryFactory.GetProducts();
            var act = products.MySumGroup(3, p => p.Price);
            var expect = new[]
            {
                630,1530,1490
            };
            expect.ToExpectedObject().ShouldEqual(act.ToArray());
        }

        [TestMethod]
        public void Any_Has_RecodrdTest()
        {
            var products = RepositoryFactory.GetProducts();
            var act = products.Myany();
            
            Assert.IsTrue(act);
        }

        [TestMethod]
        public void MyAll_FalseTest()
        {
            var products = RepositoryFactory.GetProducts();
            var act = products.MyAll(p=>p.Price > 200);

            Assert.IsFalse(act);
        }

        [TestMethod]
        public void MyDistinctTest()
        {
            var employees = RepositoryFactory.GetEmployees();
            var act = employees.MySelect(e=>e.Role).MyDistinct();
            var expect = new[]
            {
                RoleType.Engineer,
                RoleType.Manager,
                RoleType.OP
            };
            expect.ToExpectedObject().ShouldEqual(act.ToArray());
        }
    }
}

public static class YourOwnLinq
{
    public static IEnumerable<string> Add91Port(IEnumerable<string> urlsList)
    {
        foreach (var url in urlsList)
        {
            if (url.Contains("tw"))
                yield return url + ":91";
            else yield return url;
        }
    }

    public static IEnumerable<TResult> MySelect2<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
    {
        var enumerator = source.GetEnumerator();
        while (enumerator.MoveNext())
        {
            yield return selector(enumerator.Current);
        }
    }


    public static IEnumerable<TSource> MyDistinct<TSource>(this IEnumerable<TSource> source)
    {
        var r = source.GetEnumerator();
        var hashSet = new HashSet<TSource>();
        while (r.MoveNext())
        {
            var canAdd = hashSet.Add(r.Current);
            if (canAdd)
                yield return r.Current;
        }
    }

    public static bool MyAll<T>(this IEnumerable<T> source, Predicate<T> func)
    {
        //other 
        //var r = source.GetEnumerator();
        //while (r.MoveNext())
        //{
        //    if (!func(r.Current))
        //        return false;
        //}

        foreach (var item in source)
        {
            if (func(item))
                return false;
        }

        return true;
    }

    public static bool Myany<T>(this IEnumerable<T> source)
    {
        return source.GetEnumerator().MoveNext();
    }

    public static IEnumerable<int> MySumGroup<T>(this IEnumerable<T> source, int pageSize, Func<T, int> selectorFunc)
    {
        var rowIndex = 0;
        while (rowIndex <= source.Count())
        {
            yield return source.Skip(rowIndex).Take(pageSize).Sum(selectorFunc);
            rowIndex += pageSize;
        }
    }

    public static int MySum<T>(this IEnumerable<T> source, Func<T, int> func)
    {
        var result = 0;
        foreach (var price in source)
        {
            result += func(price);
        }

        return result;
    }

    public static IEnumerable<T> MyWhere<T>(this IEnumerable<T> source, Func<T, bool> func)
    {
        foreach (var p in source)
        {
            if (func(p))
            {
                yield return p;
            }
        }
    }

    public static IEnumerable<TResult> MySelect<TSource, TResult>(this IEnumerable<TSource> source,
        Func<TSource, TResult> func)
    {
        foreach (var p in source)
        {
            yield return func(p);
        }
    }

    public static IEnumerable<TResult> MySelect<TSource, TResult>(this IEnumerable<TSource> source,
        Func<TSource, int, TResult> func)
    {
        var index = 1;
        foreach (var p in source)
        {
            yield return func(p, index++);
        }
    }

    public static IEnumerable<string> Append91Port(IEnumerable<string> urlsList)
    {
        foreach (var url in urlsList)
        {
            if (url.IndexOf("tw") >= 0)
                yield return url + ":91";
            else yield return url;
        }
    }

    public static IEnumerable<T> MyTake<T>(this IEnumerable<T> source, int take)
    {
        var i = source.GetEnumerator();
        while (i.MoveNext())
        {
            if (take-- <= 0)
                yield break; //important
            yield return i.Current;
        }
    }

    public static IEnumerable<T> MySkip<T>(this IEnumerable<T> source, int skipCount)
    {
        var i = source.GetEnumerator();
        //while (i.MoveNext())
        //{
        //    if (takeCount-- > 0)
        //        continue;
        //    yield return i.Current;
        //}

        var count = 1;
        while (i.MoveNext())
        {
            if (count > skipCount)
                yield return i.Current;
            count++;
        }
    }

    public static IEnumerable<T> mySelectSkip<T>(this IEnumerable<T> source, Func<T, bool> func, int skipCount)
    {
        var i = source.GetEnumerator();
        while (i.MoveNext())
        {
            if (func(i.Current))
            {
                if (skipCount-- <= 0)
                {
                    yield return i.Current;
                }
            }
            else
            {
                yield return i.Current;
            }
        }
    }

    public static IEnumerable<T> MyTakeWhile<T>(this IEnumerable<T> source, Func<T, bool> func, int takeCount)
    {
        var i = source.GetEnumerator();
        var counter = 1;
        while (i.MoveNext())
        {
            if (func(i.Current) && counter++ <= takeCount)
                yield return i.Current;
        }
    }

    public static IEnumerable<T> MyRealTakeWhile<T>(this IEnumerable<T> source, Func<T, bool> func)
    {
        var i = source.GetEnumerator();

        var counter = 1;
        while (i.MoveNext())
        {
            if (func(i.Current))
                yield return i.Current;
            else
                yield break;
            counter++;
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


    public static IEnumerable<string> Tohttps(IEnumerable<string> urls)
    {
        foreach (var item in urls)
        {
            if (item.Contains("http:"))
            {
                yield return item.Replace("http:", "https:");
            }
            else
            {
                yield return item;
            }
        }
    }
}