using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Core.Tests")]

namespace Core.Employees
{
    /// <summary>
    /// Реализует общие свойства и поведение сотрудников предприятия.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Начальник сотрудника.
        /// </summary>
        private BossBase _boss;

        /// <summary>
        /// Имя сотрудника.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Дата поступления на работу.
        /// </summary>
        public DateTime EnrollmentDate { get; }

        /// <summary>
        /// Базовая ставка.
        /// </summary>
        public decimal BaseRate => 1000M;   // Для простоты, это значение по-умолчанию одинаково для всех видов сотрудников.

        /// <summary>
        /// Начальник сотрудника.
        /// </summary>
        /// <exception cref="ArgumentException">Попытка назначить сотрудника начальником самому себе.</exception>
        public virtual BossBase Boss
        {
            get => _boss;
            set
            {
                if (value == this)
                {
                    throw new ArgumentException("Сотрудник не может быть начальником сам себе.");
                }
                _boss = value;
            }
        }

        /// <summary>
        /// Процент надбавки от базовой ставки за каждый год работы в компании.
        /// </summary>
        public virtual decimal YearBonusPercentage => 3M;

        /// <summary>
        /// Максимальный размер надбавки от базовой ставки за каждый год работы в компании, выраженный в процентах от базовой ставки.
        /// </summary>
        public virtual decimal BaseRateMaximumPercentage => 30M;

        /// <summary>
        /// Основной конструктор.
        /// </summary>
        /// <param name="name">Имя сотрудника.</param>
        /// <param name="enrollmentDate">Дата поступления на работу.</param>
        /// <exception cref="ArgumentNullException">Не указано имя сотрудника.</exception>
        public Employee(string name, DateTime enrollmentDate)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            EnrollmentDate = enrollmentDate;
        }

        /// <summary>
        /// Возвращает рассчитанную зарплату сотрудника.
        /// </summary>
        /// <remarks>
        /// Зарплата сотрудника - это базовая ставка (BaseRate) плюс процент (YearBonusPercentage) за каждый год работы в компании, но не более значения процента (BaseRateMaximumPercentage) от базовой ставки.
        /// </remarks>
        /// <param name="date">Дата, на которую необходимо рассчитать зарплату.</param>
        /// <returns>Рассчитанное значение зарплаты сотрудника.</returns>
        public virtual decimal GetSalary(DateTime date)
        {
            if (date.Date < EnrollmentDate.Date)
            {
                return 0M;
            }

            int yearsOfWork = GetYearsOfWork(date);
            if (yearsOfWork == 0)
            {
                return BaseRate;
            }

            decimal bonusForYear = BaseRate / 100 * YearBonusPercentage;
            decimal maxBaseRateBonus = BaseRate / 100 * BaseRateMaximumPercentage;
            decimal bonus = Math.Min(bonusForYear * yearsOfWork, maxBaseRateBonus);

            return BaseRate + bonus;
        }

        /// <summary>
        /// Переопределение строкового представления сотрудника.
        /// </summary>
        /// <returns>Представление сотрудника.</returns>
        public override string ToString() => Name;

        /// <summary>
        /// Возвращает количество полных лет, которые сотрудник отработал в компании.
        /// </summary>
        /// <param name="date">Дата, на которую необходимо рассчитать количество лет.</param>
        /// <returns>Количество полных лет работы сотрудника в компании.</returns>
        protected internal int GetYearsOfWork(DateTime date)
        {
            int years = date.Year - EnrollmentDate.Year;
            if (years <= 0)
            {
                return 0;
            }

            bool monthLess = date.Month < EnrollmentDate.Month; // Меньше ли месяц даты рассчёта даты приёма на работу.
            bool dayOfMonthLessEqual = date.Month == EnrollmentDate.Month && date.Day <= EnrollmentDate.Day;     // Меньше или равен день даты рассчёта дню даты приёма на работу.
            if (years > 0
                && (monthLess || dayOfMonthLessEqual))
            {
                years--;    // В такой ситуации нужно уменьшить количество лет, так как последний год отработан не полностью.
            }

            return years;
        }
    }
}
