using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kutuphane.Models
{
    public class Books
    {
      [Key]  public int BookId{ get; set; }
        public string BookName { get; set; }
        public int NumOfPages { get; set; }
        public virtual Authors Authors { get; set; }
        public virtual Publisher Publisher { get; set; }
    }
}
