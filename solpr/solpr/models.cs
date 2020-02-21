using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace solpr
{
    enum ComponentType { processor, mboard, video, ram, disk}
    enum PeripheryType { mouse, keyboard, monitor, printer, webcam, other}

    class Manufacturer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Component> Components { get; set; }
        public virtual ICollection<Periphery> Peripheries { get; set; }
    }

    class Component
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
    }

<<<<<<< HEAD
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
=======
    class Periphery 
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
>>>>>>> bc5cd93d60e8fbacbbc976eb6af63b4957cec0b5
    }

    class Specs 
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Value { get; set; }
    }
}
