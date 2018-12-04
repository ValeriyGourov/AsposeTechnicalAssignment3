using System;
using Core.Employees;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Tests
{
    [TestClass]
    public class BossBaseTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Boss_SetSubordinateBoss_ArgumentException()
        {
            DataSet _dataSet = new DataSet();
            Sales sales = new Sales("Sales1", new DateTime(2015, 6, 30));
            Manager manager1 = new Manager("Manager1", new DateTime(2015, 6, 30)) { Boss = sales };
            Manager manager2 = new Manager("Manager2", new DateTime(2015, 6, 30)) { Boss = manager1 };
            _dataSet.Add(sales);
            _dataSet.Add(manager1);
            _dataSet.Add(manager2);

            sales.Boss = manager2;
        }
    }
}
