using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace solpr
{
    enum ComponentType { video, processor, rom, disk }
    class Manufacturer
    {
        [Key]
        public string Name { get; set; }
        public ICollection<Component> Components { get; set; }
    }

    class Component
    {
        [Key]
        public int Id { get; set; }
        public ComponentType Type { get; set; }
        public int ManufacturerId { get; set; }
        [ForeignKey("ManufacturerId")]
        public Manufacturer Manufacturer { get; set; }
    }

    class Department
    {
        [Key]
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }    
    }
    class Employee
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

}
