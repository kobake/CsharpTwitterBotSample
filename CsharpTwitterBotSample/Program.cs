using CoreTweet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CsharpTwitterBotSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            var r = new Random();

            try
            {
                // セッション開始
                var session = OAuth.Authorize("api_key", "api_secret"); // ※ここは適宜書き換える
                
                // 認証ページをブラウザで開く (このページでPINCODEをコピーしてくる)
                Process.Start(session.AuthorizeUri.AbsoluteUri);

                // PINCODE入力
                Console.Write("PINCODE> ");
                var pincode = Console.ReadLine();

                // トークン取得
                var tokens = OAuth.GetTokens(session, pincode);
                Console.WriteLine(tokens);

                while (true)
                {
                    // 発言内容
                    string[] table = { "🍣", "🍤", "🍜", "🍙", "🍛", "♨", "🐬", "👍", "👎", "🍮", "🍵" };
                    string body = table[r.Next(table.Length)];

                    // 発言
                    Console.WriteLine("Tweet " + body); // さすがに絵文字はコンソールでは化ける…
                    try
                    {
                        tokens.Statuses.Update(status => body);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }

                    // 次の発言は1時間後
                    Thread.Sleep(1000 * 60 * 60);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
