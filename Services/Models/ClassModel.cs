using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class ClassModel
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public List<UserModel>? Users { get; set; }
    }
}
