using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 정보보호
{
    class Engine
    {
        int l;
        char[,] dft = new char[,] { { 'a', 'b' }, { 'c', 'd' }, { 'e', 'f' }, { 'g', 'h' },
            { 'i', 'j' }, { 'k', 'l' }, { 'm', 'n' }, { 'o', 'p' }, { 'q', 'r' }, { 's', 't' }, { 'u', 'v' }, { 'w', 'x' }, { 'y', 'z' } };
        List<char> dKey = new List<char>();
        char[,] rKey;
        internal void init(String key)
        {
            //중복제거
            Deduplication(ref key);
            //입력받은 키를 이용해서 2차원 배열 만들기
            if(key.Length % 2 == 1)
            {
                l = key.Length / 2 + 1;
            }
            else
            {
                l = key.Length/2;
            }
            rKey = new char[l,2];
            for(int i=0; i < l; i++)
            {
                for(int j = 0; j < 2; j++)
                {
                    rKey[i, j] = key[i*2+j];
                }
            }
            //입력받은 평문을 암호화 하기
            encryption();
        }
        internal void Deduplication(ref String key)
        {
            char[] tKey = key.ToArray();
            for(int i = 0; i < tKey.Length; i++)
            {
                dKey.Add(tKey[i]);
            }
            IEnumerable<char> distinctKey = dKey.Distinct();
            key = String.Join("", distinctKey);
            dKey.Clear();
        }
        internal void encryption()
        {

        }
    }
}
