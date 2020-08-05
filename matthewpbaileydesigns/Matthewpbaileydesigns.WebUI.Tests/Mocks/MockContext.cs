using Matthewpbaileydesigns.Core.Contracts;
using Matthewpbaileydesigns.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Matthewpbaileydesigns.WebUI.Tests.Mocks
{
    public class MockContext<T> : IRepository<T> where T: BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        string className;

        public MockContext()
        {
            items = new List<T>();
        }


        public void Commit()
        {
            return;
        }

        public void Insert(T t)
        {
            items.Add(t);
        }

        public void Update(T t)
        {
            T typeToUpdate = items.Find(i => i.Id == t.Id);

            if (typeToUpdate != null)
            {
                typeToUpdate = t;
            }
            else
            {
                throw new Exception(className + " not found");
            }
        }

        public T Find(string Id)
        {
            T t = items.Find(i => i.Id == Id);
            if (t != null)
            {
                return t;
            }
            else
            {
                throw new Exception(className + " not found");
            }
        }

        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }

        public void Delete(string id)
        {
            T t = items.Find(i => i.Id == id);
            if (t != null)
            {
                items.Remove(t);
            }
            else
            {
                throw new Exception(className + " not found");
            }
        }
    }
}
