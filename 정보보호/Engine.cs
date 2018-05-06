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
        char[,] cryptogram = new char[5,5];
        List<String> dContext = new List<String>();

        internal void init(String key, String Context)
        {
            //키 중복제거
            Deduplication(ref key);

            //입력받은 평문을 암호화 하기
            encryption(ref key, ref Context);
        }
        internal void Deduplication(ref String key)
        {
            //key를 list에 넣기
            char[] tKey = key.ToArray();
            for(int i = 0; i < tKey.Length; i++)
            {
                dKey.Add(tKey[i]);
            }

            //list에 나머지 문자들 넣기
            for(int i = 0; i < 25; i++)
            {
                dKey.Add((char)('a' + i));
            }

            //중복제거
            IEnumerable<char> distinctKey = dKey.Distinct();

            //키에 중복제거된 키를 넣기
            key = String.Join("", distinctKey);

            dKey.Clear();
        }
        internal void encryption(ref String key, ref String Context)
        {
            int cnt = 0;
            //5x5배열로 암호문 만들기
            for(int i = 0; i < 5; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    cryptogram[i, j] = key[cnt];
                    cnt++;
                }
            }

            //평문을 2차원 배열로 만들기
            for(int i=0; ;i+=2)
            {
                if (Context[i] == Context[i + 1])
                {
                    dContext.Add(Context[i] + "X");
                    i--;
                }
                else if(Context.Length-1 == i)
                {
                    dContext.Add(Context[i] + "X");
                    break;
                }
                else
                {
                    dContext.Add(Context[i].ToString() + Context[i+1].ToString());
                }
            }

        }
    }
}
