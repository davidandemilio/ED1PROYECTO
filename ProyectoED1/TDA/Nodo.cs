using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDA
{
    public class Nodo<T,K>
    {
        public elemento<T,K> []elementos { get; set; }
        public Nodo<T,K>[] hijos { get; set; }
        public Nodo<T, K> padre { get; set; }

        public Nodo(int grado){
            elementos = new elemento<T, K>[grado - 1];
            hijos = new Nodo<T, K>[grado];
        }
    }
    public class elemento<T,K> 
    {
      public T valor { get; set; }
      public  K llave { get; set; }
        public elemento(T val,K llave) {
            valor = val;
            llave = this.llave;

        }
    }
}
