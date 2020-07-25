using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainProject
{
    public class Actor
    {
        public string Character { get; set; }
        public string CreditId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string ProfilePath { get; set; }
        public string Photo { get; set; }

        public Actor()
        {

        }

        public override bool Equals(object obj)
        {
            if (obj.GetType().Equals(typeof(Actor)))
            {
                return this.Id.Equals(((Actor)obj).Id);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
