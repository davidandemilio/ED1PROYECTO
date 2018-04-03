using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDA
{
    public class ArbolB<T,K>
    {
        public Nodo<T, K> raiz { get; set; }
        public void insertar(T valor,K llave) {

            elemento<T, K> elemento_insertar = new elemento<T, K>(valor,llave);
            if (raiz == null) {
                raiz.elementos[0] = elemento_insertar;
            }
        }
    }
}
