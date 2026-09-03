using System;
using System.Collections.Generic;
using System.Text;
using TestForABP.Domain.Common;
using TestForABP.Domain.Halls.VO;

namespace TestForABP.Domain.Halls
{
    public class Amenity : Entity
    {
        public Guid Id { get; private set; }
        public string? Name { get; private set; }
        public Money? Price { get; private set; }

        private Amenity () { }

        internal Amenity (Guid id , string name , Money price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}
