using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl
{
    public interface IBusinessLayer<t>
    {
        List<t> GetAll();
        t GetById(int id);
        bool Save(t table);
        bool Delete(int id);
        bool Delete(t table);
        bool Trans(t table);
        t AuthorizeUser(t table);
    }
} 
