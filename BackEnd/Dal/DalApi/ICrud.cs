using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.DalApi
{
    public interface ICrud<T>
    {
        public List<T> GetAll();

        public List<T> Read(Predicate<T> filter);

        public bool Create(T item);

        public bool Delete(T item);

        public bool Update(T item);


    }
}
