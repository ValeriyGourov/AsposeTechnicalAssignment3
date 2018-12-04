using System;
using System.Linq;

namespace Core.Employees
{
    public class Sales : BossBase
    {
        /// <summary>
        /// Процент надбавки от базовой ставки за каждый год работы в компании.
        /// </summary>
        public override decimal YearBonusPercentage => 1M;

        /// <summary>
        /// Максимальный размер надбавки от базовой ставки за каждый год работы в компании, выраженный в процентах от базовой ставки.
        /// </summary>
        public override decimal BaseRateMaximumPercentage => 35M;

        /// <summary>
        /// Процент от суммарной зарплаты подчинённых сотрудников.
        /// </summary>
        public override decimal SubordinateEmployeesSalaryPercentage => 0.3M;

        /// <summary>
        /// Основной конструктор.
        /// </summary>
        /// <param name="name">Имя сотрудника.</param>
        /// <param name="enrollmentDate">Дата поступления на работу.</param>
        public Sales(string name, DateTime enrollmentDate) : base(name, enrollmentDate)
        {
        }

        /// <summary>
        /// Возвращает суммарную зарплату всех подчинённых сотрудников всех уровней иерархии.
        /// </summary>
        /// <param name="date">Дата, на которую необходимо рассчитать зарплату.</param>
        /// <returns>Рассчитанное значение зарплаты подчинённых сотрудников.</returns>
        protected override decimal GetSubordinateEmployeesSalary(DateTime date) => AllSubordinateEmployees.Sum(item => item.GetSalary(date));
    }
}
