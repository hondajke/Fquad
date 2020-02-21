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
