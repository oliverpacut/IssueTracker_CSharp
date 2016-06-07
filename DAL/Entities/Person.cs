using Riganti.Utils.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Person : IEntity<int>
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public string Email { get; set; }

        public bool IsAdmin { get; set; }
    }
}
