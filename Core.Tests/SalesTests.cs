﻿using System;
using Core.Employees;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Tests
{
    [TestClass]
    public class SalesTests
    {
        private static DataSet _dataSet = new DataSet();
        private static  Sales _sales1;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            _sales1 = new Sales("Sales1", new DateTime(2004, 10, 11));
            _dataSet.Add(_sales1);

            _dataSet.Add(new Employee("Employee8", new DateTime(2008, 10, 29)) { Boss = _sales1 });

            Manager manager1 = new Manager("Manager1", new DateTime(2004, 10, 10)) { Boss = _sales1 };
            _dataSet.Add(manager1);

            _dataSet.Add(new Employee("Employee1", new DateTime(2005, 1, 2)) { Boss = manager1 });
            _dataSet.Add(new Employee("Employee2", new DateTime(2006, 3, 4)) { Boss = manager1 });
            _dataSet.Add(new Employee("Employee3", new DateTime(2007, 5, 6)) { Boss = manager1 });

            Manager manager2 = new Manager("Manager2", new DateTime(2008, 5, 5)) { Boss = manager1 };
            _dataSet.Add(manager2);

            _dataSet.Add(new Employee("Employee4", new DateTime(2008, 7, 8)) { Boss = manager2 });
            _dataSet.Add(new Employee("Employee5", new DateTime(2009, 9, 10)) { Boss = manager2 });

            Manager manager3 = new Manager("Manager3", new DateTime(2010, 6, 6));
            _dataSet.Add(manager3);

            _dataSet.Add(new Employee("Employee6", new DateTime(2010, 10, 11)) { Boss = manager3 });
            _dataSet.Add(new Employee("Employee7", new DateTime(2009, 12, 13)) { Boss = manager3 });
        }

        [TestMethod]
        public void GetSalary_6YearsWork_1087_25050675()
        {
            DateTime date = new DateTime(2011, 7, 1);
            decimal expectedSalary = 1087.25050675M;

            decimal salary = _sales1.GetSalary(date);

            Assert.AreEqual(expectedSalary, salary);
        }
    }
}
