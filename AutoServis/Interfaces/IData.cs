using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServis.Interfaces
{
    interface IData<T>
    {
        void Sacuvaj();
        void Obrisi();
        void Azuriraj(T obj);
        bool PostojiDuplikat();
    }
}
