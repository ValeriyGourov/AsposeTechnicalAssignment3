using System;
using System.Collections.Generic;
using Core.Employees;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Tests
{
    [TestClass]
    public class EmployeeTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Boss_SetSelfBoss_ArgumentException()
        {
            Manager manager = new Manager("Manager1", new DateTime(2015, 6, 30));

            manager.Boss = manager;
        }

        [TestMethod]
        public void GetYearsOfWork_DataCollection()
        {
            DateTime enrollmentDate = new DateTime(2015, 6, 30);
            var dates = new Dictionary<DateTime, int>
            {
                { new DateTime(2015, 7, 1), 0 },
                { new DateTime(2016, 6, 30), 0 },
                { new DateTime(2016, 7, 1), 1 },
                { new DateTime(2015, 6, 30), 0 },
                { new DateTime(2015, 6, 29), 0 },
                { new DateTime(2017, 3, 10), 1 },
                { new DateTime(2018, 11, 20), 3 },
                { new DateTime(2018, 6, 30), 2 },
            };

            foreach (var date in dates)
            {
                Employee employee = new Employee("Employee1", enrollmentDate);
                int expectedYears = date.Value;

                int years = employee.GetYearsOfWork(date.Key);
                Assert.AreEqual(expectedYears, years);
            }
        }

        [TestMethod]
        public void GetSalary_DateBeforeEnrollmentDate_0()
        {
            Employee employee = GetEmployee();
            DateTime date = new DateTime(2000, 4, 30);
            decimal expectedSalary = 0M;

            decimal salary = employee.GetSalary(date);

            Assert.AreEqual(expectedSalary, salary);
        }

        [TestMethod]
        public void GetSalary_DateDaySameEnrollmentDateDay_BaseRate()
        {
            Employee employee = GetEmployee();
            DateTime date = new DateTime(2001, 6, 30, 1, 2, 3);
            decimal expectedSalary = employee.BaseRate;

            decimal salary = employee.GetSalary(date);

            Assert.AreEqual(expectedSalary, salary);
        }

        [TestMethod]
        public void GetSalary_11MonthsWork_BaseRate()
        {
            Employee employee = GetEmployee();
            DateTime date = new DateTime(2001, 5, 31);
            decimal expectedSalary = employee.BaseRate;

            decimal salary = employee.GetSalary(date);

            Assert.AreEqual(expectedSalary, salary);
        }

        [TestMethod]
        public void GetSalary_2YearsWork_1060()
        {
            Employee employee = GetEmployee();
            DateTime date = new DateTime(2002, 7, 1);
            decimal expectedSalary = 1060M;

            decimal salary = employee.GetSalary(date);

            Assert.AreEqual(expectedSalary, salary);
        }

        [TestMethod]
        public void GetSalary_11YearsWork_1300()
        {
            Employee employee = GetEmployee();
            DateTime date = new DateTime(2011, 7, 1);
            decimal expectedSalary = 1300M;

            decimal salary = employee.GetSalary(date);

            Assert.AreEqual(expectedSalary, salary);
        }

        private Employee GetEmployee()
        {
            DateTime enrollmentDate = new DateTime(2000, 6, 30);
            return new Employee("Employee1", enrollmentDate);
        }
    }
}
