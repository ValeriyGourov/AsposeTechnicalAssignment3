using System;
using System.Collections.Generic;
using System.Linq;
using Core.Employees;

namespace Core
{
    /// <summary>
    /// Хранилище данных о сотрудниках предприятия и методы работы с этими данными.
    /// </summary>
    public class DataSet
    {
        /// <summary>
        /// Коллекция сотрудников предприятия.
        /// </summary>
        private static List<Employee> _employees = new List<Employee>();

        /// <summary>
        /// Добавляет сотрудника в хранилище.
        /// </summary>
        /// <param name="employee">Добавляемый сотрудник.</param>
        public void Add(Employee employee) => _employees.Add(employee);

        /// <summary>
        /// Возвращает коллекцию сотрудников предприятия с учётом отбора.
        /// </summary>
        /// <param name="filter">Отбор сотрудников.</param>
        /// <returns>Коллекция сотрудников предприятия.</returns>
        public IEnumerable<Employee> GetRange(Func<Employee, bool> filter = null)
        {
            IEnumerable<Employee> result = _employees;
            if (filter != null)
            {
                result = result.Where(filter);
            }

            return result.ToList();
        }

        /// <summary>
        /// Возвращает суммарную зарплату всех сотрудников предприятия в целом.
        /// </summary>
        /// <param name="date">Дата, на которую необходимо рассчитать зарплату.</param>
        /// <returns>Рассчитанное значение зарплаты всех сотрудников.</returns>
        public decimal GetTotalSalary(DateTime date) => GetRange().Sum(item => item.GetSalary(date));
    }
}
