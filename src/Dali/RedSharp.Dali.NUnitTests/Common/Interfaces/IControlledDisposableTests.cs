using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using NUnit;
using NUnit.Framework;

using RedSharp.Dali.Common.Interop;

namespace RedSharp.Dali.NUnitTests.Common.Interfaces
{
    [TestFixture]
    public class IControlledDisposableTests
    {
        public static Func<IDisposable>[] TestCases = new Func<IDisposable>[]
        {

        };

        public IDisposable _testedValue;

        [TearDown]
        public void AfterTest()
        {
            if(_testedValue != null)
            {
                try
                {
                    _testedValue.Dispose();
                }
                catch(Exception exception)
                {
                    Trace.WriteLine(exception.Message);
                    Trace.WriteLine(exception.StackTrace);
                }
            }
        }
    }
}
