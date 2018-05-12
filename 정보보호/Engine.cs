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
            encryption(1);
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
            encryption(2);
        }

        internal void Deduplication(ref String key)
        {
            List<char> dKey = new List<char>();
            key = key.ToLower();
            //key를 list에 넣기
            char[] tKey = key.ToArray();
            for (int i = 0; i < tKey.Length; i++)
            {
                if (tKey[i] == 'z')
                {
                    dKey.Add('q');
                }
                else
                {
                    dKey.Add(tKey[i]);
                }
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
            Context = Context.ToLower();
            //평문 정렬 하기
            if (Context.Length % 2 == 1)
            {
                Context += "x";
            }
            for (int i = 0; i < Context.Length / 2; i++)
            {
                if (Context[i * 2] == Context[i * 2 + 1])
                {
                    tContext += Context[i * 2].ToString() + "x" + Context[i * 2 + 1].ToString();
                }
                else
                {
                    tContext += Context[i * 2].ToString() + Context[i * 2 + 1].ToString();
                }

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
            Context = Context.ToLower();
            dContext = new char[Context.Length / 2, 2];
            for (int i = 0; i < Context.Length / 2; i++)
            {
                dContext[i, 0] = Context[i * 2];
                dContext[i, 1] = Context[i * 2 + 1];
            }
        }
        internal void encryption(int temp)
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
                if (temp == 1)
                {
                    Einsertion(ref fx, ref fy, ref sx, ref sy);
                }
                else
                {
                    Binsertion(ref fx, ref fy, ref sx, ref sy);
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
        internal void Einsertion(ref int fx, ref int fy, ref int sx, ref int sy)
        {
            if (fx == sx)
            {
                if (fy == 4)
                {
                    result += cryptogram[fx, 0].ToString();
                    result += cryptogram[sx, sy + 1].ToString();
                }
                else if (sy == 4)
                {
                    result += cryptogram[fx, fy + 1].ToString();
                    result += cryptogram[sx, 0].ToString();
                }
                else
                {
                    result += cryptogram[fx, fy + 1].ToString();
                    result += cryptogram[sx, sy + 1].ToString();
                }
            }
            else if (fy == sy)
            {
                if (fx == 4)
                {
                    result += cryptogram[0, fy].ToString();
                    result += cryptogram[sx + 1, sy].ToString();
                }
                else if (sx == 4)
                {
                    result += cryptogram[fx + 1, fy].ToString();
                    result += cryptogram[0, sy].ToString();
                }
                else
                {
                    result += cryptogram[fx + 1, fy].ToString();
                    result += cryptogram[sx + 1, sy].ToString();
                }
            }
            else
            {
                result += cryptogram[sx, fy].ToString();
                result += cryptogram[fx, sy].ToString();
            }
        }

        internal void Binsertion(ref int fx, ref int fy, ref int sx, ref int sy)
        {
            if (fx == sx)
            {
                if (fy == 0)
                {
                    result += cryptogram[fx, 4].ToString();
                    result += cryptogram[sx, sy - 1].ToString();
                }
                else if (sy == 0)
                {
                    result += cryptogram[fx, fy - 1].ToString();
                    result += cryptogram[sx, 4].ToString();
                }
                else
                {
                    result += cryptogram[fx, fy - 1].ToString();
                    result += cryptogram[sx, sy - 1].ToString();
                }
            }
            else if (fy == sy)
            {
                if (fx == 0)
                {
                    result += cryptogram[4, fy].ToString();
                    result += cryptogram[sx - 1, sy].ToString();
                }
                else if (sx == 0)
                {
                    result += cryptogram[fx - 1, fy].ToString();
                    result += cryptogram[4, sy].ToString();
                }
                else
                {
                    result += cryptogram[fx - 1, fy].ToString();
                    result += cryptogram[sx - 1, sy].ToString();
                }
            }
            else
            {
                result += cryptogram[sx, fy].ToString();
                result += cryptogram[fx, sy].ToString();
            }
        }
        internal void RemoveResult()
        {
            Array.Clear(dContext, 0, dContext.Length);
            tContext = "";
            result = "";
        }
    }
}
