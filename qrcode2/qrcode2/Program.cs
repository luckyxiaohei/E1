using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using ThoughtWorks.QRCode.Codec;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
namespace qrcode2
{
    class Program
    {
        public void PrintQrToConsole()  //控制台输出
        {
            Console.Write(@"Type some text to QR code: ");
            string sampleText = Console.ReadLine();

            if (sampleText.Length < 10)
            {

                QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.M);
                QrCode qrCode = qrEncoder.Encode(sampleText);
                for (int j = 0; j < qrCode.Matrix.Width; j++)
                {
                    for (int i = 0; i < qrCode.Matrix.Width; i++)
                    {

                        char charToPrint = qrCode.Matrix[i, j] ? '█' : ' ';
                        Console.Write(charToPrint);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine(@"Press any key to quit.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine(@"Please type the QR code with right length and Press any key to quit ");
                Console.ReadKey();
            }


        }





       public void GenerateQRByThoughtWorks(string m,int linenum)  //依据参数qrcode内容和该内容行数制作并保存qrcode到E盘
        {

            if (m.Length < 10)
            {

                QRCodeEncoder encoder = new QRCodeEncoder
                {
                    QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE,//编码方式(注意：BYTE能支持中文，ALPHA_NUMERIC扫描出来的都是数字)
                    QRCodeScale = 4,//大小(值越大生成的二维码图片像素越高)
                    QRCodeVersion = 0,//版本(注意：设置为0主要是防止编码的字符串太长时发生错误)
                    QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M,//错误效验、错误更正(有4个等级)
                    QRCodeBackgroundColor = Color.Yellow,
                    QRCodeForegroundColor = Color.Green
                };


                Bitmap bcodeBitmap = encoder.Encode(m.ToString());

                //以下代码保证名字规范
                string head = string.Format("{0:D3}", linenum);



                char[] ch = m.ToCharArray();
                int lownum = 0;
                if (m.Length > 4) { lownum = 4; }
                else
                {
                    lownum = m.Length;
                }
                string last = null;
                for (int i = 0; i < lownum; i++)
                {
                    last = last + ch[i];
                }
                m = head + "+" + last;
                string qrpath = "E://" + m;
                //以上代码保证名字规范



                bcodeBitmap.Save(qrpath, ImageFormat.Png);
                bcodeBitmap.Dispose();


            }
            else
            {
                Console.WriteLine(@"Please type the QR code with right length and Press any key to quit ");
                Console.ReadKey();
            }
        }


        


        public void  CreatAndSave()  //读取文件，提供参数内容
        {
            string filePath = Console.ReadLine();
           
            try
            {

                // 创建一个 StreamReader 的实例来读取文件 ,using 语句也能关闭 StreamReader
                using (System.IO.StreamReader sr = new System.IO.StreamReader(filePath,System.Text.Encoding.GetEncoding("gb2312")))
                {

                    string line;
                    Program k = new Program();
                    int ln = 0;

                    while ((line = sr.ReadLine()) != null)
                    {
                        ln++;
                        k.GenerateQRByThoughtWorks(line, ln);
                    }
                }
            }
            catch (Exception e)
            {
                // 向用户显示出错消息
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            
        }

        public void ReadCsv()  //读取excel文件（转换为csv)
        {
          
            Console.WriteLine("输入csv目录");
            string path = Console.ReadLine();
            string data = null;
            string[] val = null;
            try
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader(path, System.Text.Encoding.Default))
                {
                    while ((data = sr.ReadLine()) != null)
                    {
                        val = data.Split(',');
                    }
                }
                Program p = new Program();

                for (int i = 0; i < val.Length; i++)
                {
                    p.GenerateQRByThoughtWorks(val[i], i+1);
                }
            }
            catch (Exception e)
            {
                // 向用户显示出错消息
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

        }










        static void Main(string[] args)
        {


            /* Console.Write(@"type the path");
             string n = Console.ReadLine();
             Program k = new Program();
             StreamReader sr = new StreamReader(n, Encoding.Default);
             String line;
             int ln = 0;
             while((line=sr.ReadLine()) != null)
             {
                 ln++;
                 k.GenerateQRByThoughtWorks(line,ln);
             }
             ln = 0;*/
            //Program k = new Program();
            //k.PrintQrToConsole();
            Program p = new Program();
            p.ReadCsv();





        }
    }
}
