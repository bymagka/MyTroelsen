using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLotDAL.Models
{
    public partial class Car
    {
        public override string ToString()
        {
            return $"{this.CarNickName ?? "**No name**"} is a {this.Color}\n{this.Make} with ID {this.CarId}.";
        }
    }
}
