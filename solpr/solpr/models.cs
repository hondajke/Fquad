using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Reflection;

namespace solpr
{

    public enum ComponentType {[Description("Процессор")] processor,
        [Description("Материнская плата")] mboard,
        [Description("Видеокарта")] video,
        [Description("Оперативная память")] ram,
        [Description("Жесткий диск")] disk }
    public enum PeripheryType { [Description("Мышь")] mouse,
        [Description("Клавиатура")] keyboard,
        [Description("Монитор")] monitor,
        [Description("Принтер")] printer,
        [Description("Веб-камера")] webcam,
        [Description("Другое")] other }
    public enum ComputerStatus {[Description("Окей")] ok,
        [Description("В ремонте")] under_repair,
        [Description("Списан")] scrapped }

    public class Manufacturer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Component> Components { get; set; }
        public virtual ICollection<Periphery> Peripheries { get; set; }
    }

    public class Component
    {
        [Key]
        public int Id { get; set; }
        public ComponentType Type { get; set; }
        public int ManufacturerId { get; set; }
        [ForeignKey("ManufacturerId")]
        public Manufacturer Manufacturer { get; set; }
        public int SpecId { get; set; }
        [ForeignKey("SpecId")]
        public Specs Specs { get; set; }
        public ICollection<Computer> Computers { get; set; }
    }

    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }

    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic_Name { get; set; }
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
    }
    public class Computer
    {
        [Key]
        public int Id { get; set; }
        public ComputerStatus Status { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public ICollection<Component> Components { get; set; }
    }

    public class Periphery
    {
        [Key]
        public int Id { get; set; }
        public PeripheryType Type { get; set; }
        [StringLength(100)]
        public string model { get; set; }
        public int ManufacturerId { get; set; }
        [ForeignKey("ManufacturerId")]
        public Manufacturer Manufacturer { get; set; }
        public int SpecId { get; set; }
        [ForeignKey("SpecId")]
        public Specs Specs { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
    }

    public class Specs
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Value { get; set; }
    }

    public static class GetTypes
    {
        static string GetDescription(Enum enumElement)
        {
            Type type = enumElement.GetType();

            MemberInfo[] memInfo = type.GetMember(enumElement.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return enumElement.ToString();
        }
    }
}