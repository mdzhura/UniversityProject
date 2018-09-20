using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityProject
{
    class HTTPError : IComparable
    {
        public int code { get; set; }

        public String description { get; set; }

        public DateTime dateTime { get; set; }

        public int CompareTo(object obj)
        {
            var item = obj as HTTPError;
            return code.CompareTo(item.code);
        }

        public override bool Equals(object obj)
        {
            var item = obj as HTTPError;

            if (item == null)
            {
                return false;
            }

            return this.code.Equals(item.code);
        }

        public override int GetHashCode()
        {
            return this.code.GetHashCode();
        }
    }
}
