using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqTests
{
    internal static class YourOwnLinq
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
                var element = selector(enumerator.Current);
                yield return element;
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

        public static IEnumerable<TResult> MySelect<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> func)
        {
            foreach (var p in source)
            {
                yield return func(p);
            }
        }

        public static IEnumerable<TResult> MySelect<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, TResult> func)
        {
            var index = 1;
            var list = new List<TResult>();
            foreach (var p in source)
            {
                list.Add(func(p, index++));
            }

            return list;
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

        public static bool ChloeAny<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            ////step 1
            //foreach (var item in source)
            //{
            //    if (predicate(item))
            //        return true;
            //}
            //return false;

            ////step 2
            var item = source.GetEnumerator();
            while (item.MoveNext())
            {
                if (predicate(item.Current))
                    return true;
            }
            return false;
        }

        public static bool ChloeAll(this IEnumerable<ColorBall> colorBalls, Func<ColorBall, bool> predicate)
        {
            var enumerator = colorBalls.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (!(predicate(current)))
                {
                    return false;
                }
            }
            return true;
        }

        public static TSource ChloeFirst<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            ////step 1
            //foreach (var item in source)
            //{
            //    if (predicate(item))
            //    {
            //        return item;
            //    }
            //}
            //return default(TSource);

            ////step 2
            return ChloeFirst2(source, predicate, default(TSource));
        }

        public static TSource ChloeFirst2<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, TSource defaultValue)
        {
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (predicate(enumerator.Current))
                {
                    return enumerator.Current;
                }
            }

            return defaultValue;
        }

        public static TSource IsSingle<TSource>(this IEnumerable<TSource> colorBalls, Func<TSource, bool> predicate)
        {
            var enumerator = colorBalls.GetEnumerator();
            var isAleardyFind = false;
            var result = default(TSource);
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (predicate(current))
                {
                    if (isAleardyFind)
                    {
                        throw new InvalidOperationException();
                    }
                    result = current;
                    isAleardyFind = true;
                }
            }
            if (!isAleardyFind)
            {
                throw new InvalidOperationException();
            }

            return result;
        }

        public static TSource ChloeLast<TSource>(this IEnumerable<TSource> colorBalls, Func<TSource, bool> predicate)
        {
            var result = default(TSource);   
            var enumerator = colorBalls.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (predicate(current))
                {
                    result = current;
                }
            }

            return result;
        }

        public static bool IsSameBall(IEnumerable<ColorBall> colorBalls, ColorBall colorBall, ChloeBallCompare chloeBallCompare)
        {
            foreach (var ball in colorBalls)
            {
                if (chloeBallCompare.Equals(ball, colorBall))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsSameEmployee(IEnumerable<Employee> firstEmployees, IEnumerable<Employee> secondEmployee,
            ChloeEmployeeCompare chloeEmployeeCompare)
        {
            //var firstEnumerator = firstEmployees.GetEnumerator();
            //var secondEnumerator = secondEmployee.GetEnumerator();

            //var firstHasNext = firstEnumerator.MoveNext();
            //var secondHasNext = secondEnumerator.MoveNext();

            //while (firstHasNext && secondHasNext)
            //{
            //    if (!chloeEmployeeCompare.Equals(firstEnumerator.Current, secondEnumerator.Current))
            //    {
            //        return false;
            //    }
            //    firstHasNext = firstEnumerator.MoveNext();
            //    secondHasNext = secondEnumerator.MoveNext();
            //}

            //if (firstHasNext || secondHasNext)
            //{
            //    return false;
            //}

            //return true;

            var firstEnumerator = firstEmployees.GetEnumerator();
            var secondEnumerator = secondEmployee.GetEnumerator();

            while (true)
            {
                var firstMoveNext = firstEnumerator.MoveNext();
                var secondMoveNext = secondEnumerator.MoveNext();

                if (IsLengthDifferent(firstMoveNext, secondMoveNext))
                {
                    return false;
                }

                if (IsEnd(firstMoveNext))
                {
                    return true;
                }

                if (!chloeEmployeeCompare.Equals(firstEnumerator.Current, secondEnumerator.Current))
                {
                    return false;
                }
            }
        }

        private static bool IsLengthDifferent(bool firstMoveNext, bool secondMoveNext)
        {
            return firstMoveNext != secondMoveNext;
        }

        private static bool IsEnd(bool firstMoveNext)
        {
            return !firstMoveNext;
        }
    }
}