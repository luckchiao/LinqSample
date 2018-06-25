using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqTests
{
    public class MyPracticeLinq
    {

    }

    public class Something
    {
        Dictionary<string, Func<bool>> funcs = new Dictionary<string, Func<bool>>();

        public Something()
        {
            funcs.Add("IsAccountValid", () => true);
            funcs.Add("IsPersonValid", () => false);
        }

        public void DoSomethingByBusinessType()
        {
            DoSomething("IsAccountValid");
            DoSomething("IsPersonValid");
        }

        private void DoSomething(string businessType)
        {
            var booleanResult = funcs[businessType]();
        }
    }
}
