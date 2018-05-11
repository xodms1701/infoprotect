using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 정보보호
{
    class Engine
    {
        char[,] dContext;
        String tContext = "";
        char[,] cryptogram = new char[5, 5];
        String result = "";
        internal void Einit(String key, String Context)
        {
            //키 중복제거
            Deduplication(ref key);
            //공백제거
            Context = Context.Replace(" ", "");
            //암호문 만들기
            MakeCryptogram(key);
            //평문 정렬하기
            Etheorem(Context);
            //입력받은 평문을 암호화 하기
            encryption(ref Context);
        }

        internal void Binit(String key, String Context)
        {
            //키 중복제거
            Deduplication(ref key);
            //공백제거
            Context = Context.Replace(" ", "");
            //암호문 만들기
            MakeCryptogram(key);
            //평문 정렬하기
            Btheorem(Context);
            //입력받은 평문을 복호화 하기
            encryption(ref Context);
        }

        internal void Deduplication(ref String key)
        {
            List<char> dKey = new List<char>();
            //key를 list에 넣기
            char[] tKey = key.ToArray();
            for (int i = 0; i < tKey.Length; i++)
            {
                dKey.Add(tKey[i]);
            }

            //list에 나머지 문자들 넣기
            for (int i = 0; i < 25; i++)
            {
                dKey.Add((char)('a' + i));
            }

            //중복제거
            IEnumerable<char> distinctKey = dKey.Distinct();

            //키에 중복제거된 키를 넣기
            key = String.Join("", distinctKey);

            dKey.Clear();
        }
        internal void MakeCryptogram(String key)
        {
            int cnt = 0;
            //5x5배열로 암호문 만들기
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    cryptogram[i, j] = key[cnt];
                    cnt++;
                }
            }
        }
        internal void Etheorem(String Context)
        {
            //평문 정렬 하기
            for (int i = 0; i < Context.Length; i += 2)
            {
                if (Context[i] == Context[i + 1])
                {
                    tContext += Context[i].ToString() + "x" + Context[i + 1].ToString();
                }
                else
                {
                    tContext += Context[i].ToString() + Context[i + 1].ToString();
                }
            }
            if (tContext.Length % 2 == 1)
            {
                tContext += "x";
            }
            //평문을 2차원 배열로 만들기
            dContext = new char[tContext.Length / 2, 2];

            for (int i = 0; i < tContext.Length / 2; i++)
            {
                dContext[i, 0] = tContext[i * 2];
                dContext[i, 1] = tContext[i * 2 + 1];
            }

        }
        internal void Btheorem(String Context)
        {
            dContext = new char[Context.Length / 2, 2];
            for (int i = 0; i < Context.Length / 2; i++)
            {
                dContext[i, 0] = Context[i * 2];
                dContext[i, 1] = Context[i * 2 + 1];
            }
        }
        internal void encryption(ref string Context)
        {
            int cnt = 0;
            
            //암호문으로 평문 암호화하기
            int fx = 0, fy = 0, sx = 0, sy = 0;
            while (cnt < dContext.Length / 2)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (dContext[cnt, 0].ToString() == cryptogram[i, j].ToString() ||
                            (dContext[cnt, 0].ToString() == "z" && cryptogram[i, j].ToString() == "q"))
                        {
                            fx = i;
                            fy = j;
                        }
                        if (dContext[cnt, 1].ToString() == cryptogram[i, j].ToString() ||
                            (dContext[cnt, 1].ToString() == "z" && cryptogram[i, j].ToString() == "q"))
                        {
                            sx = i;
                            sy = j;
                        }
                        if (fx > 0 && fy > 0 && sx > 0 && sy > 0)
                        {
                            break;
                        }
                    }
                }
                if (fx == sx)
                {
                    result += cryptogram[fx, fy + 1].ToString();
                    result += cryptogram[sx, sy + 1].ToString();
                }
                else if (fy == sy)
                {
                    result += cryptogram[fx + 1, fy].ToString();
                    result += cryptogram[sx + 1, sy].ToString();
                }
                else
                {
                    result += cryptogram[sx, fy].ToString();
                    result += cryptogram[fx, sy].ToString();
                }
                cnt++;
                fx = 0;
                fy = 0;
                sx = 0;
                sy = 0;
            }
        }
        internal String GetContext()
        {
            return result;
        }
    }
}
