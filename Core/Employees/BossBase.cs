using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Employees
{
    /// <summary>
    /// Реализует общие свойства и поведение начальников.
    /// </summary>
    public abstract class BossBase : Employee
    {
        /// <summary>
        /// Процент от суммарной зарплаты подчинённых сотрудников.
        /// </summary>
        public abstract decimal SubordinateEmployeesSalaryPercentage { get; }

        /// <summary>
        /// Сотрудники, непосредственно подчинённые данному начальнику.
        /// </summary>
        public IEnumerable<Employee> SubordinateEmployees
        {
            get
            {
                DataSet dataSet = new DataSet();
                return dataSet.GetRange(item => item.Boss == this);
            }
        }

        /// <summary>
        /// Сотрудники, входящие в структуру текущего начальника на всех уровнях структуры подчинённости.
        /// </summary>
        public IEnumerable<Employee> AllSubordinateEmployees
        {
            get
            {
                var allSubordinateEmployees = new List<Employee>();
                FillAllSubordinateEmployees(SubordinateEmployees, allSubordinateEmployees);
                return allSubordinateEmployees;
            }
        }

        /// <summary>
        /// Начальник сотрудника.
        /// </summary>
        /// <remarks>
        /// Переопределяет свойство базового класса для добавления дополнительных проверок.
        /// </remarks>
        public override BossBase Boss
        {
            get => base.Boss;
            set
            {
                if (AllSubordinateEmployees.Contains(value))
                {
                    throw new ArgumentException("Начальником не может быть сотрудник, уже подчиняющийся данному сотруднику.");
                }
                base.Boss = value;
            }
        }

        /// <summary>
        /// Основной конструктор.
        /// </summary>
        /// <param name="name">Имя сотрудника.</param>
        /// <param name="enrollmentDate">Дата поступления на работу.</param>
        public BossBase(string name, DateTime enrollmentDate) : base(name, enrollmentDate)
        {
        }

        /// <summary>
        /// Возвращает суммарную зарплату всех подчинённых сотрудников.
        /// </summary>
        /// <param name="date">Дата, на которую необходимо рассчитать зарплату.</param>
        /// <returns>Рассчитанное значение зарплаты подчинённых сотрудников.</returns>
        protected abstract decimal GetSubordinateEmployeesSalary(DateTime date);

        /// <summary>
        /// Возвращает рассчитанную зарплату сотрудника.
        /// </summary>
        /// <remarks>
        /// Зарплата начальника рассчитывается аналогично зарплате сотрудника и к ней добавляется процент от суммарной зарплаты подчинённых сотрудников.
        /// </remarks>
        /// <param name="date">Дата, на которую необходимо рассчитать зарплату.</param>
        /// <returns>Рассчитанное значение зарплаты начальника.</returns>
        public override decimal GetSalary(DateTime date) => base.GetSalary(date) + GetSubordinateEmployeesSalary(date) / 100 * SubordinateEmployeesSalaryPercentage;

        /// <summary>
        /// Заполняет коллекцию подчинённых сотрудников всех уровней на основании коллекции сотрудников конкретного уровня.
        /// </summary>
        /// <param name="subordinateEmployees">Коллекция подчинённых сотрудников конкретного уровня иерархии.</param>
        /// <param name="allSubordinateEmployees">Коллекция подчинённых сотрудников всех уровней иерархии.</param>
        private void FillAllSubordinateEmployees(IEnumerable<Employee> subordinateEmployees, List<Employee> allSubordinateEmployees)
        {
            foreach (Employee subordinateEmployee in subordinateEmployees)
            {
                allSubordinateEmployees.Add(subordinateEmployee);
                if (subordinateEmployee is BossBase boss)
                {
                    FillAllSubordinateEmployees(boss.SubordinateEmployees, allSubordinateEmployees);
                }
            }
        }
    }
}
